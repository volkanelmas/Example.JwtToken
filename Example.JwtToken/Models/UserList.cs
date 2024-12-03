namespace Example.JwtToken.Models
{
    public class UserList
    {
        public static List<User> AllUsers = new()
        {
            new User() {
                Id = 1,
                UserName="Volkan",
                Password="123456",
                Role="Yönetici"
            },
            new User() {
                Id = 2,
                UserName="Ahmet",
                Password="456789",
                Role="Standart"
            }
        };
    }
}
