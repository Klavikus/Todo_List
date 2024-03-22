using Assets.Source.Common.Components.Implementations.Tweens;
using Cysharp.Threading.Tasks;
using Source.Controllers.Api.Mediators;
using Source.Presentation.Api;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Presentation.Core.LevelSelection
{
    public class LevelView : MonoBehaviour, ILevelView
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

        public void Initialize(IUIMediator iuiMediator, int order, Sprite icon)
        {
            _iuiMediator = iuiMediator;
            _order = order;
            _icon.sprite = icon;

            _tweenActionBaseComponent.Initialize();

            OnLevelSelected(_iuiMediator.GetSelectedLevelId());

            _iuiMediator.LevelSelected += OnLevelSelected;
            _button.onClick.AddListener(OnButtonCLicked);

            _isInitialized = true;
        }

        private void OnDestroy()
        {
            if (_isInitialized == false)
                return;

            _iuiMediator.LevelSelected -= OnLevelSelected;
            _button.onClick.RemoveListener(OnButtonCLicked);
        }

        public async void Focus()
        {
            await UniTask.WaitUntil(() => _isInProgress == false);

            if (_isFocused)
                return;

            _isInProgress = true;
            _isFocused = true;

            await _tweenActionBaseComponent.PlayForward();
            _isInProgress = false;
        }

        public async void Unfocus()
        {
            await UniTask.WaitUntil(() => _isInProgress == false);

            if (_isFocused == false)
                return;

            _isInProgress = true;
            _isFocused = false;

            await _tweenActionBaseComponent.PlayBackward();
            _isInProgress = false;
        }

        private void OnButtonCLicked()
        {
            if (_iuiMediator.GetSelectedLevelId() == _order)
                return;

            _iuiMediator.SelectLevel(_order);
        }

        private void OnLevelSelected(int selectedLevelId)
        {
            _selectionBorder.enabled = selectedLevelId == _order;

            if (selectedLevelId == _order)
                Focus();
            else
                Unfocus();
        }
    }
}