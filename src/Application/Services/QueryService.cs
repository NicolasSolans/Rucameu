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
        private readonly ICartRepository _cartRepository;
        private readonly IRepositoryBase<User> _userRepository;

        public QueryService(IRepositoryBase<Query> repositoryBase, ICartRepository cartRepository, IRepositoryBase<User> userRepository)
        {
            _repositoryBase = repositoryBase;
            _cartRepository = cartRepository;
            _userRepository = userRepository;
        }

        public async Task<QueryDTO> CreateQuery(CreateQueryDTO createQuery)
        {
            //Proceso de creación de la query
            var cart = await _cartRepository.GetByIdAsync(createQuery.CartId);
            if (cart.Query != null)throw new Exception("Este carrito ya tiene una consulta asociada.");
            if(cart.Items.Count == 0) throw new Exception("El carrito no tiene items para consultar.");

            var query = createQuery.ToEntity(cart);
            var newQuery = await _repositoryBase.CreateAsync(query);

            //Proceso de crear un Cart despues de crear la query.
            //Usamos la logica de Create() del CartService.
            var user = await _userRepository.GetByIdAsync(cart.UserId);
            var newCart = new Cart
            {
                UserId = cart.UserId,
                User = user,
                TotalPrice = 0
            };
            await _cartRepository.CreateAsync(newCart);

            //return del DTO
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
