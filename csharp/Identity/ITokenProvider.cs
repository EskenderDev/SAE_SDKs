using System.Threading;
using System.Threading.Tasks;

namespace SAE.Identity.Client;

public interface ITokenProvider
{
    Task<string> GetAccessTokenAsync(CancellationToken ct = default);
}
