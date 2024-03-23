using Source.Controllers.Api.Services;
using Source.Domain;
using Source.Domain.Data;
using Source.Infrastructure.Api.Services.Providers;

namespace Source.Infrastructure.Core.Factories
{
    public class LevelModelFactory : ILevelModelFactory
    {
        private readonly IConfigurationProvider _configurationProvider;
        private readonly IPersistentDataService _persistentDataService;

        public LevelModelFactory
        (
            IConfigurationProvider configurationProvider,
            IPersistentDataService persistentDataService
        )
        {
            _configurationProvider = configurationProvider;
            _persistentDataService = persistentDataService;
        }

        public Level[] Create()
        {
            LevelViewData[] levelViewsData = _configurationProvider.LevelViewConfig.LevelViewsData;

            Level[] result = new Level[levelViewsData.Length];

            for (int i = 0; i < levelViewsData.Length; i++)
                result[i] = levelViewsData[i].ToModel();

            return result;
        }

        public Level[] ApplyProgress(Level[] levels)
        {
            foreach (Level level in levels)
            {
                LevelProgress levelProgress = _persistentDataService.GetLevelProgress(level);

                if (levelProgress != null)
                    level.ApplyLevelProgress(levelProgress);
            }

            return levels;
        }
    }
}