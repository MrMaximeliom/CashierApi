using CashierApi.Models;

namespace CashierApi.Services
{
    public interface IAuthService
    {
        Task<Auth> RegisterAsync(RegisterCustomer model);

        Task<Auth> GetTokenAsync(RequestToken model);

        Task<string> AddUserToRoleAsync(AddRole model);

        Task<Auth> RefreshTokenAsync(string token);

        Task<bool> RevokeTokenAsync(string token);
    }
}
