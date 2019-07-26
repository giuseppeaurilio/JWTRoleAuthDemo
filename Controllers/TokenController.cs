using JwtTokenDemo.Middlewares;
using JwtTokenDemo.Model.Requests;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JwtTokenDemo.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        // POST api/Token
        [HttpPost]
        public IActionResult GetToken([FromBody] TokenRequest tokenRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!AuthorizedUser.VerifyCredentials(tokenRequest.Username, tokenRequest.Password))
            {
                return Unauthorized();
            }

            //L'utente ha fornito credenziali valide
            //creiamo per lui una ClaimsIdentity
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            //Aggiungiamo uno o più claim relativi all'utente loggato
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, tokenRequest.Username));

            //Incapsuliamo l'identità in una ClaimsPrincipal e associamola alla richiesta corrente
            HttpContext.User = new ClaimsPrincipal(identity);

            //Non restituiamo nulla. Il token verrà prodotto dal JwtTokenMiddleware
            return NoContent();
        }
    }
}
