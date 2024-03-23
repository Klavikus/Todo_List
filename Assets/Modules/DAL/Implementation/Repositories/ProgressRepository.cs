using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Modules.DAL.Abstract.Data;
using Modules.DAL.Abstract.DataContexts;
using Modules.DAL.Abstract.Repositories;
using Modules.DAL.Implementation.Data;

namespace Modules.DAL.Implementation.Repositories
{
    public class ProgressRepository : IProgressRepository
    {
        private readonly Repository _levelRepository;
        private readonly Repository _syncDataRepository;
        private readonly Repository _profileRepository;
        private readonly Repository _menuStateRepository;

        private readonly Dictionary<Type, Func<IEntity, IEntity>> _addEntityStrategies;
        private readonly Dictionary<Type, Func<string, IEntity>> _getEntityByIdStrategies;
        private readonly Dictionary<Type, Func<IEnumerable<IEntity>>> _getAllStrategies;

        public ProgressRepository(IDataContext baseDataContext)
        {
            _levelRepository = new Repository(typeof(LevelProgress), baseDataContext);
            _syncDataRepository = new Repository(typeof(SyncData), baseDataContext);
            _profileRepository = new Repository(typeof(ProfileProgress), baseDataContext);
            _menuStateRepository = new Repository(typeof(MainMenuStateProgress), baseDataContext);

            _addEntityStrategies = new Dictionary<Type, Func<IEntity, IEntity>>
            {
                {typeof(LevelProgress), (entity) => _levelRepository.Add(entity as LevelProgress)},
                {typeof(SyncData), (entity) => _syncDataRepository.Add(entity as SyncData)},
                {typeof(ProfileProgress), (entity) => _profileRepository.Add(entity as ProfileProgress)},
                {typeof(MainMenuStateProgress), (entity) => _menuStateRepository.Add(entity as MainMenuStateProgress)},
            };

            _getEntityByIdStrategies = new Dictionary<Type, Func<string, IEntity>>
            {
                {typeof(LevelProgress), (id) => _levelRepository.GetById(id)},
                {typeof(SyncData), (id) => _syncDataRepository.GetById(id)},
                {typeof(ProfileProgress), (id) => _profileRepository.GetById(id)},
                {typeof(MainMenuStateProgress), (id) => _menuStateRepository.GetById(id)},
            };

            _getAllStrategies = new Dictionary<Type, Func<IEnumerable<IEntity>>>
            {
                {typeof(LevelProgress), () => _levelRepository.GetAll()},
                {typeof(SyncData), () => _syncDataRepository.GetAll()},
                {typeof(ProfileProgress), () => _profileRepository.GetAll()},
                {typeof(MainMenuStateProgress), () => _menuStateRepository.GetAll()},
            };
        }

        public IEntity Add<T>(IEntity entity) where T : class, IEntity =>
            _addEntityStrategies.TryGetValue(typeof(T), out Func<IEntity, IEntity> strategy)
                ? strategy?.Invoke(entity)
                : null;

        public IEnumerable<Type> HandledTypes { get; }

        public T GetById<T>(string id) where T : class, IEntity
        {
            if (_getEntityByIdStrategies.TryGetValue(typeof(T), out Func<string, IEntity> strategy))
                return strategy?.Invoke(id) as T;

            return null;
        }

        public List<T> GetAll<T>() where T : class, IEntity
        {
            if (_getAllStrategies.TryGetValue(typeof(T), out Func<IEnumerable<IEntity>> strategy))
                return strategy?.Invoke() as List<T>;

            return null;
        }

        public List<IEntity> GetAll(Type type)
        {
            if (_getAllStrategies.TryGetValue(type, out Func<IEnumerable<IEntity>> strategy))
                return strategy?.Invoke() as List<IEntity>;

            return null;
        }

        public UniTask Load() =>
            UniTask.WhenAll(
                _levelRepository.Load(),
                _syncDataRepository.Load(),
                _profileRepository.Load(),
                _menuStateRepository.Load()
            );

        public UniTask Save() =>
            UniTask.WhenAll(
                _levelRepository.Save(),
                _syncDataRepository.Save(),
                _profileRepository.Save(),
                _menuStateRepository.Save()
            );

        public void CopyFrom(IProgressRepository progressRepository)
        {
            _levelRepository.Clear();
            foreach (LevelProgress progress in progressRepository.GetAll<LevelProgress>())
                _levelRepository.Add(progress);

            _syncDataRepository.Clear();
            foreach (SyncData syncData in progressRepository.GetAll<SyncData>())
                _syncDataRepository.Add(syncData);

            _profileRepository.Clear();
            foreach (ProfileProgress profileProgress in progressRepository.GetAll<ProfileProgress>())
                _profileRepository.Add(profileProgress);

            _menuStateRepository.Clear();
            foreach (MainMenuStateProgress menuStateProgress in progressRepository.GetAll<MainMenuStateProgress>())
                _menuStateRepository.Add(menuStateProgress);
        }

        public void Clear()
        {
            _levelRepository.Clear();
            _syncDataRepository.Clear();
            _profileRepository.Clear();
            _menuStateRepository.Clear();
        }
    }
}