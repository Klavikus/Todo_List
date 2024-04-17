using Modules.DAL.Implementation.Data.Entities;
using Modules.MVPPassiveView.Runtime;
using Source.Common.WindowFsm;
using Source.Controllers.Api.Factories;
using Source.Controllers.Api.Services;
using Source.Controllers.Core.Presenters;
using Source.Presentation.Api.Views;

namespace Source.Controllers.Core.Factories
{
    public class TaskPresenterFactory : ITaskPresenterFactory<ICreatedTaskView>
    {
        public IPresenter Create(
            ICreatedTaskView view,
            IWindowFsm windowFsm,
            ILogger logger,
            ITaskService taskService,
            TaskData taskData)
            => new TaskPresenter(view, windowFsm, logger, taskService, taskData);
    }
}