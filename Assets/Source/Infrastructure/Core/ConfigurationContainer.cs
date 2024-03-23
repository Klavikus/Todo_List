using Source.Domain.Configs;
using UnityEngine;

namespace Source.Infrastructure.Core
{
    [CreateAssetMenu(menuName = "Data/Create ConfigurationContainer", fileName = "ConfigurationContainer", order = 0)]
    public class ConfigurationContainer : ScriptableObject
    {

        [field: SerializeField] public LevelViewSo LevelViewConfig { get; private set; }
    }
}