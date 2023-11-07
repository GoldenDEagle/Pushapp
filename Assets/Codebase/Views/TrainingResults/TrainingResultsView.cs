﻿using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.TrainingResults;
using Assets.Codebase.Utils.Extensions;
using Assets.Codebase.Views.Base;
using Assets.Codebase.Views.Common;
using Cysharp.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.TrainingResults
{
    public class TrainingResultsView : BaseView
    {
        [SerializeField] private Button _goNextButton;
        [SerializeField] private UISwitch _stretchingToogle;

        [SerializeField] private TMP_Text _resultsLineText;

        private ITrainingResultsPresenter _presenter;

        public override void Init(IPresenter presenter)
        {
            _presenter = presenter as ITrainingResultsPresenter;

            base.Init(presenter);

            // Set default values
            _stretchingToogle.SetInitialState(_presenter.IsStretchingEnabled());
        }

        protected override void SubscribeToUserInput()
        {
            _goNextButton.OnClickAsObservable().Subscribe(_ => _presenter.GoNextClicked()).AddTo(CompositeDisposable);
            _stretchingToogle.OnSwitched.Subscribe(value => _presenter.StretchingToggleClicked(value)).AddTo(CompositeDisposable);
        }

        protected override void SubscribeToPresenterEvents()
        {
            base.SubscribeToPresenterEvents();

            _presenter.ResultsString.SubscribeToTMPText(_resultsLineText).AddTo(CompositeDisposable);
        }
    }
}
