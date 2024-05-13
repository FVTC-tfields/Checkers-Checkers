namespace Checkers.UI.Final.Controllers
{
    public class GameController : GenericController<Game>
    {
        public GameController(HttpClient httpClient) : base(httpClient)
        {
        }
    }
}
