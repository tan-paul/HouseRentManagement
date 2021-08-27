using HouseRentManagement.Models;
using System.Threading.Tasks;

namespace HouseRentManagement.Repository
{
    public interface IUsersRepository
    {
        Task<string> SignUpAsync(SignUpModel signUp);
        Task<string> SignInAsync(SignInModel signIn);
    }
}