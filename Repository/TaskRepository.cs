using System;
using System.Collections.Generic;
using System.Linq;
//using System.Threading.Tasks;
using LogicLoopTask.Models;

namespace LogicLoopTask.Repository
{
    public class TaskRepository:ITaskRepository
    {
        readonly TaskContext _taskContext = null;
        public TaskRepository(TaskContext context)
        {
            _taskContext = context;
        }
        public IEnumerable<Task> GetTaskList()
        {
           return _taskContext.tasks.ToList();
        }

        public bool AddTaskList(Task model)
        {
             _taskContext.tasks.Add(model);
           int result = _taskContext.SaveChanges();
            if(result > 0)
            {
                return true;
            }
            return false;
        }
        public string UpdateTask(Task task)
        {
             _taskContext.tasks.Update(task);
            int result = _taskContext.SaveChanges();
           if(result > 0)
            {
                return "Task details updated.";
            }
            return "Task details update failed.";
        }
        public string Delete(int id)
        {
            var data = _taskContext.tasks.Find(id);
            _taskContext.tasks.Remove(data);
            int result = _taskContext.SaveChanges();
            if (result > 0)
            {
                return "record deleted";
            }
            return "record not deleted";
        }
    }
}
