using System;
using Modules.DAL.Abstract;
using Modules.DAL.Abstract.Data;

namespace Modules.DAL.Implementation.Data
{
    [Serializable]
    public class ProfileProgress : IEntity, ICloneable
    {
        public int StashedProgress;
        public int CurrentProgress;

        public string Id => nameof(ProfileProgress);
        public int Total => StashedProgress + CurrentProgress;

        public object Clone()
        {
            return new ProfileProgress()
            {
                CurrentProgress = CurrentProgress,
                StashedProgress = StashedProgress,
            };
        }
    }
}