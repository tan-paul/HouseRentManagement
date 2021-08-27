using HouseRentManagement.Models;

namespace HouseRentManagement.Repository
{
    public interface IClaimsRepository
    {
        ClaimsDetailsModel UserClaims { get; }
    }
}