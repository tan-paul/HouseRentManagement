using HouseRentManagement.Models;
using HouseRentManagement.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HouseRentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HousesController : ControllerBase
    {
        private readonly IHousesRepository _housesRepository;
        private readonly IClaimsRepository _claimsRepository;

        public HousesController(IHousesRepository housesRepository , IClaimsRepository claimsRepository)
        {
            _housesRepository = housesRepository;
            _claimsRepository = claimsRepository;
            //_userId = httpContextAccessor.HttpContext.User?.FindFirstValue("UserId");
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllHousesAsync() {
            return Ok(await _housesRepository.GetAllHousesAsync());
        }
        [HttpGet("{HouseId}")]
        public async Task<IActionResult> GetHouseAsync([FromRoute] string HouseId) {
            var house=await _housesRepository.GetHouseAsync(HouseId);
            if (house != null){
                return Ok(house);
            }
            return NotFound();
        }
        [HttpGet("myadd/{UserId}")]
        public async Task<IActionResult> GetMyHouseAsync([FromRoute] string UserId)
        {
            var house = await _housesRepository.GetMyHouseAsync(UserId);
            return Ok(house);
        }
        [HttpPost("")]
        public async Task<IActionResult> AddHouseAsync([FromBody] HouseDetailsModel houseDetails) {

            var UserId = _claimsRepository.UserClaims.Id;
            if (UserId != null)
            {
                var houseId = await _housesRepository.AddHouseAsync(_claimsRepository.UserClaims.Id, houseDetails);
                return Created("~/api/Houses/" + houseId, houseId);
            }

            //return StatusCode(503);
            return BadRequest();
        }
        [HttpPut("{HouseId}")]
        public async Task<IActionResult> UpdateHouseAsync([FromRoute] string houseId,[FromBody] HouseDetailsModel houseDetails) {
            await _housesRepository.UpdateHouseAsync(_claimsRepository.UserClaims.Id ,houseId,houseDetails);
            return Ok(); 
        }
        [HttpPatch("{HouseId}")]
        public async Task<IActionResult> UpdateByPatchHouseAsync([FromRoute] string houseId, [FromBody] JsonPatchDocument houseDetails)
        {
            if (await _housesRepository.UpdateByPatchHouseAsync(houseId, houseDetails)) return Ok(true);
            return BadRequest();
        }
        [HttpDelete("{HouseId}")]
        public async Task<IActionResult> DeleteHouseAsync([FromRoute] string houseId) {
            await _housesRepository.DeleteHouseAsync(houseId);
            return Ok();
        }


    }
}
