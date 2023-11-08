﻿using Dapper;
using JobChannel.Domain.BO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobChannel.DAL.UOW.Repositories.RegionRepositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly IDbSession _dbSession;

        public RegionRepository(IDbSession dbSession) => _dbSession = dbSession;

        public async Task<IEnumerable<Region>> GetAll()
        {
            string query = @"SELECT r.Id, r.Name, r.Code
                            FROM JobChannel.Region r";

            return await _dbSession.Connection.QueryAsync<Region>(query);
        }

        public async Task<Region?> GetById(int id)
        {
            string query = @"SELECT r.Id, r.Name, r.Code
                            FROM JobChannel.Region r
                            WHERE r.Id = @Id";

            return await _dbSession.Connection.QueryFirstOrDefaultAsync<Region>(query, new { id });
        }
    }
}
