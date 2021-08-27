using Microsoft.AspNetCore.Identity;

namespace HouseRentManagement.Models
{
    public class UserAccountModel : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
