using Source.Application.Factories;
using VContainer.Unity;

namespace Source.Application.CompositionRoots
{
    public class MainMenuCompositionRoot : IStartable
    {
        private readonly ISceneFactory _sceneFactory;

        public MainMenuCompositionRoot(ISceneFactory sceneFactory)
        {
            _sceneFactory = sceneFactory;
        }

        public void Start() =>
            _sceneFactory.CreateMainScene();
    }
}