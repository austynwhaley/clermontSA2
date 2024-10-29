namespace clermontSA2.Models
{
    // model for pagination
    public class UserListViewModel
    {
        public required IEnumerable<User> Users { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? SearchTerm { get; set; }
    }
}
