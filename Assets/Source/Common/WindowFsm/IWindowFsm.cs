﻿using System;
using Assets.Source.Common.WindowFsm.Windows;

namespace Assets.Source.Common.WindowFsm
{
    public interface IWindowFsm
    {
        event Action<IWindow> Opened;
        event Action<IWindow> Closed;
        
        IWindow CurrentWindow { get; }
        
        void OpenWindow<TWindow>() where TWindow : IWindow;
        void CloseCurrentWindow();
        void ClearHistory();
    }
}