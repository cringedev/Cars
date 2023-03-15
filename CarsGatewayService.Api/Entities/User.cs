namespace Cars.Api.Entities;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
}