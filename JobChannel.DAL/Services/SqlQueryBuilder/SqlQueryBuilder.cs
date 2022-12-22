using System.Collections.Generic;
using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dapper;

namespace JobChannel.DAL.Services.SqlQueryBuilder
{
    public class SqlQueryBuilder
    {
        public bool WhereClauseUsed { get; protected set; }

        protected DynamicParameters Parameters { get; }
        private readonly StringBuilder _request;

        private const string LeadingSpace = " ";
        private bool IsEmpty => _request.Length == 0;

        public SqlQueryBuilder()
        {
            _request = new StringBuilder();
            Parameters = new DynamicParameters();
        }

        public SqlQueryBuilder Append(string sqlPart, params object[]? parameters)
        {
            if (ParseParameters(sqlPart, parameters))
            {
                _request.Append(sqlPart + LeadingSpace);

                if (!WhereClauseUsed && sqlPart.Contains("WHERE"))
                {
                    WhereClauseUsed = true;
                }
            }

            return this;
        }

        public SqlQueryBuilder Where(string clause, params object[]? parameters)
            => Append(IsEmpty ? clause : $"WHERE {clause}", parameters);

        public SqlQueryBuilder Or(string clause, params object[]? parameters)
            => Append(IsEmpty ? clause : $"OR {clause}", parameters);

        public SqlQueryBuilder And(string clause, params object[] parameters)
            => !WhereClauseUsed ? Where(clause, parameters) : Append(IsEmpty ? clause : $"AND {clause}", parameters);

        public SqlQueryBuilder And(SqlExpressionBuilder sqlQuery)
        {
            _request.Append((!WhereClauseUsed ? "WHERE (" : "AND (") + sqlQuery._request + ") ");

            WhereClauseUsed = true;

            Parameters.AddDynamicParams(sqlQuery.Parameters);

            return this;
        }

        public SqlQueryBuilder LeftJoin(string table, string on, params object[]? parameters)
            => Append($"LEFT JOIN {table} ON {on}", parameters);

        public SqlQueryBuilder OrderBy(string clause, params object[]? parameters)
        {
            if (null == parameters)
            {
                Append($"ORDER BY {clause}");
            }
            else
            {
                var orderByClauseWithParameters = BuildDapperOrderByClauseWithParameters(clause, parameters);

                if (orderByClauseWithParameters.Length > 2)
                {
                    Append($"ORDER BY {orderByClauseWithParameters}");
                }
            }

            return this;
        }

        public SqlQueryBuilder Skip(string clause, params object[]? parameters)
            => Append($"OFFSET {clause} ROWS", parameters);

        public SqlQueryBuilder Take(string clause, params object[]? parameters)
            => Append($"FETCH NEXT {clause} ROWS ONLY", parameters);

        public IEnumerable<T> Query<T>(IDbConnection connection)
            => connection.Query<T>(_request.ToString(), Parameters);

        public SqlMapper.GridReader QueryMultiple(IDbConnection connection)
            => connection.QueryMultiple(_request.ToString(), Parameters);

        public Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection)
            => connection.QueryAsync<T>(_request.ToString(), Parameters);

        public T QueryFirst<T>(IDbConnection connection)
            => connection.QueryFirst<T>(_request.ToString(), Parameters);

        private bool ParseParameters(string sqlPart, params object?[]? parameters)
        {
            try
            {
                var inputParameters = Regex.Matches(sqlPart, "@.[^ )]*");

                if (null == parameters || inputParameters.Count != parameters.Length)
                {
                    return false;
                }

                for (var i = 0; i < parameters.Length; i++)
                {
                    if (null == parameters[i])
                    {
                        return false;
                    }

                    Parameters.Add(inputParameters[i].Value, parameters[i]);
                }
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException or ArgumentNullException or IndexOutOfRangeException)
                {
                    return false;
                }

                throw;
            }

            return true;
        }

        private string BuildDapperOrderByClauseWithParameters(string clause, params object[] parameters)
        {
            /* With Dapper and parameters, order by clause must be like :
              ORDER BY 
                  CASE WHEN @SQLParameter = 'Id ASC' THEN Id END ASC,
                  ...
                  CASE WHEN @SQLParameter = 'Id DESC' THEN Id END DESC;
    */
            if (parameters.Length == 0)
            {
                return clause;
            }

            if (!ParseParameters(clause, parameters))
            {
                return string.Empty;
            }

            var cases = string.Empty;
            const int idField = 0;
            const int idDirection = 1;

            for (var i = 0; i < parameters.Length; i++)
            {
                var parameterValue = parameters[i].ToString();

                if (null == parameterValue)
                {
                    continue;
                }

                var fieldsAndDirectionPair = parameterValue.Split(' ');

                var casesSeparator = i == parameters.Length - 1 ? ';' : ',';

                if (fieldsAndDirectionPair.Length >= 1)
                {
                    var sortDirection = fieldsAndDirectionPair.Length == 2 ? fieldsAndDirectionPair[idDirection] : "ASC";

                    cases +=
                        $"CASE WHEN {clause} ='{fieldsAndDirectionPair[idField]}' THEN {fieldsAndDirectionPair[idField]} END {sortDirection}{casesSeparator}" +
                        Environment.NewLine;
                }
            }

            return cases.Length > 0 ? cases : clause;
        }
    }

    public class SqlSelectBuilder : SqlQueryBuilder
    {
        public SqlSelectBuilder(string select, string from)
        {
            Append($"SELECT {select} FROM {from}");
        }
    }

    public class SqlExpressionBuilder : SqlQueryBuilder
    {
        public SqlExpressionBuilder(bool whereClauseUsed)
        {
            WhereClauseUsed = whereClauseUsed;
        }
    }
}
