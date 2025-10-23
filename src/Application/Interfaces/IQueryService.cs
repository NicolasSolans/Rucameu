using Application.Models;
using Application.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IQueryService
    {
        Task<List<QueryDTO>> GetAllQueries();
        Task<QueryDTO> CreateQuery(CreateQueryDTO createQuery);
        Task<QueryDTO> GetQueryById(int id);
    }
}
