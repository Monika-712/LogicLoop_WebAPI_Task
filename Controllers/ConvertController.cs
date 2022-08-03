using System;
using LogicLoopTask.Converter;
using LogicLoopTask.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogicLoopTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvertController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        readonly ITaskRepository _taskRepository = null;

        public ConvertController(ITaskRepository taskRepository, IHttpContextAccessor httpContextAccessor)
        {
            this._taskRepository = taskRepository;
            this._httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("{from}/{amount}/{to}")]
        public ActionResult Get(string from, string amount, string to)
        {
            try
            {
                var data = CurrencyConverter.Converter(amount, from, to);
                if (data == null) return NotFound();
                return Ok(data);
            }
            catch (Exception ex)
            {
                string currentUrl = _httpContextAccessor.HttpContext.Request.Host.Value + _httpContextAccessor.HttpContext.Request.Path.Value + "/Get/" + from + "/" + amount + "/" + to;

                ExceptionLogging.Logs(ex, currentUrl);

                return BadRequest();
            }
        }
    }
}
