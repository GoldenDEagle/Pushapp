using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.Warmup;
using Assets.Codebase.Views.Base;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.Warmup
{
    public class WarmUpView : BaseView
    {
        [SerializeField] private Button _skipButton;

        private IWarmUpPresenter _presenter;

        public override void Init(IPresenter presenter)
        {
            _presenter = presenter as IWarmUpPresenter;

            base.Init(presenter);
        }

        protected override void SubscribeToUserInput()
        {
            _skipButton.OnClickAsObservable().Subscribe(_ => _presenter.SkipWarmup()).AddTo(CompositeDisposable);
        }
    }
}