using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Checkers.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private IUserService _userService;
    private readonly ILogger<GameController> logger;
    private readonly DbContextOptions<CheckersEntities> options;

    //public UserController(IUserService userService,
    //                      ILogger<UserController> logger,
    //                      DbContextOptions<CheckersEntities> options) : base(logger, options)
    //{

    //}

    public UserController(IUserService userService,
                          ILogger<GameController> logger,
                          DbContextOptions<CheckersEntities> options)
    {
        this._userService = userService;
        this.options = options;
        this.logger = logger;
    }

    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        var response = _userService.Authenticate(model);

        if (response == null)
        {
            logger.LogWarning("Authentication unsuccessful for {UserId}", model.Username);
            return BadRequest(new { message = "Username or password is incorrect" });
        }
        logger.LogWarning("Authentication successful for {UserId}", model.Username);
        return Ok(response);
    }

    [Authorize]
    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }

    [Authorize]
    [HttpGet]

    public IEnumerable<User> Get()
    {
        logger.LogWarning("Get Users");
        return new UserManager(options).Load();
    }

    //[HttpPost]
    //public IActionResult Post([FromBody] T entity, bool rollback = false)
    //{
    //    logger.LogWarning("Post Users");
    //    var user = new User();
    //    user.FirstName
    //    Guid id = await UserManager.InsertAsync(user, rollback);

    //    return Ok(id);
    //}

    [HttpPost]
    public IActionResult Create(User user)
    {
        try
        {
            int result = new UserManager(options).Insert(user);
            //ViewBag.Title = "Create User";
            //TempData["info"] = result + " user added.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            //ViewBag.Title = "Create User";
            //ViewBag.Error = ex.Message;
            return Ok(user);
        }
    }
}
