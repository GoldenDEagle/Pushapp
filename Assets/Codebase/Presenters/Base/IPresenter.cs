using System;

namespace Assets.Codebase.Presenter.Base
{
    /// <summary>
    /// Common presenter functions.
    /// </summary>
    public interface IPresenter
    {
        public event Action OnCloseView;

        public void CloseView();
    }
}
