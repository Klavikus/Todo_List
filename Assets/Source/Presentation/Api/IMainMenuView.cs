using Source.Common.Components.Implementations.Buttons;
using TMPro;

namespace Source.Presentation.Api
{
    public interface IMainMenuView
    {
        ActionButton ViewTasksButton { get; }
        ActionButton CreateTasksButton { get; }
        TMP_Text AllTasksText { get; }
        TMP_Text AllCompletedText { get; }
        TMP_Text AllInProgressText { get; }
        TMP_Text CurrentDateText { get; }
        TMP_Text TodayTasksText { get; }
        TMP_Text TodayCompletedTasksText { get; }
        TMP_Text TodayInProgressTasksText { get; }
        public void Show();
        public void Hide();
    }
}