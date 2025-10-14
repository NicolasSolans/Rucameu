using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        //GET
        Task<T> GetByIdAsync(int idEntity);
        Task<List<T>> GetAllAsync();
        //Task<List<T>> GetByNameAsync(string nameEntity);

        //POST
        Task<T> CreateAsync(T entity);

        //UPDATE
        Task UpdateAsync(T updateEntity);

        //Aplica un borrado lógico, no borra literalmente la instancia de la clase.
        Task DisableAsync(T disableEntity);

        //Aplica un borrado literal, borra la instancia de la clase.
        Task DeleteAsync(T deleteEntity);
    }
}
