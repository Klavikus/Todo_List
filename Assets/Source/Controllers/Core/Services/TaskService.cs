using System;
using System.Collections.Generic;
using System.Linq;
using Modules.DAL.Abstract.Repositories;
using Modules.DAL.Implementation.Data;
using Source.Controllers.Api.Services;

namespace Source.Controllers.Core.Services
{
    public class TaskService : ITaskService
    {
        private readonly IProgressRepository _repository;

        public TaskService(IProgressRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public event Action<TaskData> TaskCreated;
        public event Action<DateTime> FocusedDateChanged;

        public DateTime FocusedDate { get; private set; }

        public void FocusDate(DateTime dateTime) =>
            FocusedDate = dateTime.Date;

        public IEnumerable<TaskData> GetTodayTasks()
        {
            DateTime dateTimeNow = DateTime.Now;

            return _repository.GetAll<TaskData>().Where(data => data.TargetDate.Date == dateTimeNow.Date);
        }
    }
}