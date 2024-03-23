using Source.Common.Components.Implementations.Buttons;

namespace Source.Presentation.Api
{
    public interface IMainMenuView
    {
        ActionButton ViewTasksButton { get; }
        ActionButton CreateTasksButton { get; }
        public void Show();
        public void Hide();
        void SetCurrentDateText(string text);
        void SetTodayTasksText(string text);
    }
}