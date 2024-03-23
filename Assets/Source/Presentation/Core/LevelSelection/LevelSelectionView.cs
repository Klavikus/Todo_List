using Source.Common.Components.Implementations.SelectablesGroup;
using Source.Common.Components.Implementations.Tweens;
using Source.Controllers.Api.Mediators;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Presentation.Core.LevelSelection
{
    public class LevelSelectionView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _nextStageButton;
        [SerializeField] private Button _previousStageButton;
        [SerializeField] private Button _startGameButton;

        [SerializeField] private TweenActionBaseComponent _tweenActionComponent;
        [SerializeField] private SelectablesUiGroup _selectableLevels;
        [SerializeField] private SelectablesUiGroup _selectableLevelStages;

        private IUIMediator _iuiMediator;

        private bool _isInitialized;

        public void Initialize(IUIMediator iuiMediator)
        {
            _iuiMediator = iuiMediator;

            _tweenActionComponent.Initialize();
            
            _iuiMediator.BindSelectableLevelsGroup(_selectableLevels);
            _iuiMediator.BindSelectableLevelStagesGroup(_selectableLevelStages);

            UpdateStageButtonsVisibility(_iuiMediator.GetSelectedLevelStageId());

            _iuiMediator.LevelStageSelected += UpdateStageButtonsVisibility;
            _iuiMediator.LevelClosed += OnLevelClosed;

            _nextStageButton.onClick.AddListener(OnNextStageButtonClicked);
            _previousStageButton.onClick.AddListener(OnPreviousStageButtonClicked);
            _startGameButton.onClick.AddListener(OnStartGameButtonClicked);

            _isInitialized = true;
        }

        private void OnDestroy()
        {
            if (_isInitialized == false)
                return;

            _iuiMediator.LevelStageSelected -= UpdateStageButtonsVisibility;
            _iuiMediator.LevelClosed -= OnLevelClosed;

            _nextStageButton.onClick.RemoveListener(OnNextStageButtonClicked);
            _previousStageButton.onClick.RemoveListener(OnPreviousStageButtonClicked);
            _startGameButton.onClick.RemoveListener(OnStartGameButtonClicked);
        }

        private void UpdateStageButtonsVisibility(int selectedStageId)
        {
            _previousStageButton.gameObject.SetActive(_iuiMediator.IsPreviousStageAvailable);
            _nextStageButton.gameObject.SetActive(_iuiMediator.IsNextStageAvailable);
        }

        private void OnNextStageButtonClicked()
        {
            _iuiMediator.SelectNextStage();
        }

        private void OnPreviousStageButtonClicked()
        {
            _iuiMediator.SelectPreviousStage();
        }

        private async void OnStartGameButtonClicked()
        {
            _iuiMediator.StartLevel();
            await _tweenActionComponent.PlayForward();
            Hide();
        }

        private async void OnLevelClosed(int _)
        {
            Show();
            await _tweenActionComponent.PlayBackward();
        }

        private void Hide() =>
            _canvas.enabled = false;

        private void Show() =>
            _canvas.enabled = true;
    }
}