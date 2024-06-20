using Blazored.SessionStorage;
using GradeSystem.v1.Client.Extencion;
using GradeSystem.v1.Shared;
using System.Security.Claims;

namespace GradeSystem.v1.Client.Auth
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorageService;
        private ClaimsPrincipal _anonymus = new ClaimsPrincipal(new ClaimsPrincipal());
        public CustomAuthenticationStateProvider(ISessionStorageService sessionStorageService)
        {
            _sessionStorageService = sessionStorageService;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSession = await _sessionStorageService.ReadEncryptedAsync<UserSession>("UserSession");
                if (userSession == null)
                    return await Task.FromResult(new AuthenticationState(_anonymus));
                var claims = new List<Claim>();
                foreach (var role in userSession.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                claims.Add(new Claim(ClaimTypes.Name, userSession.UserName));
                claims.Add(new Claim(ClaimTypes.Email, userSession.UserID.ToString()));
                claims.Add(new Claim(ClaimTypes.Gender, userSession.ID.ToString()));
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "JwtAuth"));

                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymus));
            }
        }
        public async Task UpdateAuthenticationState(UserSession? userSession)
        {
            ClaimsPrincipal claimsPrincipal;
            if (userSession != null)
            {
                var claims = new List<Claim>();
                foreach (var role in userSession.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                claims.Add(new Claim(ClaimTypes.Name, userSession.UserName));
                claims.Add(new Claim(ClaimTypes.Email, userSession.UserID.ToString()));
                claims.Add(new Claim(ClaimTypes.Gender, userSession.ID.ToString()));
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims));
                userSession.ExpiryTimeStamp = DateTime.Now.AddSeconds(userSession.ExpiresIn);
                await _sessionStorageService.SaveItemEncryptedAsync("UserSession", userSession);

            }
            else
            {
                claimsPrincipal = _anonymus;
                await _sessionStorageService.RemoveItemAsync("UserSession");
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));

        }
        public async Task<string> GetToken()
        {
            var result = string.Empty;
            try
            {
                var userSession = await _sessionStorageService.ReadEncryptedAsync<UserSession>("UserSession");
                if (userSession != null && DateTime.Now < userSession.ExpiryTimeStamp)
                    result = userSession.Token;
            }
            catch
            {

            }
            return result;
        }
    }
}
