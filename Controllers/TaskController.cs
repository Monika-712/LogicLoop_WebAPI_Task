using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LogicLoopTask.Repository;

namespace LogicLoopTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        readonly ITaskRepository _taskRepository = null;

        public TaskController(ITaskRepository taskRepository, IHttpContextAccessor httpContextAccessor)
        {
            this._taskRepository = taskRepository;
            this._httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public ActionResult GetList()
        {
            try
            {
                var data = _taskRepository.GetTaskList().ToList();
                if (data == null) return NotFound();
                return Ok(data);

            }
            catch (Exception ex)
            {
                string currentUrl = _httpContextAccessor.HttpContext.Request.Host.Value + _httpContextAccessor.HttpContext.Request.Path.Value + "/GetList";

                ExceptionLogging.Logs(ex, currentUrl);

                return BadRequest();
            }
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                int i = int.Parse("ABV");
                var data = _taskRepository.GetTaskList().Where(x => x.id == id).FirstOrDefault();
                return Ok(data);
            }
            catch (Exception ex)
            {
                string currentUrl = _httpContextAccessor.HttpContext.Request.Host.Value + _httpContextAccessor.HttpContext.Request.Path.Value + "/Get/" + id;

                ExceptionLogging.Logs(ex, currentUrl);

                return BadRequest();
            }

        }
        [HttpPost]
        public ActionResult Post(Models.Task model)
        {
            try
            {
                var data = _taskRepository.AddTaskList(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                string currentUrl = _httpContextAccessor.HttpContext.Request.Host.Value + _httpContextAccessor.HttpContext.Request.Path.Value + "/Post";

                ExceptionLogging.Logs(ex, currentUrl);

                return BadRequest();
            }

        }
        [HttpPut]
        public ActionResult Put(Models.Task model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string data = _taskRepository.UpdateTask(model);
                    return Ok(data);
                }
            }
            catch (Exception ex)
            {
                string currentUrl = _httpContextAccessor.HttpContext.Request.Host.Value + _httpContextAccessor.HttpContext.Request.Path.Value + "/Put";

                ExceptionLogging.Logs(ex, currentUrl);

                return BadRequest();
            }
            return NotFound();
        }
        [HttpDelete, Route("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                if (id > 0)
                {
                    string data = _taskRepository.Delete(id);
                    return Ok(data);
                }
            }
            catch (Exception ex)
            {
                string currentUrl = _httpContextAccessor.HttpContext.Request.Host.Value + _httpContextAccessor.HttpContext.Request.Path.Value + "/Delete/ " + id;

                ExceptionLogging.Logs(ex, currentUrl);

                return BadRequest();
            }
            return NotFound();
        }
    }
}