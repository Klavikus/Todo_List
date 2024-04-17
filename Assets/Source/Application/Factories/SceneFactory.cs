using Modules.DAL.Abstract.Repositories;
using Modules.MVPPassiveView.Runtime;
using Source.Controllers.Core.Presenters;
using Source.Presentation.Api.Views;
using VContainer;

namespace Source.Application.Factories
{
    public class SceneFactory : ISceneFactory
    {
        private readonly IObjectResolver _container;

        public SceneFactory(IObjectResolver container)
        {
            _container = container;
        }

        public async void CreateMainScene()
        {
            IProgressRepository repository = _container.Resolve<IProgressRepository>();
            await repository.Load();

            ConstructView<IMainMenuView, MainMenuPresenter>();
            ConstructView<IMainTaskListView, MainTaskListPresenter>();
            ConstructView<ITaskCreationView, TaskCreationPresenter>();
            ConstructView<ITaskView, TaskViewPresenter>();
        }

        private void ConstructView<TView, TPresenter>()
            where TView : IView
            where TPresenter : IPresenter
        {
            TView mainMenuView = _container.Resolve<TView>();
            TPresenter mainMenuPresenter = _container.Resolve<TPresenter>();
            mainMenuView.Construct(mainMenuPresenter);
        }
    }
}