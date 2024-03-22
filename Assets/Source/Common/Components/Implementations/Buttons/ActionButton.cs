using System;
using Assets.Source.Common.Components.Implementations.Tweens;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Common.Components.Implementations.Buttons
{
    public class ActionButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TweenActionBaseComponent _actionComponent;

        public event Action Clicked;
        
        private bool _isInteractionLocked;

        private void OnEnable() =>
            _button.onClick.AddListener(OnButtonClicked);

        private void OnDestroy() =>
            _button.onClick.RemoveListener(OnButtonClicked);

        public void Initialize() =>
            _actionComponent.Initialize();

        public void SetInteractionLock(bool isLock) =>
            _isInteractionLocked = isLock;

        private async void OnButtonClicked()
        {
            if (_isInteractionLocked)
                return;

            await _actionComponent.PlayForward();
            await _actionComponent.PlayBackward();
            
            Clicked?.Invoke();
        }
    }
}