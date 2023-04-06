using PrivateChatApi.Models.UserModel.Friends.Chat;

namespace PrivateChatApi.Models.UserModel.Friends
{
    public class FriendsList
    {
        public string AccountID { get; set; }
        public string Name { get; set; }
        public FriendChat FriendChats { get; set; }

    }
}
