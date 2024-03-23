using Source.Common.Components.Implementations.Buttons;
using UnityEngine;

namespace Source.Presentation.Api
{
    public interface IMainTaskListView
    {
        void Show();
        void Hide();
        ActionButton SelectDateButton { get; }
        ActionButton CreateTasksButton { get; }
        ActionButton ExitTasksButton { get; }
        Transform CreatedTaskContainer { get; }
        void SetSelectedDateText(string text);
    }
}