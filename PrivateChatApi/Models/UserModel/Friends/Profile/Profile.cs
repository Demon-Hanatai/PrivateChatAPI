namespace PrivateChatApi.Models.UserModel.Friends.Profile
{
    public class Profile
    {
        public ulong UserId { get; set; }
        public string Username { get; set; }
        public long Total_Friends { get; set; }
        public long Total_Following { get; set; }
        public long Total_Followers { get; set; }
        public long Total_Followed { get; set; }

    }
}
