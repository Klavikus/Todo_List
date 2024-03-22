using Source.Domain;

namespace Source.Infrastructure.Core.Factories
{
    public interface ILevelModelFactory
    {
        Level[] Create();
        Level[] ApplyProgress(Level[] levels);
    }
}