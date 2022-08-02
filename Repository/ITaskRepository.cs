using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogicLoopTask.Repository
{
    public interface ITaskRepository
    {
        IEnumerable<Models.Task> GetTaskList();
        bool AddTaskList(Models.Task model);
        string UpdateTask(Models.Task task);
        string Delete(int id);
    }
}
