using System;
using System.Collections.Generic;
using Source.Infrastructure.Api;
using Source.Presentation.Core.LevelSelection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Infrastructure.Core.Services.Providers
{
    public class ResourceProvider : IResourceProvider
    {
        private static readonly Dictionary<Type, string> ResourcePathByType = new()
        {
            [typeof(ConfigurationContainer)] = "ConfigurationContainer",
            [typeof(LevelView)] = $"{nameof(LevelView)}_template",
            [typeof(LevelStageView)] = $"{nameof(LevelStageView)}_template",
        };

        public T Load<T>() where T : Object
        {
            Debug.Log($"Load asset from {ResourcePathByType[typeof(T)]}");

            return Resources.Load<T>(ResourcePathByType[typeof(T)]);
        }
    }
}