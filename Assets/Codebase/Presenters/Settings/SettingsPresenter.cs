using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Views.Base;

namespace Assets.Codebase.Presenters.Settings
{
    public class SettingsPresenter : BasePresenter, ISettingsPresenter
    {
        public SettingsPresenter()
        {
            ViewId = ViewId.SettingsView;
        }
    }
}
