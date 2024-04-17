using Modules.DAL.Abstract.Repositories;
using Modules.MVPPassiveView.Runtime;
using Source.Controllers.Core.Presenters;
using Source.Presentation.Api.Views;
using Zenject;

namespace Source.Application.Factories
{
    public class SceneFactory : ISceneFactory
    {
        private readonly DiContainer _container;

        public SceneFactory(DiContainer container)
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