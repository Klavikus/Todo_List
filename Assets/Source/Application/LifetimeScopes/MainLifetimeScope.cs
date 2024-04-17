using System;
using System.Collections.Generic;
using Modules.DAL.Abstract.Data;
using Modules.DAL.Abstract.DataContexts;
using Modules.DAL.Abstract.Repositories;
using Modules.DAL.Implementation.Data;
using Modules.DAL.Implementation.Data.Entities;
using Modules.DAL.Implementation.DataContexts;
using Modules.DAL.Implementation.Repositories;
using Modules.MVPPassiveView.Runtime;
using Source.Application.CompositionRoots;
using Source.Application.Factories;
using Source.Common.WindowFsm;
using Source.Common.WindowFsm.Windows;
using Source.Controllers.Api.Factories;
using Source.Controllers.Api.Services;
using Source.Controllers.Core.Factories;
using Source.Controllers.Core.Presenters;
using Source.Controllers.Core.Services;
using Source.Controllers.Core.WindowFsms;
using Source.Controllers.Core.WindowFsms.Windows;
using Source.Presentation.Api.Factories;
using Source.Presentation.Api.Views;
using Source.Presentation.Core.Configs;
using Source.Presentation.Core.Factories;
using Source.Presentation.Core.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using ILogger = Source.Controllers.Api.Services.ILogger;

namespace Source.Application.LifetimeScopes
{
    public class MainLifetimeScope : LifetimeScope
    {
        [SerializeField] private PresentationConfiguration _presentationConfiguration;

        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private MainTaskListView _mainTaskListView;
        [SerializeField] private TaskCreationView _taskCreationView;
        [SerializeField] private TaskView _taskView;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterProgressRepository(builder);
            RegisterConfigurationProvider(builder);

            RegisterWindowFsm(builder);
            RegisterLogger(builder);
            RegisterTaskService(builder);

            BindTaskPresenterFactory(builder);
            BindCreatedTaskViewFactory(builder);
            BindSceneFactory(builder);

            BindViewWithPresenter<IMainMenuView, MainMenuPresenter>(builder, _mainMenuView);
            BindViewWithPresenter<IMainTaskListView, MainTaskListPresenter>(builder, _mainTaskListView);
            BindViewWithPresenter<ITaskCreationView, TaskCreationPresenter>(builder, _taskCreationView);
            BindViewWithPresenter<ITaskView, TaskViewPresenter>(builder, _taskView);

            builder.RegisterEntryPoint<MainMenuCompositionRoot>(Lifetime.Scoped);
        }

        private void RegisterProgressRepository(IContainerBuilder builder)
        {
            builder.Register<IProgressRepository>(_ =>
                {
                    Type[] dataTypes = {typeof(TaskData)};
                    IData gameData = new GameData(dataTypes);
                    IDataContext dataContext = new JsonPrefsDataContext(gameData, "JsonData");

                    return new CompositeRepository(dataContext, dataTypes);
                },
                Lifetime.Singleton);
        }

        private void RegisterConfigurationProvider(IContainerBuilder builder)
        {
            builder.Register<PresentationConfiguration>(
                _ => _presentationConfiguration,
                Lifetime.Singleton);
        }

        private void RegisterWindowFsm(IContainerBuilder builder)
        {
            builder.Register<IWindowFsm>(_ =>
                {
                    Dictionary<Type, IWindow> windows = new Dictionary<Type, IWindow>()
                    {
                        [typeof(RootWindow)] = new RootWindow(),
                        [typeof(MainTaskListWindow)] = new MainTaskListWindow(),
                        [typeof(TaskCreationWindow)] = new TaskCreationWindow(),
                        [typeof(TaskWindow)] = new TaskWindow(),
                    };

                    return new WindowFsm<RootWindow>(windows);
                },
                Lifetime.Singleton);
        }

        private void RegisterLogger(IContainerBuilder builder) =>
            builder.Register<ILogger, DebugLogger>(Lifetime.Singleton);

        private void RegisterTaskService(IContainerBuilder builder) =>
            builder.Register<ITaskService, TaskService>(Lifetime.Singleton);

        private void BindTaskPresenterFactory(IContainerBuilder builder) =>
            builder.Register<ITaskPresenterFactory<ICreatedTaskView>, TaskPresenterFactory>(Lifetime.Singleton);

        private void BindCreatedTaskViewFactory(IContainerBuilder builder) =>
            builder.Register<ICreatedTaskViewFactory, CreatedTaskViewFactory>(Lifetime.Singleton);

        private void BindSceneFactory(IContainerBuilder builder) =>
            builder.Register<ISceneFactory, SceneFactory>(Lifetime.Singleton);

        private void BindViewWithPresenter<TView, TPresenter>(IContainerBuilder builder, TView viewInstance)
            where TView : IView
            where TPresenter : IPresenter
        {
            builder.RegisterComponent(viewInstance).As<TView>();
            builder.Register<TPresenter>(Lifetime.Transient);
        }
    }
}