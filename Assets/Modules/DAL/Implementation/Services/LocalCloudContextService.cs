using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Modules.DAL.Abstract.Repositories;
using Modules.DAL.Abstract.Services;
using Modules.DAL.Implementation.Data;
using Modules.DAL.Implementation.Data.Entities;
using UnityEngine;

namespace Modules.DAL.Implementation.Services
{
    public class LocalCloudContextService : ILocalCloudContextService
    {
        private readonly IProgressRepository _localProgressRepository;
        private readonly IProgressRepository _cloudProgressRepository;

        public LocalCloudContextService(
            IProgressRepository localProgressRepository,
            IProgressRepository cloudProgressRepository
        )
        {
            _localProgressRepository = localProgressRepository;
            _cloudProgressRepository = cloudProgressRepository;
        }

        public UniTask LoadLocalContext() =>
            _localProgressRepository.Load();

        public UniTask LoadCloudContext() =>
            _cloudProgressRepository.Load();

        public async UniTask SaveLocalContext()
        {
            SyncData localSyncData = _localProgressRepository.GetAll<SyncData>().FirstOrDefault();

            if (localSyncData == null)
            {
                localSyncData = new SyncData();
                _localProgressRepository.Add<SyncData>(localSyncData);
            }

            localSyncData.SyncCount++;

            await _localProgressRepository.Save();
        }

        public UniTask SaveCloudContext() =>
            _cloudProgressRepository.Save();

        public bool CheckUpdateFromCloud()
        {
            List<SyncData> cloudSyncData = _cloudProgressRepository.GetAll<SyncData>() ?? new List<SyncData>();
            List<SyncData> localSyncData = _localProgressRepository.GetAll<SyncData>() ?? new List<SyncData>();

            if (cloudSyncData.Count == 0)
                return false;

            if (localSyncData.Count == 0)
                return true;

            return cloudSyncData[0].SyncCount > localSyncData[0].SyncCount && IsLevelProgressDifferent();
        }

        public bool CheckUpdateFromLocal()
        {
            List<SyncData> cloudSyncData = _cloudProgressRepository.GetAll<SyncData>() ?? new List<SyncData>();
            List<SyncData> localSyncData = _localProgressRepository.GetAll<SyncData>() ?? new List<SyncData>();

            if (localSyncData.Count == 0)
                return false;

            if (cloudSyncData.Count == 0)
                return true;

            return localSyncData[0].SyncCount > cloudSyncData[0].SyncCount && IsLevelProgressDifferent();
        }

        public void TransferCloudToLocal() =>
            _localProgressRepository.CopyFrom(_cloudProgressRepository);

        public void TransferLocalToCloud() =>
            _cloudProgressRepository.CopyFrom(_localProgressRepository);

        public async UniTask ClearLocalContext()
        {
            _localProgressRepository.Clear();
            await _localProgressRepository.Save();
        }

        public async UniTask ClearCloudContext()
        {
            _cloudProgressRepository.Clear();
            await _cloudProgressRepository.Save();
        }

        private bool IsLevelProgressDifferent()
        {
            List<LevelProgress> cloudProgressData =
                _cloudProgressRepository.GetAll<LevelProgress>() ?? new List<LevelProgress>();
            List<LevelProgress> localProgressData =
                _localProgressRepository.GetAll<LevelProgress>() ?? new List<LevelProgress>();

            int cloudProgressTotal = cloudProgressData.Sum(data => data.LevelCurrency);
            int localProgressTotal = localProgressData.Sum(data => data.LevelCurrency);

            Debug.Log($"Cloud total progress {cloudProgressTotal}");
            Debug.Log($"Local total progress {localProgressTotal}");

            return cloudProgressTotal != localProgressTotal;
        }
    }
}