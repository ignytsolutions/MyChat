using MyChat.Shared.Models;
using System.Threading.Tasks;

namespace MyChat.Client.Services {
    public interface IAuthenticationService {
        Task Login(LoginRequest loginRequest);
        Task Register(RegisterRequest registerRequest);
        Task Logout();
        Task<CurrentUser> CurrentUserInfo();
    }
}
