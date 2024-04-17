using Modules.MVPPassiveView.Runtime;
using Source.Common.Components.Implementations.Buttons;
using TMPro;

namespace Source.Presentation.Api.Views
{
    public interface ITaskView : IView
    {
        void Hide();
        void Show();
        ActionButton ExitButton { get; }
        ActionButton CompleteButton { get; }
        TMP_Text CurrentDateText { get; }
        TMP_Text NameText { get; }
        TMP_Text TaskDescriptionText { get; }
        TMP_Text CompletionText { get; }
        ActionButton DeleteButton { get; }
        void SetCompletionStatus(bool isCompleted);
    }
}