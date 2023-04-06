using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrivateChatApi.Models.UserModel;
using PrivateChatApi.Models.UserModel.Friends;
using PrivateChatApi.Models.UserModel.Friends.Chat;
using PrivateChatApi.Models.Auth;

namespace PrivateChatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        [HttpGet("{UserId}")]
        public IActionResult GetProfile(long UserId)
        {
            var GetProfile = LocalDb.users.FirstOrDefault(x => x.AccountId == UserId.ToString());
            if (GetProfile == null) return BadRequest("Profile Could Not be Found");
            else
                return Ok(GetProfile.Proile);
        }
        [HttpPut("{PKT}")]
        public IActionResult EditProile(string PKT, Registration account)
        {
            if (PKT != null)
            {
                var Profile = LocalDb.users.FirstOrDefault(x => x.PTK == PKT);
                if (Profile == null) return BadRequest("Invaild Profile");
                else
                {
                    LocalDb.users[LocalDb.users.IndexOf(Profile)].email = account.email;
                    LocalDb.users[LocalDb.users.IndexOf(Profile)].password = account.password;
                    LocalDb.users[LocalDb.users.IndexOf(Profile)].username = account.username;
                    return Ok(Profile.Proile);
                }


            }
            return BadRequest("Invaild PKT");
        }


    }
}
