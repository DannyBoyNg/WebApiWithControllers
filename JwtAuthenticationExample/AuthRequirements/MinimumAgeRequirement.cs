using Microsoft.AspNetCore.Authorization;

namespace JwtAuthenticationExample.AuthRequirements
{
    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
        public MinimumAgeRequirement(int minimumAge) =>
        MinimumAge = minimumAge;

        public int MinimumAge { get; }
    }
}
