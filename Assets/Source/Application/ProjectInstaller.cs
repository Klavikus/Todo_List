using System;
using Modules.DAL.Abstract.Data;
using Modules.DAL.Abstract.DataContexts;
using Modules.DAL.Abstract.Repositories;
using Modules.DAL.Implementation.Data;
using Modules.DAL.Implementation.Data.Entities;
using Modules.DAL.Implementation.DataContexts;
using Modules.DAL.Implementation.Repositories;
using Source.Infrastructure.Core;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private ConfigurationContainer _configurationContainer;

    public override void InstallBindings()
    {
        BindProgressRepository();
        BindConfigurationProvider();
    }

    private void BindProgressRepository()
    {
        Type[] dataTypes = {typeof(TaskData)};
        IData gameData = new GameData(dataTypes);
        IDataContext dataContext = new JsonPrefsDataContext(gameData, "JsonData");
        IProgressRepository repository = new CompositeRepository(dataContext, dataTypes);

        Container
            .Bind<IProgressRepository>()
            .FromInstance(repository)
            .AsSingle();
    }

    private void BindConfigurationProvider()
    {
        Container
            .Bind<ConfigurationContainer>()
            .FromInstance(_configurationContainer)
            .AsSingle();
    }
}