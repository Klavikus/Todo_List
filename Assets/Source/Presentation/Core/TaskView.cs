using Source.Common.Components.Implementations.Buttons;
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
        [SerializeField] private TMP_Text _taskDescriptionText;
        [SerializeField] private TMP_Text _completionText;

        public ActionButton ExitButton => _exitButton;
        public ActionButton CompleteButton => _completeButton;
        public TMP_Text CurrentDateText => _currentDateText;
        public TMP_Text NameText => _nameText;
        public TMP_Text TaskDescriptionText => _taskDescriptionText;
        public TMP_Text CompletionText => _completionText;

        public void Hide() =>
            _canvas.enabled = false;

        public void Show() =>
            _canvas.enabled = true;
    }
}