using Blazored.SessionStorage;
using GradeSystem.v1.Client.Extencion;
using GradeSystem.v1.Shared;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

public class JwtAuthorizationMessageHandler : DelegatingHandler
{
    private readonly ISessionStorageService _sessionStorageService;

    public JwtAuthorizationMessageHandler(ISessionStorageService sessionStorageService)
    {
        _sessionStorageService = sessionStorageService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var userSession = await _sessionStorageService.ReadEncryptedAsync<UserSession>("UserSession");
        if (userSession != null && DateTime.Now < userSession.ExpiryTimeStamp)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userSession.Token);
        }
        else
        {
            request.Headers.Authorization = null;
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
