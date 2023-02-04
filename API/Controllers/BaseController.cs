using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        internal string UserId => !User.Identity.IsAuthenticated
          ? string.Empty
          : User.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
