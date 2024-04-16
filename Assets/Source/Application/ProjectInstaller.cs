using System;
using Modules.DAL.Abstract.Data;
using Modules.DAL.Abstract.DataContexts;
using Modules.DAL.Abstract.Repositories;
using Modules.DAL.Implementation.Data;
using Modules.DAL.Implementation.Data.Entities;
using Modules.DAL.Implementation.DataContexts;
using Modules.DAL.Implementation.Repositories;
using Source.Application;
using Source.Application.GameFSM;
using Source.Infrastructure.Api;
using Source.Infrastructure.Api.Services;
using Source.Infrastructure.Api.Services.Providers;
using Source.Infrastructure.Core;
using Source.Infrastructure.Core.Services.Providers;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private ConfigurationContainer _configurationContainer;

    public override void InstallBindings()
    {
        Type[] dataTypes = {typeof(TaskData)};
        IData gameData = new GameData(dataTypes);
        IDataContext dataContext = new JsonPrefsDataContext(gameData, "JsonData");
        IProgressRepository repository = new CompositeRepository(dataContext, dataTypes);

        Container
            .BindInterfacesTo(typeof(CompositeRepository))
            .FromInstance(repository)
            .AsSingle();
        
        // BindNotUsed();
    }

    private void BindNotUsed()
    {
        IConfigurationProvider configurationProvider = new ConfigurationProvider(_configurationContainer);

        Container
            .Bind<IConfigurationProvider>()
            .FromMethod(() => new ConfigurationProvider(_configurationContainer))
            .AsSingle();

        Container
            .Bind<ICoroutineRunner>()
            .FromMethod(() => new GameObject(nameof(CoroutineRunner)).AddComponent<CoroutineRunner>())
            .AsSingle();

        Container
            .Bind<IResourceProvider>()
            .FromMethod(() => new CustomResourceProvider())
            .AsSingle();

        Container
            .BindInterfacesAndSelfTo<GameStateMachine>()
            .AsSingle();
        Container
            .Bind<SceneLoader>()
            .AsSingle();

        Container
            .Bind<Game>()
            .AsSingle();
    }
}