using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Source.Application.Configs;
using Source.Controllers.Api.Services;
using Source.Domain.Data;
using Source.Presentation.Core.PuzzleCore;
using UnityEngine;

namespace Source.Presentation.Core
{
    public class PuzzleView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _targetElementPrefab;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _duration;
        [SerializeField] private float _strength;
        [SerializeField] private int _vibrato;
        [SerializeField] private float _randomness;
        [SerializeField] private bool _fadeOut;
        [SerializeField] private ShakeRandomnessMode _shakeRandomnessMode;

        private ILevelService _levelService;
        private LevelViewSo _levelViewConfig;
        private List<GameObject> _slicedElements;
        private SpriteRenderer _contourPad;

        public void Initialize(ILevelService levelService, LevelViewSo levelViewConfig)
        {
            _levelService = levelService;
            _levelViewConfig = levelViewConfig;

            _spriteRenderer = Instantiate(_targetElementPrefab);

            _levelService.LevelStageSelected += OnStageSelected;
            _levelService.LevelStarted += LevelServiceOnLevelStarted;
            _levelService.LevelClosed += LevelServiceOnLevelClosed;
        }

        private void OnDestroy()
        {
            if (_levelService == null)
                return;

            _levelService.LevelStageSelected -= OnStageSelected;
            _levelService.LevelStarted -= LevelServiceOnLevelStarted;
            _levelService.LevelClosed -= LevelServiceOnLevelClosed;
        }

        private async void LevelServiceOnLevelStarted(int obj)
        {
            GenerateSimplePolygonCollider(_spriteRenderer.gameObject, _spriteRenderer.sprite);
            GenerateSliceable();

            await UniTask.WaitForEndOfFrame(this);

            GenerateSpritePad();
            PerformSlice();

            SetPartsToNewPositions();
        }

        private void SetPartsToNewPositions()
        {
            _contourPad.transform.position =
                Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.75f, Camera.main.nearClipPlane));
            _contourPad.transform.localScale = Vector3.one * 0.5f;

            CreateCopies(_slicedElements, 0, _slicedElements.Count);
        }

        public void CreateCopies(List<GameObject> gameObjects, int topCount, int bottomCount)
        {
            if (gameObjects == null)
                return;

            int currentObjectId = 0;

            List<GameObject> newGameObjects = new List<GameObject>();

            foreach (GameObject o in gameObjects)
            {
                newGameObjects.Add(CenterPivot(o));
                var puzzlePiece = o.AddComponent<PuzzlePiece>();
                puzzlePiece.Initialize(puzzlePiece.transform.parent);
            }

            // // Создание копий в верхней части экрана
            // for(int i = 0; i < topCount; i++)
            // {
            //     var copyTop = Instantiate(baseObject);
            //     copyTop.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(
            //         (i + 1f) / (topCount + 1), 0.75f, Camera.main.nearClipPlane));
            //     copyTop.transform.localScale = baseObject.transform.localScale * 0.5f;
            // }

            // Создание копий в нижней части экрана
            for (int i = 0; i < bottomCount; i++)
            {
                var copyBottom = newGameObjects[currentObjectId];
                var newPosition = Camera.main.ViewportToWorldPoint(new Vector3(
                    (i + 1f) / (bottomCount + 1), 0.25f, Camera.main.nearClipPlane));
                newPosition.z = 0;
                copyBottom.transform.position = newPosition;
                copyBottom.transform.localScale = Vector3.one * 0.5f;

                currentObjectId++;
            }
        }

        private void GenerateSpritePad()
        {
            _contourPad = new GameObject().AddComponent<SpriteRenderer>();
            _contourPad.sprite = _spriteRenderer.sprite;
            _contourPad.color = Color.black;
            _contourPad.sortingOrder = _spriteRenderer.sortingOrder - 1;
            _contourPad.transform.position = _spriteRenderer.transform.position;
            _contourPad.transform.localScale = _spriteRenderer.transform.localScale;
        }

        private void PerformSlice()
        {

        }

        private void GenerateSliceable()
        {

        }

        public static GameObject CenterPivot(GameObject child)
        {
            if (child == null || child.GetComponent<MeshFilter>() == null) return null;

            // Создание родительского объекта
            GameObject parent = new GameObject(child.name + "_CenteredPivot");

            // Вычисление центра тяжести меша
            Vector3 center = child.GetComponent<MeshFilter>().mesh.bounds.center;

            // Перемещение ребенка в координаты центра его меша
            child.transform.localPosition = -center;

            // Помещение ребенка в родительский объект
            child.transform.SetParent(parent.transform);

            // Позиционирование родителя в исходное положение ребенка, с учетом нового смещения
            parent.transform.position = child.transform.position + center;

            return parent;
        }

        private PolygonCollider2D GenerateSimplePolygonCollider(GameObject obj, Sprite sprite)
        {
            if (obj == null || sprite == null)
                return null;

            PolygonCollider2D polygonCollider = obj.AddComponent<PolygonCollider2D>();
            polygonCollider.pathCount = 1;

            Vector2[] vertices = new Vector2[4];
            vertices[0] = new Vector2(-sprite.bounds.size.x / 2, -sprite.bounds.size.y / 2); // Bottom Left
            vertices[1] = new Vector2(-sprite.bounds.size.x / 2, sprite.bounds.size.y / 2); // Top Left
            vertices[2] = new Vector2(sprite.bounds.size.x / 2, sprite.bounds.size.y / 2); // Top Right
            vertices[3] = new Vector2(sprite.bounds.size.x / 2, -sprite.bounds.size.y / 2); // Bottom Right

            polygonCollider.SetPath(0, vertices);

            return polygonCollider;
        }

        private void LevelServiceOnLevelClosed(int obj)
        {
            if (_spriteRenderer == null)
            {
                _spriteRenderer = Instantiate(_targetElementPrefab);

                StageVariantData variantData = _levelViewConfig.LevelViewsData
                    .First(data => data.Order == _levelService.SelectedLevelId)
                    .StageVariantsData[_levelService.SelectedLevelStageId];
                _spriteRenderer.sprite = variantData.Icon;

                foreach (GameObject slicedElement in _slicedElements)
                {
                    Destroy(slicedElement.gameObject);
                }

                Destroy(_contourPad.gameObject);
            }
        }

        private void OnStageSelected(int levelId)
        {
            StageVariantData variantData = _levelViewConfig.LevelViewsData
                .First(data => data.Order == _levelService.SelectedLevelId)
                .StageVariantsData[_levelService.SelectedLevelStageId];

            // TODO: Вынести в отдельный ActionTween
            _spriteRenderer.sprite = variantData.Icon;
            _spriteRenderer.transform.localScale = Vector3.one * variantData.SizeMultiplayer;
            _spriteRenderer.transform.DOShakeScale(_duration, _strength, _vibrato, _randomness, _fadeOut,
                _shakeRandomnessMode);
        }
    }
}