using Source.Infrastructure.Api;
using UnityEngine;

namespace Source.Application
{
    internal class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        private void Awake() => 
            DontDestroyOnLoad(this);
    }
}