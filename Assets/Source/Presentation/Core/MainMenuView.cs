using System.Threading;
using Assets.Source.Common.Components.Implementations.Buttons;
using Assets.Source.Common.WindowFsm;
using Source.Controllers.Api;
using Source.Presentation.Api;
using TMPro;
using UnityEngine;

namespace Source.Presentation.Core
{
    public class MainMenuView : ViewBase, IMainMenuView
    {
        [SerializeField] private Canvas _canvas;

        [SerializeField] private TMP_Text _currentDateText;
        [SerializeField] private TMP_Text _todayTasksText;
        [SerializeField] private ActionButton _viewTasksButton;
        [SerializeField] private ActionButton _createTasksButton;

        public ActionButton ViewTasksButton => _viewTasksButton;
        public ActionButton CreateTasksButton => _createTasksButton;

        public void Show() =>
            _canvas.enabled = true;

        public void Hide() =>
            _canvas.enabled = false;

        public void SetCurrentDateText(string text) =>
            _currentDateText.text = text; 
        public void SetTodayTasksText(string text) =>
            _todayTasksText.text = text;
    }
}