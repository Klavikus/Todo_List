﻿using Source.Controllers.Api.Services;
using Source.Controllers.Core.Services;
using Source.Infrastructure.Api;
using Source.Infrastructure.Core;
using Source.Infrastructure.Core.Factories;
using Source.Infrastructure.Core.Services;
using Source.Infrastructure.Core.Services.DI;
using Source.Infrastructure.Core.Services.Providers;
using Source.Infrastructure.Core.Services.SDK;
using Sources.Infrastructure.Api.GameFsm;
using Sources.Infrastructure.Api.Services;
using Sources.Infrastructure.Api.Services.Providers;
using UnityEngine;

namespace Sources.Application.GameFSM.States
{
    public class BootstrapState : IState
    {
        private const string BootstrapScene = "Bootstrap";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ServiceContainer _services;

        public BootstrapState
        (
            GameStateMachine gameStateMachine,
            SceneLoader sceneLoader,
            ServiceContainer serviceContainer
        )
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = serviceContainer;
        }

        public void Enter()
        {
            RegisterServices();

            _sceneLoader.Load(BootstrapScene, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        public void Update()
        {
        }

        private void RegisterServices()
        {
            ConfigurationContainer configurationContainer = new ResourceProvider().Load<ConfigurationContainer>();
            IConfigurationProvider configurationProvider = new ConfigurationProvider(configurationContainer);
            ICoroutineRunner coroutineRunner = new GameObject(nameof(CoroutineRunner)).AddComponent<CoroutineRunner>();
         
            FirebaseService firebaseService = new FirebaseService();
            
            ISaveService saveService = new SaveService();
            IPersistentDataService persistentDataService = new PersistentDataService(saveService);
           
            IResourceProvider resourceProvider = new ResourceProvider();
            _services.RegisterAsSingle(resourceProvider);


            LevelModelFactory levelModelFactory = new LevelModelFactory(configurationProvider, persistentDataService);

            _services.RegisterAsSingle<IFirebaseService>(firebaseService);
            _services.RegisterAsSingle<ILevelModelFactory>(levelModelFactory);

            
            _services.RegisterAsSingle<IGameStateMachine>(_gameStateMachine);
            _services.RegisterAsSingle(coroutineRunner);
            _services.RegisterAsSingle(configurationProvider);
            _services.RegisterAsSingle(persistentDataService);

            _services.LockRegister();
        }

        private void EnterLoadLevel() => 
            _gameStateMachine.Enter<MainMenuState>();
    }
}