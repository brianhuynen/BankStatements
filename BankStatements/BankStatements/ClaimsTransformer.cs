using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Security.Claims;

namespace BankStatements
{
    /// <summary>
    /// This class is not in use but could show how to secure the API such that only roles with value 'editor' can access the API.
    /// </summary>
    public class ClaimsTransformer : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            List<string> roleList = new()
            {
                "editor"
            };

            ClaimsIdentity claimsIdentity = (ClaimsIdentity)principal.Identity;

            if (claimsIdentity.IsAuthenticated && claimsIdentity.HasClaim((claim) => claim.Type == "balance-role"))
            {
                var realmAccessClaim = claimsIdentity.FindFirst((claim) => claim.Type == "balance-role");
                var realmAccessAsDict = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(realmAccessClaim.Value);
                if (realmAccessAsDict["roles"] != null)
                {
                    foreach (var role in realmAccessAsDict["roles"])
                    {
                        if (roleList.Contains(role))
                        {
                            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
                            Console.WriteLine(role);
                        }
                    }
                }
            }
            return Task.FromResult(principal);
        }
    }
}
