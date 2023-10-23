using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.Training;
using Assets.Codebase.Views.Base;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.Training
{
    public class TrainingView : BaseView
    {
        [SerializeField] private Button _backButton;

        private ITrainingPresenter _presenter;

        public override void Init(IPresenter presenter)
        {
            _presenter = presenter as ITrainingPresenter;

            base.Init(presenter);
        }

        protected override void SubscribeToUserInput()
        {
            _backButton.OnClickAsObservable().Subscribe(_ => _presenter.BackToMenu()).AddTo(CompositeDisposable);
        }
    }
}
