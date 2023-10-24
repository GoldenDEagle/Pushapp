using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.Settings;
using Assets.Codebase.Views.Base;
using System;

namespace Assets.Codebase.Views.Settings
{
    public class SettingsView : BaseView
    {
        private ISettingsPresenter _presenter;

        public override void Init(IPresenter presenter)
        {
            _presenter = presenter as ISettingsPresenter;

            base.Init(presenter);
        }

        protected override void SubscribeToUserInput()
        {
            throw new NotImplementedException();
        }
    }
}
