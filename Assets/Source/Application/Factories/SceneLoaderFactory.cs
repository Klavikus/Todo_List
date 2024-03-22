using Source.Infrastructure.Core;
using Sources.Infrastructure.Api.Services;
using UnityEngine;

namespace Sources.Application.Factories
{
    internal class SceneLoaderFactory
    {
        private GameObject _gameObject;

        public SceneLoader Create()
        {
            _gameObject = new GameObject(nameof(SceneLoader));
            
            ICoroutineRunner coroutineRunner = _gameObject.AddComponent<CoroutineRunner>();
            
            return new SceneLoader(coroutineRunner);
        }
    }
}