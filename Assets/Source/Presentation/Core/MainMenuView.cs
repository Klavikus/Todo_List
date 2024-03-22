using System.Threading;
using Assets.Source.Common.Components.Implementations.Tweens;
using Source.Controllers.Api.Mediators;
using UnityEngine;

namespace Source.Presentation.Core
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Transform _levelContainer;
        [SerializeField] private Transform _stageContainer;

        [SerializeField] private TweenActionBaseComponent _levelReactingTween;

        private IUIMediator _uiMediator;
        private CancellationTokenSource _cancellationTokenSource;

        private bool _isInitialized;

        public void Initialize()
        {
            _isInitialized = true;
        }

        private void OnDestroy()
        {
            if (_isInitialized == false)
                return;

            _uiMediator.LevelSelected -= OnLevelSelected;
            _uiMediator.LevelStarted -= OnLevelStarted;
        }

        private void OnLevelSelected(int _) =>
            _uiMediator.RebuildLevelStageViews();

        private async void OnLevelStarted(int _)
        {
            await _levelReactingTween.PlayForward();
            Hide();
            _uiMediator.StartGameLoop();
        }

        private async void OnLevelClosed(int _)
        {
            Show();
            await _levelReactingTween.PlayBackward();
        }

        private void Hide() =>
            _canvas.enabled = false;

        private void Show() =>
            _canvas.enabled = true;
    }
}