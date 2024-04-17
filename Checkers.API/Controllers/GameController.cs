namespace Checkers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : GenericController<Game, GameManager>
    {
        public GameController(ILogger<GameController> logger,
                                DbContextOptions<CheckersEntities> options) : base(logger, options)
        {
        }

    }
}
