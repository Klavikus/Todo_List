using Source.Common.Components.Implementations.Buttons;
using Source.Presentation.Api;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Presentation.Core
{
    public class MainMenuView : ViewBase, IMainMenuView
    {
        [SerializeField] private Canvas _canvas;

        [field: SerializeField] public TMP_Text AllTasksText { get; private set; }
        [field: SerializeField] public TMP_Text AllCompletedText { get; private set; }
        [field: SerializeField] public TMP_Text AllInProgressText { get; private set; }
        [field: SerializeField] public TMP_Text CurrentDateText { get; private set; }
        [field: SerializeField] public TMP_Text TodayTasksText { get; private set; }
        [field: SerializeField] public TMP_Text TodayCompletedTasksText { get; private set; }
        [field: SerializeField] public TMP_Text TodayInProgressTasksText { get; private set; }
        [field: SerializeField] public ActionButton ViewTasksButton { get; private set; }
        [field: SerializeField] public ActionButton CreateTasksButton { get; private set; }

        public void Show() =>
            _canvas.enabled = true;

        public void Hide() =>
            _canvas.enabled = false;
    }
}