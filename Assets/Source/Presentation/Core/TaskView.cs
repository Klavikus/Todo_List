using System.Threading;
using Assets.Source.Common.Components.Implementations.Buttons;
using TMPro;
using UnityEngine;

namespace Source.Presentation.Core
{
    public class TaskView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        [SerializeField] private TMP_Text _currentDateText;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private ActionButton _exitButton;
        [SerializeField] private ActionButton _completeButton;
        [SerializeField] private TMP_InputField _taskDescriptionInputField;
        
        private CancellationTokenSource _cancellationTokenSource;

        private bool _isInitialized;

        public void Initialize()
        {
            _exitButton.Initialize();
            _completeButton.Initialize();
            
            _isInitialized = true;
        }

        private void OnDestroy()
        {
            if (_isInitialized == false)
                return;
        }

        private void Hide() =>
            _canvas.enabled = false;

        private void Show() =>
            _canvas.enabled = true;
    }
}