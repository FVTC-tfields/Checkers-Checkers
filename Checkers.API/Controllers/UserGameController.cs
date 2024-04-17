namespace Checkers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGameController : GenericController<UserGame, UserGameManager>
    {
        public UserGameController(ILogger<UserGameController> logger,
                                DbContextOptions<CheckersEntities> options) : base(logger, options)
        {
        }

    }
}
