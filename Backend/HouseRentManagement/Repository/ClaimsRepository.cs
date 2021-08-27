using HouseRentManagement.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace HouseRentManagement.Repository
{
    public class ClaimsRepository: IClaimsRepository
    {
        public ClaimsDetailsModel UserClaims { get; }
        public ClaimsRepository(IHttpContextAccessor httpContextAccessor)
        {
            UserClaims = new ClaimsDetailsModel()
            {
                Id = httpContextAccessor.HttpContext.User?.FindFirstValue("UserId"),
                Name= httpContextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.Name)
            };

        }
    }
}
