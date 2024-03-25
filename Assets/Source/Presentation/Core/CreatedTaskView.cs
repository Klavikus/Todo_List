using Source.Common.Components.Implementations.Buttons;
using Source.Presentation.Api;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Presentation.Core
{
    public class CreatedTaskView : ViewBase, ICreatedTaskView
    {
        [field: SerializeField] public TMP_Text Name { get; private set; }
        [field: SerializeField] public Image StatusImage { get; private set; }
        [field: SerializeField] public ActionButton OpenViewButton { get; private set; }

        public void Destroy() =>
            Object.Destroy(gameObject);
    }
}