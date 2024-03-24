using Modules.DAL.Implementation.Data;
using Source.Presentation.Api;
using UnityEngine;

namespace Source.Application.Factories
{
    public interface ICreatedTaskViewFactory
    {
        ICreatedTaskView Create(TaskData taskData, Transform parentContainer);
    }
}