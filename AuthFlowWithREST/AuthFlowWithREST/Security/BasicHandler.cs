using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace AuthFlowWithREST.Security
{
    public class BasicHandler : AuthenticationHandler<BasicOptions>
    {
        public BasicHandler(IOptionsMonitor<BasicOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            /*
             * headers: {
             *    Authorization: 'Basic dHVya2F5OjEyMzQ1Ng=='
             *                          [turkay:123456]
             * }
             * 
             */

            /*
             * 1. Gelen http request'in header'inde Authorization var mı?
             * 2. Varsa Authorization formatı doğru mu?
             * 3. Authorization Scheme adı Basic mi?
             * 4. Base64String'e decode et.
             * 5. (:)'e göre ayır.
             * 
             * 
             *  
             */

            //1. Gelen http request'in header'inde Authorization var mı?
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            //2. Varsa Authorization formatı doğru mu?
            if (!AuthenticationHeaderValue.TryParse(Request.Headers["Authorization"], out AuthenticationHeaderValue parsedValue))
            {
                return Task.FromResult(AuthenticateResult.NoResult());

            }

            //3. Authorization Scheme adı Basic mi?
            if (!parsedValue.Scheme.Equals("Basic", StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(AuthenticateResult.NoResult());

            }

            // Base64String'e decode et.

            var bytes = Convert.FromBase64String(parsedValue.Parameter);
            string headerValue = Encoding.UTF8.GetString(bytes);

            string name = headerValue.Split(':')[0];
            string pass = headerValue.Split(":")[1];

            if (name == "turkay" && pass == "123")
            {
                var claims = new Claim[]
                {
                  new Claim(ClaimTypes.Name,name)

                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, Scheme.Name);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                AuthenticationTicket ticket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);
                return Task.FromResult(AuthenticateResult.Success(ticket));

            }
            return Task.FromResult(AuthenticateResult.Fail("Hatalı giriş!"));


        }
    }
}
