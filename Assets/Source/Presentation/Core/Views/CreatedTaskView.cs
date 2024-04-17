using Modules.MVPPassiveView.Runtime;
using Source.Common.Components.Implementations.Buttons;
using Source.Presentation.Api.Views;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Presentation.Core.Views
{
    public class CreatedTaskView : ViewBase, ICreatedTaskView
    {
        [SerializeField] private Sprite _completedSprite;
        [SerializeField] private Sprite _pendingSprite;

        [field: SerializeField] public TMP_Text Name { get; private set; }
        [field: SerializeField] public Image StatusImage { get; private set; }
        [field: SerializeField] public ActionButton OpenViewButton { get; private set; }

        public void Destroy() =>
            Object.Destroy(gameObject);

        public void SetCompleted() =>
            StatusImage.sprite = _completedSprite;

        public void SetPending() =>
            StatusImage.sprite = _pendingSprite;
    }
}