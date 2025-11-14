using Microsoft.JSInterop;
using System.Net.Http.Headers;

namespace EmployesManagementSystemFront.Helpers
{
    public class AuthHttpClientHandler : DelegatingHandler
    {
        private readonly IJSRuntime _js;

        public AuthHttpClientHandler(IJSRuntime js)
        {
            _js = js;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _js.InvokeAsync<string>("localStorage.getItem", "token");

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
