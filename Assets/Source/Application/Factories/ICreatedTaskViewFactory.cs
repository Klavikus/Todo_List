using Modules.DAL.Implementation.Data;
using Modules.DAL.Implementation.Data.Entities;
using Source.Presentation.Api;
using UnityEngine;

namespace Source.Application.Factories
{
    public interface ICreatedTaskViewFactory
    {
        ICreatedTaskView Create(TaskData taskData, Transform parentContainer);
    }
}