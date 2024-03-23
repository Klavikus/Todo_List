using System;
using Source.Controllers.Api.Services;
using Source.Domain;
using Source.Domain.Data;
using Source.Infrastructure.Api.Services;
using UnityEngine;

namespace Source.Controllers.Core.Services
{
    public class PersistentDataService : IPersistentDataService
    {
        private readonly ISaveService _saveService;

        public PersistentDataService(ISaveService saveService)
        {
            _saveService = saveService;
        }

        public void UpdateLevelProgress(Level level)
        {
            if (level == null) throw new ArgumentNullException(nameof(level));

            string jsonData = JsonUtility.ToJson(LevelProgress.FromModel(level));

            _saveService.Save(GetLevelProgressKey(level), jsonData);
        }

        public LevelProgress GetLevelProgress(Level level)
        {
            string jsonData = _saveService.Get(GetLevelProgressKey(level));

            LevelProgress levelProgress = JsonUtility.FromJson<LevelProgress>(jsonData);

            return levelProgress;
        }

        public void ClearData() =>
            _saveService.Clear();

        private string GetLevelProgressKey(Level level) =>
            $"{nameof(LevelProgress)}_{level.Id}";
    }
}