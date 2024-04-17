using Source.Presentation.Core.Views;
using UnityEngine;

namespace Source.Presentation.Core.Configs
{
    [CreateAssetMenu(menuName = "Data/Create PresentationConfiguration", fileName = "PresentationConfiguration", order = 0)]
    public class PresentationConfiguration : ScriptableObject
    {
        [field: SerializeField] public CreatedTaskView CreatedTaskViewPrefab { get; private set; }
    }
}