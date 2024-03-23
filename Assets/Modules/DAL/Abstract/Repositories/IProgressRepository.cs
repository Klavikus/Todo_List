using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Modules.DAL.Abstract.Data;

namespace Modules.DAL.Abstract.Repositories
{
    public interface IProgressRepository
    {
        IEnumerable<Type> HandledTypes { get; }

        T GetById<T>(string id) where T : class, IEntity;
        IEntity Add<T>(IEntity entity) where T : class, IEntity;
        List<T> GetAll<T>() where T : class, IEntity;
        List<IEntity> GetAll(Type type);
        UniTask Load();
        UniTask Save();
        void CopyFrom(IProgressRepository progressRepository);
        void Clear();
    }
}