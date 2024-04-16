using Source.Application.Builders;
using UnityEngine;
using Zenject;

namespace Source.Application.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        private Game _game;

        [Inject]
        private void Construct(Game game)
        {
            DontDestroyOnLoad(this);
            
            _game = game;
        }

        private void Start() =>
            _game.Run();

        private void Update() =>
            _game.Update();

        private void OnDestroy() =>
            _game.Finish();
    }
}