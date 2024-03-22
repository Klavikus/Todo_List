using Cysharp.Threading.Tasks;

namespace Assets.Source.Common.Components.Interfaces
{
    public interface ITweenActionBaseComponent
    {
        void Initialize();
        void Cancel();
        UniTask PlayForward();
        UniTask PlayBackward();
        void SetForwardState();
        void SetBackwardState();
    }
}