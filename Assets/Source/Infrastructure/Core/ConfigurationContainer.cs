using Source.Presentation.Core;
using UnityEngine;

namespace Source.Infrastructure.Core
{
    [CreateAssetMenu(menuName = "Data/Create ConfigurationContainer", fileName = "ConfigurationContainer", order = 0)]
    public class ConfigurationContainer : ScriptableObject
    {
        [field: SerializeField] public CreatedTaskView CreatedTaskViewPrefab { get; private set; }
    }
}