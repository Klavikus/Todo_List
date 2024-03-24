using TMPro;
using UnityEngine.UI;

namespace Source.Presentation.Api
{
    public interface ICreatedTaskView
    {
        TMP_Text Name { get; }
        Image StatusImage { get; }
    }
}