using Modules.DAL.Abstract.Repositories;
using Source.Common.WindowFsm;
using Source.Controllers.Api.Services;
using Source.Controllers.Core.Presenters;
using Source.Presentation.Core;
using UnityEngine;
using Zenject;
using ILogger = Source.Controllers.Api.Services.ILogger;

namespace Source.Application.CompositionRoots
{
    public class MainMenuCompositionRoot : MonoBehaviour
    {
        [Inject]
        public async void Initialize(
            IProgressRepository repository,
            MainMenuView mainMenuView,
            MainTaskListView mainTaskListView,
            MainTaskListPresenter mainTaskListPresenter,
            TaskCreationView taskCreationView,
            TaskCreationPresenter taskCreationPresenter,
            TaskView taskView,
            TaskViewPresenter taskViewPresenter)
        {
            await repository.Load();
        }
    }
}