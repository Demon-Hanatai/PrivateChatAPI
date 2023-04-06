using Microsoft.AspNetCore.Mvc;
using PrivateChatApi.Models.UserModel.Friends.Chat;
using System.Runtime.ConstrainedExecution;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PrivateChatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        // GET: api/<MessageController>
        [HttpGet]
        public IActionResult GetMessages(string PKT, ulong chatId)
        {
            var user = LocalDb.users.FirstOrDefault(x => x.PTK == PKT);
            if (user == null) return Unauthorized("Unauthorized");
            else
            {
                if (user.Friends.Any(x => x.FriendChats.ChatId == chatId))
                {
                    return Ok(user.Friends.FirstOrDefault(x => x.FriendChats.ChatId == chatId).FriendChats);

                }
                else
                    return BadRequest("Error PTK#");
            }
            return Ok();
        }

        // GET api/<MessageController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteMessages([FromHeader] string PKT, ulong id, int MessageId)
        {
            var user = LocalDb.users.FirstOrDefault(x => x.PTK == PKT);
            if (user == null) return Unauthorized("Unauthorized");
            else
            {
                var Chat = user.Friends.FirstOrDefault(x => x.FriendChats.ChatId == id).FriendChats;
                if (Chat == null) return BadRequest(Chat);
                else
                {
                    Chat.Messages.Remove(Chat.Messages.FirstOrDefault(x => x.Id == MessageId));
                    return Ok();
                }
            }

        }

        // POST api/<MessageController>
        [HttpPost("{chatId}")]
        public IActionResult SendMessage([FromHeader] string PKT, [FromBody] string message, ulong chatId)
        {
            var user = LocalDb.users.FirstOrDefault(x => x.PTK == PKT);
            if (user == null) return Unauthorized("Unauthorized");
            else
            {
                var chat = user.Friends.FirstOrDefault(x => x.FriendChats.ChatId == chatId);
                if (chat == null) return BadRequest("Could not find this chat");
                else
                {
                    var get_Chats = LocalDb.users.Where(x => x.Friends.FirstOrDefault(x => x.FriendChats.ChatId == chatId).FriendChats.ChatId == chatId);
                    if (get_Chats == null) return BadRequest();
                    else
                    {
                        foreach (var gc in get_Chats)
                        {
                            gc.Friends.FirstOrDefault(x => x.FriendChats.ChatId == chatId).FriendChats.Messages.Add(new Message()
                            {
                                Id = new Random().Next(100000, 999999),
                                MessageText = message,
                                Date = DateTime.Now,
                                From = user.username
                            });
                        }
                    }
                    return Ok();
                }
            }
        }

        // PUT api/<MessageController>/5
        [HttpPut("{id}")]
        public IActionResult EditMessage([FromHeader] string PKT, [FromBody] string message, ulong chatId, int MessageId)
        {
            var user = LocalDb.users.FirstOrDefault(x => x.PTK == PKT);
            if (user == null) return Unauthorized("Unauthorized");
            else
            {
                var chat = user.Friends.FirstOrDefault(x => x.FriendChats.ChatId == chatId);
                if (chat == null) return BadRequest("Could not find this chat");
                else
                {

                    user.Friends.FirstOrDefault(x => x.FriendChats.ChatId == chatId).FriendChats.Messages.
                        FirstOrDefault(x => x.Id == MessageId).MessageText = message;
                    return Ok();
                }
            }
        }


    }
}
