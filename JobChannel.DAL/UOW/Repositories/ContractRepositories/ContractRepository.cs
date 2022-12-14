using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using JobChannel.Domain.BO;

namespace JobChannel.DAL.UOW.Repositories.ContractRepositories
{
    public class ContractRepository : IContractRepository
    {
        private readonly IDbSession _dbSession;

        public ContractRepository(IDbSession dbSession) => _dbSession = dbSession;

        public async Task<IEnumerable<Contract>> GetAllContracts()
        {
            string query = @"SELECT c.Id, c.Name, c.Code
                            FROM JobChannel.Contract c";

            return await _dbSession.Connection.QueryAsync<Contract>(query);
        }

        public async Task<Contract?> GetById(int id)
        {
            string query = @"SELECT c.Id, c.Name, c.Code
                            FROM JobChannel.Contract c
                            WHERE c.Id = @id";

            return await _dbSession.Connection.QueryFirstOrDefaultAsync<Contract>(query, new { id });
        }
    }
}
