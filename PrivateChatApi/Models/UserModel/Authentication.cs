#nullable disable
using PrivateChatApi;
using PrivateChatApi.Models.UserModel.Friends;
using PrivateChatApi.Models.UserModel.Friends;
using PrivateChatApi.Models.UserModel.Friends.Profile;

namespace PrivateChatApi.Models.UserModel
{
    public class Authentication
    {
        public int Id { get; set; }
        public string AccountId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string code { get; set; }
        public string PTK { get; set; }
        public Profile Proile { get; set; }
        public List<FriendsList> Friends { get; set; } = new List<FriendsList> { };
        public List<FriendsRequest> FriendsRequests { get; set; } = new List<FriendsRequest> { };
        public List<PendingFriendsRequest> PendingFriends { get; set; } = new List<PendingFriendsRequest> { };
        public Authentication()
        {
        }



    }
}
