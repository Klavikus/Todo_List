using System;
using Modules.DAL.Abstract;
using Modules.DAL.Abstract.Data;

namespace Modules.DAL.Implementation.Data
{
    [Serializable]
    public class MainMenuStateProgress : IEntity, ICloneable
    {
        public int LastSelectedLevel;
        public int SelectedMusicVolume = 50;
        public int SelectedVFXVolume = 50;
        public bool IsMuted;

        public bool IsRandomOrderOn;
        public int SkinOptionId;
        public int SpeedOptionId;
        public int BackgroundOptionId;
        public int PulseOptionId;

        public MainMenuStateProgress()
        {
            Id = nameof(MainMenuStateProgress);
        }

        public string Id { get; }

        public object Clone()
        {
            return new MainMenuStateProgress()
            {
                LastSelectedLevel = LastSelectedLevel,
                SelectedMusicVolume = SelectedMusicVolume,
                SelectedVFXVolume = SelectedVFXVolume,
                IsMuted = IsMuted,
                IsRandomOrderOn = IsRandomOrderOn,
                SkinOptionId = SkinOptionId,
                SpeedOptionId = SpeedOptionId,
                BackgroundOptionId = BackgroundOptionId,
                PulseOptionId = PulseOptionId
            };
        }
    }
}