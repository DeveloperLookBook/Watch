namespace Watch.Models.Users
{
    public interface ICredentials
    {
        string Login { get; set; }
        string Password { get; set; }
    }
}