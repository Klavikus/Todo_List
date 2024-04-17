using System;
using Modules.DAL.Abstract.Data;
using Modules.DAL.Abstract.DataContexts;
using Modules.DAL.Abstract.Repositories;
using Modules.DAL.Implementation.Data;
using Modules.DAL.Implementation.Data.Entities;
using Modules.DAL.Implementation.DataContexts;
using Modules.DAL.Implementation.Repositories;
using Source.Presentation.Core.Configs;
using UnityEngine;
using Zenject;

namespace Source.Application.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private PresentationConfiguration _presentationConfiguration;

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
                .Bind<PresentationConfiguration>()
                .FromInstance(_presentationConfiguration)
                .AsSingle();
        }
    }
}