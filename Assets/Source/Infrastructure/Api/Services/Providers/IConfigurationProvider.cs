using Source.Presentation.Api;

namespace Source.Infrastructure.Api.Services.Providers
{
    public interface IConfigurationProvider
    {
        ICreatedTaskView CreatedTaskViewPrefab { get; }
    }
}