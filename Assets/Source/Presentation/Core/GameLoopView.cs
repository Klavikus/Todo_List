using System.Threading;
using Source.Common.Components.Implementations.Buttons;
using Source.Common.Components.Implementations.Tweens;
using Source.Controllers.Api.Mediators;
using UnityEngine;

namespace Source.Presentation.Core
{
    public class GameLoopView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        [SerializeField] private TweenActionBaseComponent _levelReactingTween;
        [SerializeField] private ActionButton _exitButton;
        
        private IUIMediator _uiMediator;
        private CancellationTokenSource _cancellationTokenSource;

        private bool _isInitialized;

        public void Initialize(IUIMediator uiMediator)
        {
            _uiMediator = uiMediator;

            _levelReactingTween.Initialize();
            _levelReactingTween.SetForwardState();
            _exitButton.Initialize();

            _uiMediator.LevelStarted += OnLevelStarted;
            _uiMediator.LevelClosed += OnLevelClosed;
            _exitButton.Clicked += OnExitButtonClicked;
            
            _isInitialized = true;
        }

        private void OnDestroy()
        {
            if (_isInitialized == false)
                return;

            _uiMediator.LevelStarted -= OnLevelStarted;
            _uiMediator.LevelClosed -= OnLevelClosed;
            _exitButton.Clicked -= OnExitButtonClicked;
        }

        private async void OnLevelStarted(int _) =>
            await _levelReactingTween.PlayBackward();

        private async void OnLevelClosed(int _) =>
            await _levelReactingTween.PlayForward();

        private void OnExitButtonClicked()
        {
            _uiMediator.CloseLevel();
        }
    }
}