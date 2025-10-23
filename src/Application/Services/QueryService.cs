using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class QueryService : IQueryService
    {
        private readonly IRepositoryBase<Query> _repositoryBase;
        
        public QueryService(IRepositoryBase<Query> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public async Task<QueryDTO> CreateQuery(CreateQueryDTO createQuery)
        {
            var query = createQuery.ToEntity();
            var newQuery = await _repositoryBase.CreateAsync(query);
            return QueryDTO.FromEntity(newQuery);
        }

        public async Task<List<QueryDTO>> GetAllQueries()
        {
            return QueryDTO.CreateListDTO(await _repositoryBase.GetAllAsync());
        }

        public async Task<QueryDTO> GetQueryById(int id)
        {
            var query = await _repositoryBase.GetByIdAsync(id);
            if (query == null) throw new Exception("La query NO existe");

            return QueryDTO.FromEntity(query);
        }
    }
}
