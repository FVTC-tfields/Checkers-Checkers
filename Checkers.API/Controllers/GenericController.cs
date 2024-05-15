using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using Checkers.BL;
using Checkers.PL.Data;

namespace Checkers.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public class GenericController<T, U> : ControllerBase
    {
        protected DbContextOptions<CheckersEntities> options;
        protected readonly ILogger logger;
        dynamic manager;
        //protected IUserService _userService;

        public GenericController(ILogger logger,
                                 DbContextOptions<CheckersEntities> options)
        {
            this.options = options;
            this.logger = logger;
            manager = (U)Activator.CreateInstance(typeof(U), logger, options);
        }

        //public GenericController(IUserService userService,
        //                  ILogger logger,
        //                  DbContextOptions<CheckersEntities> options)
        //{
        //    this._userService = userService;
        //    this.options = options;
        //    this.logger = logger;
        //    manager = (U)Activator.CreateInstance(typeof(U), logger, options);
        //}

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<T>>> Get()
        {
            try
            {
                return Ok(await manager.LoadAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<T>> Get(Guid id)
        {
            try
            {
                return Ok(await manager.LoadByIdAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("{rollback?}")]
        public async Task<ActionResult> Post([FromBody] T entity, bool rollback = false)
        {
            try
            {
                Guid id = await manager.InsertAsync(entity, rollback);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}/{rollback?}")]
        public int Put(Guid id, [FromBody] Game format, bool rollback = false)
        {
            try
            {
                return new GameManager(options).Update(format, rollback);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id}/{rollback?}")]
        public int Delete(Guid id, bool rollback = false)
        {
            try
            {
                return new GameManager(options).Delete(id, rollback);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
