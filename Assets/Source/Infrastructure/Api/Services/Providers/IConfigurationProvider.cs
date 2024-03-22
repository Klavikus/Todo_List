using Source.Application.Configs;

namespace Sources.Infrastructure.Api.Services.Providers
{
    public interface IConfigurationProvider
    {
        LevelViewSo LevelViewConfig { get; }
    }
}