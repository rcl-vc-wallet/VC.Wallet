#nullable disable

using System.Security.Claims;

namespace VC.Wallet.WebApp
{
    public static class AdminService
    {
        public static IConfiguration config;

        public static bool IsAdmin(ClaimsPrincipal user)
        {
            try
            {
                string AdminUsername = config?.GetSection("Configuration:AdminUsername")?.Value ?? string.Empty;

                if ((user?.Identity?.Name == AdminUsername) && (AdminUsername != string.Empty))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
