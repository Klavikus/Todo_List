﻿using System.Threading;
using Assets.Source.Common.Components.Implementations.Buttons;
using TMPro;
using UnityEngine;

namespace Source.Presentation.Core
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        [SerializeField] private TMP_Text _currentDateText;
        [SerializeField] private TMP_Text _todayTasksText;
        [SerializeField] private ActionButton _viewTasksButton;
        [SerializeField] private ActionButton _createTasksButton;

        private CancellationTokenSource _cancellationTokenSource;

        private bool _isInitialized;

        public void Initialize()
        {
            _viewTasksButton.Initialize();
            _createTasksButton.Initialize();
            
            _isInitialized = true;
        }

        private void OnDestroy()
        {
            if (_isInitialized == false)
                return;
        }

        private void Hide() =>
            _canvas.enabled = false;

        private void Show() =>
            _canvas.enabled = true;
    }
}