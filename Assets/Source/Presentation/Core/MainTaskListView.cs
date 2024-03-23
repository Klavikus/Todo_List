using Assets.Source.Common.Components.Implementations.Buttons;
using Source.Presentation.Api;
using TMPro;
using UnityEngine;

namespace Source.Presentation.Core
{
    public class MainTaskListView : ViewBase, IMainTaskListView
    {
        [SerializeField] private Canvas _canvas;

        [SerializeField] private TMP_Text _currentDateText;
        [SerializeField] private ActionButton _selectDateButton;
        [SerializeField] private ActionButton _createTasksButton;
        [SerializeField] private ActionButton _exitTasksButton;
        [SerializeField] private Transform _createdTaskContainer;

        public override void OnAfterConstruct()
        {
            _selectDateButton.Initialize();
            _createTasksButton.Initialize();
            _exitTasksButton.Initialize();
        }

        public void Hide() =>
            _canvas.enabled = false;

        public void Show() =>
            _canvas.enabled = true;
    }
}