using AutoMapper;
using HouseRentManagement.Data;
using HouseRentManagement.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentManagement.Repository
{
    public class HousesRepository : IHousesRepository
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;
        //private readonly Profile _profileObj;

        public HousesRepository(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //get my house can also be possible.
        public async Task<List<HouseDetailsModel>> GetAllHousesAsync()
        {
            //Mapping in a single method
            //_profileObj.CreateMap<HouseDetails, HouseDetailsModel>().ReverseMap(); 
            return await _context.HouseDetails.Select(x=>_mapper.Map<HouseDetailsModel>(x)).ToListAsync();
        }
        public async Task<HouseDetailsModel> GetHouseAsync(string HouseId) {
            return _mapper.Map<HouseDetailsModel>(await _context.HouseDetails.FindAsync(HouseId));
        }
        public async Task<List<HouseDetailsModel>> GetMyHouseAsync(string UserId)
        {
            return await _context.HouseDetails.Where(x => x.UserId == UserId).Select(x => _mapper.Map<HouseDetailsModel>(x)).ToListAsync();
        }

        public async Task<string> AddHouseAsync(string userId, HouseDetailsModel house) {
            var houseInfo=_mapper.Map<HouseDetails>(house);
            houseInfo.UserId = userId;

            _context.HouseDetails.Add(houseInfo);
            await _context.SaveChangesAsync();

            return houseInfo.Id;
        }
        public async Task UpdateHouseAsync(string userId,string houseId, HouseDetailsModel house) {
            var houseInfo=_mapper.Map<HouseDetails>(house);
            houseInfo.Id = houseId;
            houseInfo.UserId = userId;
            _context.HouseDetails.Update(houseInfo);
            await _context.SaveChangesAsync();
        }
        
        public async Task<bool> UpdateByPatchHouseAsync(string houseId, JsonPatchDocument house) {
            var houseInfo = await _context.HouseDetails.FindAsync(houseId);
            if (houseInfo != null){
                house.ApplyTo(houseInfo);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task DeleteHouseAsync(string houseId) {
            
            _context.HouseDetails.Remove(new HouseDetails() { Id=houseId });
            await _context.SaveChangesAsync();
        }
    }
}
