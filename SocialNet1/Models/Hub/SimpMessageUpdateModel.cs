namespace SocialNet1.Models.Hub
{
    public class SimpMessageUpdateModel
    {
        public string SenderName { get; set; }

        public string RecipientName { get; set; }

        public int ChatId { get; set; }

        public int MessageHelpId { get; set; }

        public string Text { get; set; }

    }
}
