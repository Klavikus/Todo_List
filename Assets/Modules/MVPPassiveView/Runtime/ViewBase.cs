using System;
using UnityEngine;

namespace Modules.MVPPassiveView.Runtime
{
    public abstract class ViewBase : MonoBehaviour, IView
    {
        private IPresenter _presenter;

        public void Construct(IPresenter presenter)
        {
            if (presenter == null) 
                throw new ArgumentNullException(nameof(presenter));

            gameObject.SetActive(false);
            
            OnBeforeConstruct();
            
            _presenter = presenter;
            gameObject.SetActive(true);
            _presenter.Enable();

            OnAfterConstruct();
        }

        public virtual void OnBeforeConstruct()
        {
        }

        public virtual void OnAfterConstruct()
        {
        }

        private void OnDestroy() =>
            _presenter?.Disable();
    }
}