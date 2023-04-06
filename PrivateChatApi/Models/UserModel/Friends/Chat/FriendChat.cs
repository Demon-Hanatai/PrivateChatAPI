#nullable disable
using PrivateChatApi;

namespace PrivateChatApi.Models.UserModel.Friends.Chat
{
    public class FriendChat
    {
        public int Id { get; set; }
        public ulong ChatId { get; set; }
        public string username { get; set; }
        public int UserId { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>();


    }
}
