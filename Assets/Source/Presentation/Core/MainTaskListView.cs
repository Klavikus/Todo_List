using System.Threading;
using Assets.Source.Common.Components.Implementations.Buttons;
using TMPro;
using UnityEngine;

namespace Source.Presentation.Core
{
    public class MainTaskListView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        [SerializeField] private TMP_Text _currentDateText;
        [SerializeField] private ActionButton _selectDateButton;
        [SerializeField] private ActionButton _createTasksButton;
        [SerializeField] private ActionButton _exitTasksButton;
        [SerializeField] private Transform _createdTaskContainer;
        
        private CancellationTokenSource _cancellationTokenSource;

        private bool _isInitialized;

        public void Initialize()
        {
            _selectDateButton.Initialize();
            _createTasksButton.Initialize();
            _exitTasksButton.Initialize();
            
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