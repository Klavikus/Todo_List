using Source.Application.Factories;
using UnityEngine;
using Zenject;

namespace Source.Application.CompositionRoots
{
    public class MainMenuCompositionRoot : MonoBehaviour
    {
        [Inject]
        public void Initialize(ISceneFactory sceneFactory)
        {
            sceneFactory.CreateMainScene();
        }
    }
}