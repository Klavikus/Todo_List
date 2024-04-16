using System;
using System.Collections.Generic;
using Modules.DAL.Implementation.Data.Entities;
using Source.Application.Factories;
using Source.Common.WindowFsm;
using Source.Common.WindowFsm.Windows;
using Source.Controllers.Api;
using Source.Controllers.Api.Services;
using Source.Controllers.Core.Presenters;
using Source.Controllers.Core.Services;
using Source.Controllers.Core.WindowFsms;
using Source.Controllers.Core.WindowFsms.Windows;
using Source.Presentation.Api;
using Source.Presentation.Core;
using UnityEngine;
using Zenject;
using ILogger = Source.Controllers.Api.Services.ILogger;

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
        BindCreatedTaskViewCreatingStrategy();

        BindMainMenuView();
        BindMainTaskListView();
        BindTaskCreationView();
        BindTaskView();
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
            .Bind<ITaskPresenterFactory>()
            .To<TaskPresenterFactory>()
            .AsSingle();
    }

    private void BindCreatedTaskViewFactory()
    {
        Container
            .Bind<ICreatedTaskViewFactory>()
            .To<TaskViewFactory>()
            .AsSingle();
    }

    private void BindCreatedTaskViewCreatingStrategy()
    {
        Container
            .Bind<Func<TaskData, Transform, ICreatedTaskView>>()
            .FromMethod(_ => Container.Resolve<ICreatedTaskViewFactory>().Create)
            .AsSingle();
    }

    private void BindMainMenuView()
    {
        Container
            .Bind<IMainMenuView>()
            .FromInstance(_mainMenuView)
            .AsSingle();

        Container
            .Bind<MainMenuPresenter>()
            .AsTransient();

        Container
            .Bind<MainMenuView>()
            .FromMethod(_ =>
            {
                IPresenter presenter = Container.Resolve<MainMenuPresenter>();
                _mainMenuView.Construct(presenter);

                return _mainMenuView;
            })
            .AsSingle();
    }

    private void BindMainTaskListView()
    {
        Container
            .Bind<IMainTaskListView>()
            .FromInstance(_mainTaskListView)
            .AsSingle();

        Container
            .Bind<MainTaskListPresenter>()
            .AsTransient();

        Container
            .Bind<MainTaskListView>()
            .FromMethod(_ =>
            {
                IPresenter presenter = Container.Resolve<MainTaskListPresenter>();
                _mainTaskListView.Construct(presenter);

                return _mainTaskListView;
            })
            .AsSingle();
    }

    private void BindTaskCreationView()
    {
        Container
            .Bind<ITaskCreationView>()
            .FromInstance(_taskCreationView)
            .AsSingle();

        Container
            .Bind<TaskCreationPresenter>()
            .AsTransient();

        Container
            .Bind<TaskCreationView>()
            .FromMethod(_ =>
            {
                IPresenter presenter = Container.Resolve<TaskCreationPresenter>();
                _taskCreationView.Construct(presenter);

                return _taskCreationView;
            })
            .AsSingle();
    }

    private void BindTaskView()
    {
        Container
            .Bind<ITaskView>()
            .FromInstance(_taskView)
            .AsSingle();

        Container
            .Bind<TaskViewPresenter>()
            .AsTransient();

        Container
            .Bind<TaskView>()
            .FromMethod(_ =>
            {
                IPresenter presenter = Container.Resolve<TaskViewPresenter>();
                _taskView.Construct(presenter);

                return _taskView;
            })
            .AsSingle();
    }
}