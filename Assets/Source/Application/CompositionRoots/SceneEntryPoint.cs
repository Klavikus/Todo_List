using Source.Infrastructure.Api.Services.Providers;
using Source.Infrastructure.Core.Factories;
using Source.Infrastructure.Core.Services.DI;
using UnityEngine;
using MainMenuView = Source.Presentation.Core.MainMenuView;
using MainMenuViewModel = Source.Controllers.Core.ViewModels.MainMenuViewModel;

namespace Source.Application.CompositionRoots
{
    public class SceneEntryPoint : MonoBehaviour
    {
        [SerializeField] private MainMenuView _mainMenuView;
    
        private IConfigurationProvider _configurationProvider;
        private ILevelModelFactory _levelModelFactory;
        private MainMenuViewModel _mainMenuViewModel;

        private void Initialize(ServiceContainer container)
        {
            _configurationProvider = container.Single<IConfigurationProvider>();
            _levelModelFactory = container.Single<ILevelModelFactory>();
        
            Initialize();
        }

        private void Initialize()
        {
      
        }
    }
}