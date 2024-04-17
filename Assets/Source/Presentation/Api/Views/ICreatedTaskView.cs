using Modules.MVPPassiveView.Runtime;
using Source.Common.Components.Implementations.Buttons;
using TMPro;
using UnityEngine.UI;

namespace Source.Presentation.Api.Views
{
    public interface ICreatedTaskView : IView
    {
        TMP_Text Name { get; }
        Image StatusImage { get; }
        ActionButton OpenViewButton { get; }
        void Destroy();
        void SetCompleted();
        void SetPending();
    }
}