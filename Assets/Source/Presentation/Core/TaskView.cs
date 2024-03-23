using Assets.Source.Common.Components.Implementations.Buttons;
using Source.Presentation.Api;
using TMPro;
using UnityEngine;

namespace Source.Presentation.Core
{
    public class TaskView : ViewBase, ITaskView
    {
        [SerializeField] private Canvas _canvas;

        [SerializeField] private TMP_Text _currentDateText;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private ActionButton _exitButton;
        [SerializeField] private ActionButton _completeButton;
        [SerializeField] private TMP_InputField _taskDescriptionInputField;

        public override void OnAfterConstruct()
        {
            _exitButton.Initialize();
            _completeButton.Initialize();
        }

        public void Hide() =>
            _canvas.enabled = false;

        public void Show() =>
            _canvas.enabled = true;
    }
}