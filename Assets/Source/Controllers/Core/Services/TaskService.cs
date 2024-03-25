using System;
using System.Collections.Generic;
using System.Linq;
using Modules.DAL.Abstract.Repositories;
using Modules.DAL.Implementation.Data;
using Source.Controllers.Api.Services;
using UnityEngine;

namespace Source.Controllers.Core.Services
{
    public class TaskService : ITaskService
    {
        private readonly IProgressRepository _repository;
        private TaskData _taskData;

        public TaskService(IProgressRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public event Action<TaskData> TaskCreated;
        public event Action<TaskData> TaskChanged;
        public event Action<DateTime> FocusedDateChanged;

        public DateTime FocusedDate { get; private set; }
        public TaskData FocusedTask => _taskData;

        public void FocusDate(DateTime dateTime) =>
            FocusedDate = dateTime.Date;

        public void FocusTask(TaskData taskData) =>
            _taskData = taskData;

        public async void CompleteTask()
        {
            _taskData.IsCompleted = true;
            _repository.GetById<TaskData>(_taskData.Id).IsCompleted = true;
            await _repository.Save();
            TaskChanged?.Invoke(_taskData);
            TaskCreated?.Invoke(_taskData);
        }   
        
        public async void ResetTask()
        {
            _taskData.IsCompleted = false;
            _repository.GetById<TaskData>(_taskData.Id).IsCompleted = false;
            await _repository.Save();
            TaskChanged?.Invoke(_taskData);
            TaskCreated?.Invoke(_taskData);
        }

        public IEnumerable<TaskData> GetTodayTasks()
        {
            DateTime dateTimeNow = DateTime.Now;

            return _repository
                .GetAll<TaskData>()
                .Where(data => data.TargetDate.Date == dateTimeNow.Date)
                .OrderBy(data => data.IsCompleted);
        }
    }
}