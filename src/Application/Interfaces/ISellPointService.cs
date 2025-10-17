using Application.Models;
using Application.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISellPointService
    {
        Task<SellPointDTO> CreateSellPoint(CreateSellPointDTO createSellPointDTO);
        Task<List<SellPointDTO>> GetAllSellPoints();
        Task<List<SellPointDTO>> GetSellPointByAdress(string Adress);
        Task DeleteSellPoint(int id);
        Task<SellPointDTO> UpdateSellPoint(UpdateSellPointDTO updateSellPointDTO);
        Task<List<SellPointCalenderDTO>> GetSellPointsInDateRange(DateTime startDate, DateTime endDate);
    }
}
