using System;

namespace Source.Presentation.Core
{
    public interface IViewBase
    {
        event Action Enabled;
        event Action Disabled;
        void Activate();
    }
}