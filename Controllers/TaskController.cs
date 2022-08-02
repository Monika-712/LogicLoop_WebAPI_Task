using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LogicLoopTask.Repository;
using LogicLoopTask.Converter;

namespace LogicLoopTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        readonly ITaskRepository _taskRepository = null;
        public TaskController(ITaskRepository taskRepository)
        {
            this._taskRepository = taskRepository;
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
            catch (Exception)
            {

                return BadRequest();
            }

        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var data = _taskRepository.GetTaskList().Where(x => x.id == id).FirstOrDefault();
                return Ok(data);
            }
            catch (Exception)
            {

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
            catch (Exception)
            {

                return BadRequest();
            }

        }
        [HttpPut]
        public ActionResult UpdateTask(Models.Task model)
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
                return BadRequest(ex.Message);
            }
            return NotFound();
        }
        [HttpDelete, Route("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                if (id>0)
                {
                    string data = _taskRepository.Delete(id);
                    return Ok(data);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
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
            catch (Exception)
            {

                throw;
            }
        }
    }
}