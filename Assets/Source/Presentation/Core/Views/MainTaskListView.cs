using Modules.MVPPassiveView.Runtime;
using Source.Common.Components.Implementations.Buttons;
using Source.Presentation.Api.Views;
using TMPro;
using UnityEngine;

namespace Source.Presentation.Core.Views
{
    public class MainTaskListView : ViewBase, IMainTaskListView
    {
        [SerializeField] private Canvas _canvas;

        [SerializeField] private TMP_Text _selectedDateText;
        [SerializeField] private ActionButton _selectDateButton;
        [SerializeField] private ActionButton _createTasksButton;
        [SerializeField] private ActionButton _exitTasksButton;
        [SerializeField] private ActionButton _deleteCompletedTasksButton;
        [SerializeField] private Transform _createdTaskContainer;

        public ActionButton SelectDateButton => _selectDateButton;
        public ActionButton CreateTasksButton => _createTasksButton;
        public ActionButton ExitTasksButton => _exitTasksButton;
        public ActionButton DeleteCompletedTasksButton => _deleteCompletedTasksButton;
        public Transform CreatedTaskContainer => _createdTaskContainer;

        public void Show() =>
            _canvas.enabled = true;

        public void Hide() =>
            _canvas.enabled = false;

        public void SetSelectedDateText(string text) =>
            _selectedDateText.text = text;
    }
}