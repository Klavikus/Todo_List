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

        private TaskData _taskData;

        public TaskService(IProgressRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public event Action<TaskData> TaskCreated;
        public event Action<TaskData> TaskChanged;
        public event Action<DateTime> FocusedDateChanged;
        public event Action TasksChanged;

        public DateTime FocusedDate { get; private set; }
        public TaskData FocusedTask => _taskData;

        public void FocusDate(DateTime dateTime)
        {
            FocusedDate = dateTime.Date;
            FocusedDateChanged?.Invoke(FocusedDate);
        }

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

        public async void CreateTask(string name, string description)
        {
            TaskData taskData = new TaskData(Guid.NewGuid().ToString())
            {
                Name = name,
                Description = description,
                TargetDate = FocusedDate,
                IsCompleted = false
            };
            _repository.Add<TaskData>(taskData);
            await _repository.Save();
            _taskData = taskData;
            TaskCreated?.Invoke(_taskData);
            TaskChanged?.Invoke(_taskData);
            TasksChanged?.Invoke();
        }

        public IEnumerable<TaskData> GetTasks(DateTime dateTime) =>
            _repository
                .GetAll<TaskData>()
                .Where(data => data.TargetDate.Date == dateTime.Date)
                .OrderBy(data => data.IsCompleted);

        public IEnumerable<TaskData> GetFocusedDateTasks() =>
            GetTasks(FocusedDate);

        public IEnumerable<TaskData> GetAllTasks() =>
            _repository.GetAll<TaskData>();

        public IEnumerable<TaskData> GetTodayTasks()
        {
            DateTime dateTimeNow = DateTime.Now;

            return _repository
                .GetAll<TaskData>()
                .Where(data => data.TargetDate.Date == dateTimeNow.Date)
                .OrderBy(data => data.IsCompleted);
        }

        public async void DeleteTask(TaskData taskData)
        {
            _repository.Remove(taskData);
            await _repository.Save();
            TasksChanged?.Invoke();
        }

        public async void DeleteCompletedTasks(DateTime dateTime)
        {
            _repository.Remove<TaskData>((entity) =>
            {
                TaskData taskData = entity as TaskData;

                if (taskData == null)
                    return false;

                return taskData.TargetDate.Date == dateTime.Date && taskData.IsCompleted;
            });

            await _repository.Save();
            TasksChanged?.Invoke();
        }
    }
}