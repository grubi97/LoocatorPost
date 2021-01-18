using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System;
using System.Text;
using Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Domain;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Application;

namespace API.Handler
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly DataContext _context;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            Microsoft.Extensions.Logging.ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            DataContext context

        ) : base(options, logger, encoder, clock)
        { _context = context; }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var endpoint = Context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
                return AuthenticateResult.NoResult();

            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            Sensor sensor = null;

            try
            {
                var key = "b14ca5898a4e4133bbce2ea2315a1916";
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var id = credentials[0];
                int Id = Int32.Parse(id);
                var password = credentials[1];
                var decryptedString = Security.EncryptString(key, password);
                sensor = await _context.Sensors.Where(s => s.Id == Id && s.Password == password).FirstOrDefaultAsync();

            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            if (sensor == null)
                return AuthenticateResult.Fail("Invalid Username or Password");

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, sensor.Id.ToString()),
                new Claim(ClaimTypes.Name, sensor.Id.ToString()),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);



            throw new System.NotImplementedException();
        }
    }
}