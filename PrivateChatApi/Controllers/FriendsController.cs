using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrivateChatApi.Models.UserModel.Friends;
using PrivateChatApi.Models.UserModel.Friends.Chat;

namespace PrivateChatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        [HttpGet("[action]")]
        public IActionResult GetFriends(string PKT)
        {
            var user = LocalDb.users.FirstOrDefault(x => x.PTK == PKT) ?? null;
            if (user == null) return Unauthorized("Unauthorized");

            return Ok(user.Friends);
        }
        [HttpGet("[action]")]
        public IActionResult GetPendingFriendRequests(string PKT)
        {
            var user = LocalDb.users.FirstOrDefault(x => x.PTK == PKT) ?? null;
            if (user == null) return Unauthorized("Unauthorized");

            return Ok(user.PendingFriends);
        }
        [HttpGet("[action]")]
        public IActionResult GetFriendsRequest(string PKT)
        {
            var user = LocalDb.users.FirstOrDefault(x => x.PTK == PKT) ?? null;
            if (user == null) return Unauthorized("Unauthorized");

            return Ok(user.FriendsRequests);
        }
        [HttpPut("[action]")]
        public IActionResult FriendAccptation(string PKT, string username, string code)
        {
            var user = LocalDb.users.FirstOrDefault(x => x.PTK == PKT) ?? null;
            if (user == null) return Unauthorized("Unauthorized");

            else if (user.PendingFriends.Any(x => x.Name == username))
            {
                var Friend = LocalDb.users.FirstOrDefault(x => x.username == username);
                if (code == "0x300")
                {
                    int id = new Random().Next(1000000, int.MaxValue);
                    ulong chatid = (ulong)new Random().Next(1000000, int.MaxValue);
                    user.Friends.Add(new FriendsList()
                    {
                        AccountID = Friend.AccountId,
                        FriendChats = new FriendChat()
                        {
                            Id = id,
                            ChatId = chatid,
                            username = Friend.username,
                            UserId = Friend.Id
                        }
                    });
                    Friend.Friends.Add(new FriendsList()
                    {
                        AccountID = user.AccountId,
                        FriendChats = new FriendChat()
                        {
                            Id = id,
                            ChatId = chatid,
                            username = user.username,
                            UserId = user.Id
                        }
                    });

                    return Ok("Friend added");
                }
                if (code == "0x400")
                {
                    user.PendingFriends.Remove(user.PendingFriends.First(x => x.Name == username));
                    return Ok("Friend Request as be removed");
                }
            }
            else
                return BadRequest("this user could not be found in your friends pending list");
            return BadRequest();
        }
        [HttpPut("[action]")]
        public IActionResult SendFriendRequest(string PKT, long UserId)
        {
            var user = LocalDb.users.FirstOrDefault(x => x.PTK == PKT);
            if (user == null) return BadRequest("Error with account.");
            else
            {
                var Friend = LocalDb.users.FirstOrDefault(x => x.AccountId == UserId.ToString());
                if (Friend == null) return BadRequest("Invaild user account.");
                else
                {
                    if (Friend.FriendsRequests.Any(x => x.FriendId == user.AccountId))
                        return Ok("You already sent a friend request to this user.");
                    if (Friend.Friends.Any(x => x.AccountID == user.AccountId))
                        return Ok("You are already friends with this user");

                    else
                    {
                        Friend.PendingFriends.Add(new PendingFriendsRequest()
                        {

                            Name = user.username,
                            FriendId = user.AccountId,
                            DateTime = DateTime.Now
                        });
                        user.FriendsRequests.Add(new FriendsRequest()
                        {
                            Name = Friend.username,
                            FriendId = Friend.AccountId,
                            DateTime = DateTime.Now
                        });
                        return Ok("Friend Request as be sent");
                    }



                }
            }

        }
    }
}
