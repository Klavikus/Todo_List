using Modules.MVPPassiveView.Runtime;
using Source.Common.Components.Implementations.Buttons;
using UnityEngine;

namespace Source.Presentation.Api.Views
{
    public interface IMainTaskListView: IView
    {
        void Show();
        void Hide();
        ActionButton SelectDateButton { get; }
        ActionButton CreateTasksButton { get; }
        ActionButton ExitTasksButton { get; }
        Transform CreatedTaskContainer { get; }
        ActionButton DeleteCompletedTasksButton { get; }
        void SetSelectedDateText(string text);
    }
}