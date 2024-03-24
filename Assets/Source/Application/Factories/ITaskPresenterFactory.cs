using Source.Common.WindowFsm;
using Source.Controllers.Api;
using Source.Controllers.Api.Services;
using Source.Presentation.Api;

namespace Source.Application.Factories
{
    public interface ITaskPresenterFactory
    {
        IPresenter Create(ICreatedTaskView view, IWindowFsm windowFsm, ILogger logger, ITaskService taskService);
    }
}