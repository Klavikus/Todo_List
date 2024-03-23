using UnityEngine;
using ILogger = Sources.Infrastructure.Api.Services.ILogger;

namespace Source.Infrastructure.Core.Services
{
    public class DebugLogger : ILogger
    {
        public void Log(string message) =>
            Debug.Log(message);

        public void LogWarning(string message) =>
            Debug.LogWarning(message);

        public void LogException(string message) =>
            Debug.LogError(message);
    }
}