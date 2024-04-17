using Modules.MVPPassiveView.Runtime;
using Source.Common.Components.Implementations.Buttons;
using Source.Presentation.Api.Views;
using TMPro;
using UnityEngine;

namespace Source.Presentation.Core.Views
{
    public class TaskCreationView : ViewBase, ITaskCreationView
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private TMP_Text _selectedDateText;
        [SerializeField] private ActionButton _applyTaskButton;
        [SerializeField] private ActionButton _exitTasksButton;
        [SerializeField] private ActionButton _selectDateButton;
        [SerializeField] private TMP_InputField _taskNameInputField;
        [SerializeField] private TMP_InputField _taskDescriptionInputField;

        public ActionButton SelectDateButton => _selectDateButton;
        public ActionButton ApplyTaskButton => _applyTaskButton;
        public ActionButton ExitTasksButton => _exitTasksButton;
        public TMP_InputField TaskNameInputField => _taskNameInputField;
        public TMP_InputField TaskDescriptionInputField => _taskDescriptionInputField;

        public void Show() =>
            _canvas.enabled = true;

        public void Hide() =>
            _canvas.enabled = false;

        public void Destroy() =>
            Object.Destroy(gameObject);

        public void SetSelectedDateText(string date) =>
            _selectedDateText.text = date;
    }
}