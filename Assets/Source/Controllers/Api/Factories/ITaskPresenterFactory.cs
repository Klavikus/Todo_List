using Modules.DAL.Implementation.Data.Entities;
using Modules.MVPPassiveView.Runtime;
using Source.Common.WindowFsm;
using Source.Controllers.Api.Services;

namespace Source.Controllers.Api.Factories
{
    public interface ITaskPresenterFactory<in T>
        where T : IView
    {
        IPresenter Create(
            T view,
            IWindowFsm windowFsm,
            ILogger logger,
            ITaskService taskService,
            TaskData taskData);
    }
}