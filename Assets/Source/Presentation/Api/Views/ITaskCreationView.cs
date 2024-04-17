using Modules.MVPPassiveView.Runtime;
using Source.Common.Components.Implementations.Buttons;
using TMPro;

namespace Source.Presentation.Api.Views
{
    public interface ITaskCreationView : IView
    {
        void Hide();
        void Show();
        ActionButton SelectDateButton { get; }
        ActionButton ApplyTaskButton { get; }
        ActionButton ExitTasksButton { get; }
        TMP_InputField TaskNameInputField { get; }
        TMP_InputField TaskDescriptionInputField { get; }
        void Destroy();
        void SetSelectedDateText(string date);
    }
}