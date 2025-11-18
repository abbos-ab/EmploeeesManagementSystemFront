using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using EmployesManagementSystemFront.Providers;
using EmployesManagementSystemFront.Services;
using EmployesManagementSystemFront.Authorization;

namespace EmployesManagementSystemFront
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5203/")
            });

            // Add Blazored LocalStorage
            builder.Services.AddBlazoredLocalStorage();

            // Add Authentication and Authorization services
            builder.Services.AddAuthorizationCore(options =>
            {
                // Define role-based policies
                options.AddPolicy(Policies.RequireSuperAdminRole, policy => 
                    policy.RequireRole(Roles.SuperAdmin));
                
                options.AddPolicy(Policies.RequireAdminRole, policy => 
                    policy.RequireRole(Roles.Admin, Roles.SuperAdmin));
                
                options.AddPolicy(Policies.RequireUserRole, policy => 
                    policy.RequireRole(Roles.User, Roles.Admin, Roles.SuperAdmin));
            });
            
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            
            // Add custom services
            builder.Services.AddScoped<IAuthService, AuthService>();

            await builder.Build().RunAsync();
        }
    }
}
