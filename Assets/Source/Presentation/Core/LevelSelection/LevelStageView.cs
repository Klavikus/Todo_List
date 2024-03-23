using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Source.Common.Components.Implementations.Tweens;
using Source.Controllers.Api.Mediators;
using Source.Presentation.Api;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Presentation.Core.LevelSelection
{
    public class LevelStageView : MonoBehaviour, ILevelStageView
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _selectionBorder;
        [SerializeField] private Button _button;
        [SerializeField] private TweenActionBaseComponent _tweenActionBaseComponent;

        private IUIMediator _iuiMediator;
        private int _order;
        private bool _isInitialized;
        private bool _isFocused;
        private bool _isInProgress;
        private CancellationTokenSource _cancellationTokenSource;

        public void Initialize(IUIMediator mediator, Sprite icon, int order)
        {
            _iuiMediator = mediator;
            _icon.sprite = icon;
            _order = order;

            _cancellationTokenSource = new CancellationTokenSource();

            _tweenActionBaseComponent.Initialize();

            OnLevelStageSelected(_iuiMediator.GetSelectedLevelStageId());

            _iuiMediator.LevelStageSelected += OnLevelStageSelected;
            _button.onClick.AddListener(OnButtonCLicked);

            _isInitialized = true;
        }

        private void OnDestroy()
        {
            if (_isInitialized == false)
                return;

            _iuiMediator.LevelStageSelected -= OnLevelStageSelected;

            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }

        public async void Focus()
        {
            try
            {
                await UniTask.WaitUntil(() => _isInProgress == false,
                    cancellationToken: _cancellationTokenSource.Token);

                if (_isFocused)
                    return;

                _isInProgress = true;
                _isFocused = true;

                await _tweenActionBaseComponent.PlayForward();
                _isInProgress = false;
            }
            catch (OperationCanceledException exception)
            {
                Console.WriteLine(exception);
            }
        }

        public async void Unfocus()
        {
            try
            {
                await UniTask.WaitUntil(() => _isInProgress == false,
                    cancellationToken: _cancellationTokenSource.Token);

                if (_isFocused == false)
                    return;

                _isInProgress = true;
                _isFocused = false;

                await _tweenActionBaseComponent.PlayBackward();
                _isInProgress = false;
            }
            catch (OperationCanceledException exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void OnLevelStageSelected(int selectedOrder)
        {
            _selectionBorder.enabled = selectedOrder == _order;

            if (selectedOrder == _order)
                Focus();
            else
                Unfocus();
        }

        private void OnButtonCLicked()
        {
            if (_iuiMediator.GetSelectedLevelStageId() == _order)
                return;

            _iuiMediator.SelectLevelStage(_order);
        }
    }
}