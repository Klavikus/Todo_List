using Source.Common.Components.Implementations.Buttons;
using TMPro;

namespace Source.Presentation.Api
{
    public interface ITaskCreationView
    {
        void Hide();
        void Show();
        ActionButton SelectDateButton { get; }
        ActionButton ApplyTaskButton { get; }
        ActionButton ExitTasksButton { get; }
        TMP_InputField TaskNameInputField { get; }
        TMP_InputField TaskDescriptionInputField { get; }
    }
}