using System;
using System.Collections.Generic;
using Modules.MVPPassiveView.Runtime;
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
using Source.Presentation.Core.Factories;
using Source.Presentation.Core.Views;
using UnityEngine;
using Zenject;
using ILogger = Source.Controllers.Api.Services.ILogger;

namespace Source.Application.Installers
{
    public class MainSceneInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private MainTaskListView _mainTaskListView;
        [SerializeField] private TaskCreationView _taskCreationView;
        [SerializeField] private TaskView _taskView;

        public override void InstallBindings()
        {
            BindWindowFsm();
            BindLogger();
            BindTaskService();

            BindTaskPresenterFactory();
            BindCreatedTaskViewFactory();
            BindSceneFactory();

            BindViewWithPresenter<IMainMenuView, MainMenuPresenter>(_mainMenuView);
            BindViewWithPresenter<IMainTaskListView, MainTaskListPresenter>(_mainTaskListView);
            BindViewWithPresenter<ITaskCreationView, TaskCreationPresenter>(_taskCreationView);
            BindViewWithPresenter<ITaskView, TaskViewPresenter>(_taskView);
        }

        private void BindWindowFsm()
        {
            Dictionary<Type, IWindow> windows = new Dictionary<Type, IWindow>()
            {
                [typeof(RootWindow)] = new RootWindow(),
                [typeof(MainTaskListWindow)] = new MainTaskListWindow(),
                [typeof(TaskCreationWindow)] = new TaskCreationWindow(),
                [typeof(TaskWindow)] = new TaskWindow(),
            };

            IWindowFsm windowFsm = new WindowFsm<RootWindow>(windows);

            Container
                .Bind<IWindowFsm>()
                .FromInstance(windowFsm)
                .AsSingle();
        }

        private void BindLogger()
        {
            Container
                .Bind<ILogger>()
                .To<DebugLogger>()
                .AsSingle();
        }

        private void BindTaskService()
        {
            Container
                .Bind<ITaskService>()
                .To<TaskService>()
                .AsSingle();
        }

        private void BindTaskPresenterFactory()
        {
            Container
                .Bind<ITaskPresenterFactory<ICreatedTaskView>>()
                .To<TaskPresenterFactory>()
                .AsSingle();
        }

        private void BindCreatedTaskViewFactory()
        {
            Container
                .Bind<ICreatedTaskViewFactory>()
                .To<CreatedTaskViewFactory>()
                .AsSingle();
        }

        private void BindSceneFactory()
        {
            Container
                .Bind<ISceneFactory>()
                .To<SceneFactory>()
                .AsSingle();
        }

        private void BindViewWithPresenter<TView, TPresenter>(TView viewInstance)
            where TView : IView
            where TPresenter : IPresenter
        {
            Container
                .Bind<TView>()
                .FromInstance(viewInstance)
                .AsSingle();

            Container
                .Bind<TPresenter>()
                .AsTransient();
        }
    }
}