using System;
using System.Collections.Generic;
using Modules.DAL.Implementation.Data;

namespace Source.Controllers.Api.Services
{
    public interface ITaskService
    {
        event Action<TaskData> TaskCreated;
        event Action<DateTime> FocusedDateChanged;
        event Action<TaskData> TaskChanged;
        DateTime FocusedDate { get; }
        TaskData FocusedTask { get; }
        IEnumerable<TaskData> GetTodayTasks();
        void FocusDate(DateTime dateTime);
        void FocusTask(TaskData taskData);
        void CompleteTask();
        void ResetTask();
    }
}