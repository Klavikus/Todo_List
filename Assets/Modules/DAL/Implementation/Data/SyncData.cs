using Modules.DAL.Abstract.Data;

namespace Modules.DAL.Implementation.Data
{
    public class SyncData : IEntity
    {
        public uint SyncCount;

        public SyncData()
        {
            Id = nameof(SyncData);
        }

        public string Id { get; }

        public object Clone() =>
            new SyncData() {SyncCount = SyncCount};
    }
}