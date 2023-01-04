using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using JobChannel.Domain.BO;
using JobChannel.Domain.Exceptions;

namespace JobChannel.DAL.UOW.Repositories.ContractRepositories
{
    internal class ContractRepository : IContractRepository
    {
        private readonly IDbSession _dbSession;

        public ContractRepository(IDbSession dbSession) => _dbSession = dbSession;

        public async Task<IEnumerable<Contract>> GetAll()
        {
            string query = @"SELECT c.Id, c.Name, c.Code
                            FROM JobChannel.Contract c";

            return await _dbSession.Connection.QueryAsync<Contract>(query);
        }

        public async Task<Contract> GetById(int id)
        {
            string query = @"SELECT c.Id, c.Name, c.Code
                            FROM JobChannel.Contract c
                            WHERE c.Id = @id";

            return (await _dbSession.Connection.QueryFirstOrDefaultAsync<Contract>(query, new { id })) ?? throw new ContractNotFoundException(id);
        }

        public async Task<Contract> GetByCode(string code)
        {
            string query = @"SELECT c.Id, c.Name, c.Code
                            FROM JobChannel.Contract c
                            WHERE c.Code = @code";

            return (await _dbSession.Connection.QueryFirstOrDefaultAsync<Contract>(query, new { code }));
        }
    }
}
