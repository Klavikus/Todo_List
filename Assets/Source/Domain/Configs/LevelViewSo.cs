using Source.Domain.Data;
using UnityEngine;

namespace Source.Application.Configs
{
    [CreateAssetMenu(menuName = "Create LevelViewSo", fileName = "LevelViewSo", order = 0)]
    public class LevelViewSo : ScriptableObject
    {
        [field: SerializeField] public LevelViewData[] LevelViewsData;
    }
}