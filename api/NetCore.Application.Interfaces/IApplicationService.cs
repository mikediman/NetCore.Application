using NetCore.Application.Types;
using System.Threading.Tasks;

namespace NetCore.Application.Interfaces
{
    public interface IApplicationService
    {
        Task<UserRegistrationResponse> RegistrationUserAsync(UserRegistrationRequest request);
    }
}
