namespace cslabwasmpoc.Client
{
    public interface IClientAuthService
{
        Task<string> RefreshToken();

    }
}
