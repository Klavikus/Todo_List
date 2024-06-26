﻿using Modules.MVPPassiveView.Runtime;
using Source.Common.Components.Implementations.Buttons;
using Source.Presentation.Api.Views;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Presentation.Core.Views
{
    public class TaskView : ViewBase, ITaskView
    {
        [SerializeField] private Canvas _canvas;

        [SerializeField] private TMP_Text _currentDateText;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private ActionButton _exitButton;
        [SerializeField] private ActionButton _completeButton;
        [SerializeField] private ActionButton _deleteButton;
        [SerializeField] private TMP_Text _taskDescriptionText;
        [SerializeField] private TMP_Text _completionText;
        [SerializeField] private Image _completionIcon;
        [SerializeField] private Sprite _completeSprite;
        [SerializeField] private Sprite _pendingSprite;

        public ActionButton ExitButton => _exitButton;
        public ActionButton CompleteButton => _completeButton;
        public TMP_Text CompletionText => _completionText;
        public ActionButton DeleteButton => _deleteButton;
        public TMP_Text CurrentDateText => _currentDateText;
        public TMP_Text NameText => _nameText;
        public TMP_Text TaskDescriptionText => _taskDescriptionText;

        public void Hide() =>
            _canvas.enabled = false;

        public void Show() =>
            _canvas.enabled = true;

        public void SetCompletionStatus(bool isCompleted)
        {
            _completionIcon.sprite = isCompleted ? _completeSprite : _pendingSprite;
            _completionIcon.color = isCompleted ? HexToColor("00FF38") : HexToColor("FFAE00");
        }

        private Color32 HexToColor(string hex)
        {
            byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            return new Color32(r, g, b, 255); // Альфа устанавливаем в максимальное значение, т.е. 255
        }
    }
}