using Source.Domain.Configs;

namespace Source.Infrastructure.Api.Services.Providers
{
    public interface IConfigurationProvider
    {
        LevelViewSo LevelViewConfig { get; }
    }
}