namespace Checkers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameStateController : GenericController<GameState, GameStateManager>
    {
        public GameStateController(ILogger<GameStateController> logger,
                                DbContextOptions<CheckersEntities> options) : base(logger, options)
        {
        }

    }
}
