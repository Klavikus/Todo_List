using Modules.DAL.Implementation.Data.Entities;
using Source.Presentation.Api.Views;
using UnityEngine;

namespace Source.Presentation.Api.Factories
{
    public interface ICreatedTaskViewFactory
    {
        ICreatedTaskView Create(TaskData taskData, Transform parentContainer);
    }
}