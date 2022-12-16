using Dapper;
using JobChannel.Domain.BO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobChannel.DAL.UOW.Repositories.JobRepositories
{
    public class JobRepository : IJobRepository
    {
        private readonly IDbSession _dbSession;

        public JobRepository(IDbSession dbSession) => _dbSession = dbSession;

        public async Task<IEnumerable<Job>> GetAll()
        {
            string query = @"SELECT j.Id, j.Name, j.CodeRome
                            FROM JobChannel.Job j";

            return await _dbSession.Connection.QueryAsync<Job>(query);
        }

        public async Task<Job?> GetById(int id)
        {
            string query = @"SELECT j.Id, j.Name, j.CodeRome
                            FROM JobChannel.Job j
                            WHERE Id = @id";

            return await _dbSession.Connection.QueryFirstOrDefaultAsync<Job>(query, new { id });
        }
    }
}
