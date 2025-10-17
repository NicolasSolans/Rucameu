using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Services
{
    public class SellPointService : ISellPointService
    {
        private readonly IRepositoryBase<SellPoint> _sellPointRepositoryBase;

        public SellPointService(IRepositoryBase<SellPoint> sellPointRepositoryBase)
        {
            _sellPointRepositoryBase = sellPointRepositoryBase;
        }

        public async Task<SellPointDTO> CreateSellPoint(CreateSellPointDTO createSellPointDTO)
        {
            var sellPoint = createSellPointDTO.ToEntity();
            await _sellPointRepositoryBase.CreateAsync(sellPoint);
            return SellPointDTO.FromEntity(sellPoint);
        }

        public async Task DeleteSellPoint(int id)
        {
            var sellPointToDelete = await _sellPointRepositoryBase.GetByIdAsync(id);
            if (sellPointToDelete == null)
            {
                throw new NotImplementedException();
            }
            await _sellPointRepositoryBase.DeleteAsync(sellPointToDelete);
        }

        public async Task<List<SellPointDTO>> GetAllSellPoints()
        {
            var sellPoints = await _sellPointRepositoryBase.GetAllAsync();
            return SellPointDTO.CreateListDTO(sellPoints);
        }

        public async Task<List<SellPointDTO>> GetSellPointByAdress(string Adress)
        {
            var allSellPoints = await _sellPointRepositoryBase.GetAllAsync();
            var sellPointAdress = allSellPoints.Where(p => p.Adress.Contains(Adress, StringComparison.OrdinalIgnoreCase)).ToList();

            return SellPointDTO.CreateListDTO(sellPointAdress);
        }

        public async Task<SellPointDTO> UpdateSellPoint(UpdateSellPointDTO updateSellPointDTO)
        {
            var sellPointToUpdate = await _sellPointRepositoryBase.GetByIdAsync(updateSellPointDTO.Id);
            if (sellPointToUpdate == null) throw new Exception("No se encontro el punto de venta.");
            return SellPointDTO.FromEntity(sellPointToUpdate);
        }

        public async Task<List<SellPointCalenderDTO>> GetSellPointsInDateRange(DateTime startDate, DateTime endDate)
        {
            var sellPoints = await _sellPointRepositoryBase.GetAllAsync();
            var sellPointsInRange = sellPoints.Where(s => s.Date >= startDate && s.Date <= endDate).ToList();
            return SellPointCalenderDTO.CreateListDTO(sellPointsInRange);
        }
    }
}
