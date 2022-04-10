namespace SocialNet1.Models.API
{
    public class AddCommentImageModel
    {
        public string Text { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public int ImageId { get; set; }
    }
}
