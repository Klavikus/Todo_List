using System;
using System.Collections.Generic;
using Modules.DAL.Implementation.Data;
using Modules.DAL.Implementation.Data.Entities;

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
        void CreateTask(string name, string description);
        IEnumerable<TaskData> GetTasks(DateTime dateTime);
        IEnumerable<TaskData> GetFocusedDateTasks();
        void DeleteTask(TaskData taskData);
        void DeleteCompletedTasks(DateTime dateTime);
        event Action TasksChanged;
        IEnumerable<TaskData> GetAllTasks();
    }
}