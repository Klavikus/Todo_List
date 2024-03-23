using System;
using Modules.DAL.Implementation.Data;

namespace Source.Controllers.Api.Services
{
    public interface ITaskService
    {
        event Action<TaskData> TaskCreated;
        event Action<DateTime> FocusedDateChanged;
        DateTime FocusedDate { get; }
        TaskData[] GetTodayTasks();
        void FocusDate(DateTime dateTime);
    }
}