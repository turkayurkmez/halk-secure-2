namespace JWTWithRest.Services
{

    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
    public class UserService
    {
        private List<User> _users = new List<User>()
        {
            new User { Id = 1, UserName="admin", Password="123456", Role="admin" },
            new User { Id = 2, UserName="acakir", Password="123456", Role="client" },
            new User { Id = 3, UserName="busrak", Password="123456", Role="bd" }
        };


        public User ValidateUser(string username, string password) => _users.SingleOrDefault(u => u.UserName == username && u.Password == password);




    }
}
