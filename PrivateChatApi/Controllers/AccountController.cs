using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrivateChatApi.Models.UserModel;
using PrivateChatApi.Models.UserModel.Friends.Profile;
using PrivateChatApi.Aside;
using PrivateChatApi.Models.Auth;

namespace PrivateChatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("[action]")]
        public IActionResult Registration([FromQuery] Registration registration)
        {
            if (registration.username == null) return BadRequest("Username can't be Empty.");
            if (registration.password == null) return BadRequest("Password can't be Empty");
            if (registration.email == null) return BadRequest("Email Can't be null");
            if (string.IsNullOrWhiteSpace(registration.email)) return BadRequest("Invaild Email");
            if (string.IsNullOrWhiteSpace(registration.password)) return BadRequest("Password can't have a white");
            if (LocalDb.users.Any(x => x.username == registration.username)) return BadRequest("Username as be taken");
            if (LocalDb.users.Any(x => x.email == registration.email)) return BadRequest("emails as be used");
            else
            {
                Random random = new Random();
                string AccountId = random.Next(100000000, int.MaxValue).ToString();
                LocalDb.users.Add(new Authentication()
                {
                    AccountId = AccountId,
                    PTK = Token.Create(),
                    email = registration.email,
                    password = registration.password,
                    username = registration.username,
                    phone = registration.phone,
                    Id = random.Next(100000, 9999999),
                    Proile = new Profile()
                    {
                        Username = registration.username,
                        UserId = (ulong)Convert.ToDecimal(AccountId),
                        Total_Followed = 0,
                        Total_Followers = 0,
                        Total_Friends = 0,
                        Total_Following = 0,

                    }
                });

                return Ok(registration);
            }

        }
        [HttpGet]
        public IActionResult Login(string username, string password)
        {
            if (username is null) return BadRequest("username can't be Empty");
            if (password is null) return BadRequest("password can't be Empty");
            else
            {
                var GetUser = LocalDb.users.FirstOrDefault(x => x.username == username && x.password == password);
                if (GetUser == null) return BadRequest("Username or Password is invaild");
                else
                {
                    return Ok(GetUser);
                }
            }
        }
    }

}
