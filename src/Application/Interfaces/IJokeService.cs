using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IJokeService
    {
        Task<JokeDTO> GetRandomJokeAsync();
        Task<List<string>> GetJokeTypesAsync();
        Task<List<JokeDTO>> GetSeveralRandomJokesAsync(int amount);
        Task<List<JokeDTO>> GetRandomJokeByTypeAsync(string jokeType);
        Task<JokeDTO> GetJokeById(int id);
    }
}
