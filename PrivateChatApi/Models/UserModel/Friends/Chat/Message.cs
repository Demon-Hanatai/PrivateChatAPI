namespace PrivateChatApi.Models.UserModel.Friends.Chat
{
    public class Message
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string MessageText { get; set; }
        public DateTime Date { get; set; }

    }
}
