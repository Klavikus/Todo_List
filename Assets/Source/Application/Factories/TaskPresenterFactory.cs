using Modules.DAL.Implementation.Data;
using Source.Common.WindowFsm;
using Source.Controllers.Api;
using Source.Controllers.Api.Services;
using Source.Controllers.Core.Presenters;
using Source.Presentation.Api;

namespace Source.Application.Factories
{
    public class TaskPresenterFactory : ITaskPresenterFactory
    {
        public IPresenter Create(ICreatedTaskView view, IWindowFsm windowFsm, ILogger logger, ITaskService taskService,
            TaskData taskData) =>
            new TaskPresenter(view, windowFsm, logger, taskService, taskData);
    }
}