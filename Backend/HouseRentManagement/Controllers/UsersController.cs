using HouseRentManagement.Models;
using HouseRentManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HouseRentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        [HttpPost("signup")]
        public async Task<IActionResult> SignUpAsync([FromBody]SignUpModel signUp) {
            var result = await _usersRepository.SignUpAsync(signUp);
            if (!string.IsNullOrEmpty(result)){
                return Ok(result);
            }
            return Unauthorized();
        }

        [HttpPost("login")]
        public async Task<IActionResult> SignInAsync([FromBody] SignInModel signIn)
        {
            var result = await _usersRepository.SignInAsync(signIn);
            if (!string.IsNullOrEmpty(result)){
                return Ok(result);
            }
            return Unauthorized();
        }
    }
}
