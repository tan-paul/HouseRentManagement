using HouseRentManagement.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HouseRentManagement.Repository
{
    public interface IHousesRepository
    {
        Task<List<HouseDetailsModel>> GetAllHousesAsync();
        Task<HouseDetailsModel> GetHouseAsync(string HouseId);
        Task<List<HouseDetailsModel>> GetMyHouseAsync(string UserId);
        Task<string> AddHouseAsync(string userId, HouseDetailsModel house);
        Task UpdateHouseAsync(string userId,string houseId, HouseDetailsModel house);
        Task<bool> UpdateByPatchHouseAsync(string houseId, JsonPatchDocument house);
        Task DeleteHouseAsync(string houseId);
    }
}