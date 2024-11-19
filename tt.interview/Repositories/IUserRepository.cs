namespace tt.interview.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUser(string username, string password);
    }
}
