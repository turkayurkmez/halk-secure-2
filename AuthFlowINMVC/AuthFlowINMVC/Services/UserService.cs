using AuthFlowINMVC.Models;

namespace AuthFlowINMVC.Services
{
    public class UserService
    {
        private List<User> _users = new()
        {
            new(){ Id=1, Name="turkay", Password="123456", Email="turkay@test.com", Role="admin"},
            new(){ Id=1, Name="gulnur", Password="123456", Email="gulnur@test.com", Role="editor"},
            new(){ Id=1, Name="ferdi", Password="123456", Email="ferdi@test.com", Role="client"},



        };
        public User? ValidateUser(string username, string password)
        {
            return _users.SingleOrDefault(u => u.Name == username && u.Password == password);
        }

    }
}
