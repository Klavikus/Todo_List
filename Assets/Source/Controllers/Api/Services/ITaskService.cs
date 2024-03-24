using System;
using System.Collections.Generic;
using Modules.DAL.Implementation.Data;

namespace Source.Controllers.Api.Services
{
    public interface ITaskService
    {
        event Action<TaskData> TaskCreated;
        event Action<DateTime> FocusedDateChanged;
        DateTime FocusedDate { get; }
        IEnumerable<TaskData> GetTodayTasks();
        void FocusDate(DateTime dateTime);
    }
}