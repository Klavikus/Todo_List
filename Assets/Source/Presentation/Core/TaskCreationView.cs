using System.Threading;
using Assets.Source.Common.Components.Implementations.Buttons;
using Assets.Source.Common.WindowFsm;
using Source.Presentation.Api;
using TMPro;
using UnityEngine;

namespace Source.Presentation.Core
{
    public class TaskCreationView : ViewBase, ITaskCreationView
    {
        [SerializeField] private Canvas _canvas;

        [SerializeField] private TMP_Text _currentDateText;
        [SerializeField] private ActionButton _selectDateButton;
        [SerializeField] private ActionButton _applyTaskButton;
        [SerializeField] private ActionButton _exitTasksButton;
        [SerializeField] private TMP_InputField _taskNameInputField;
        [SerializeField] private TMP_InputField _taskDescriptionInputField;

        private CancellationTokenSource _cancellationTokenSource;
        private IWindowFsm _windowFsm;

        public override void OnAfterConstruct()
        {
            _selectDateButton.Initialize();
            _applyTaskButton.Initialize();
            _exitTasksButton.Initialize();
        }

        public void Hide() =>
            _canvas.enabled = false;

        public void Show() =>
            _canvas.enabled = true;
    }
}