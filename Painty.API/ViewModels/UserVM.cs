namespace Painty.API.ViewModels
{
    public class UserVM
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public List<UserVM>? Friends { get; set; }
        public List<UserVM>? FriendRequests { get; set; }
        public List<ImageVM>? Images { get; set; }
    }
}
