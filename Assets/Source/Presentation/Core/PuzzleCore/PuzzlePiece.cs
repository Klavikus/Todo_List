using UnityEngine;
using UnityEngine.EventSystems;

namespace Source.Presentation.Core.PuzzleCore
{
    public class PuzzlePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler
    {
        private Transform _container;
        private SpriteRenderer _spriteRenderer;
        private Camera _camera;

        public void Initialize(Transform container)
        {
            _container = container;
            _camera = Camera.main;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _container.Translate(-3 * Vector3.forward);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _container.position += _camera.ScreenToWorldPoint(eventData.delta) -
                                   _camera.ScreenToWorldPoint(Vector2.zero);
        }

        public void OnDrop(PointerEventData eventData)
        {
            _container.Translate(3 * Vector3.forward);
        }
    }
}