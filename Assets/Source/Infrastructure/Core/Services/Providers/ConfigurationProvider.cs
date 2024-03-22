using Source.Application.Configs;
using Sources.Infrastructure.Api.Services.Providers;

namespace Source.Infrastructure.Core.Services.Providers
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private readonly ConfigurationContainer _configurationContainer;

        public ConfigurationProvider(ConfigurationContainer configurationContainer)
        {
            _configurationContainer = configurationContainer;
        }

        public LevelViewSo LevelViewConfig => _configurationContainer.LevelViewConfig;
    }
}