using Assets.Codebase.Presenter.MainMenu;
using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Utils.Helpers;
using Assets.Codebase.Views.Base;
using Cysharp.Threading.Tasks;
using System;
using System.Diagnostics;
using UniRx;

namespace Assets.Codebase.Presenters.Example
{
    // Example presenter
    public class ExamplePresenter : BasePresenter, IExamplePresenter
    {
        private const string StartText = "Start";
        private const string StopText = "Stop";

        // Reactive property for each changable window element
        public ReactiveProperty<string> StartButtonText { get; private set; }
        public ReactiveProperty<string> SampleValueAmountString {  get; private set; } 

        public ExamplePresenter()
        {
            // Corresponding view id
            ViewId = ViewId.ExampleView;

            // Init all properties
            StartButtonText = new ReactiveProperty<string>(StartText);
            SampleValueAmountString = new ReactiveProperty<string>(0.ToString());
        }

        protected override void SubscribeToModelChanges()
        {
            // Subscribe to each model parameter of interest
            ProgressModel.ReactiveProgress.SampleValue.Subscribe(OnSampleValueChanged).AddTo(CompositeDisposable);
        }

        private void OnSampleValueChanged(int newGoldAmount)
        {
            SampleValueAmountString.Value = newGoldAmount.ToString();
        }

        public void OnStartButtonClicked()
        {
            if (StartButtonText.Value == StartText)
            {
                StartButtonText.Value = StopText;
            }
            else
            {
                StartButtonText.Value = StartText;
            }

            ProgressModel.ReactiveProgress.SampleValue.Value++;

            SimpleTimer().Forget();
        }

        private async UniTaskVoid SimpleTimer()
        {
            UnityEngine.Debug.Log("Starting 5 sec timer");
            // Preparation
            var timer = 5f;
            while (timer >= 0)
            {
                UnityEngine.Debug.Log(timer.ToString());
                timer--;
                await UniTask.Delay(TimeSpan.FromSeconds(1));
            }

            UnityEngine.Debug.Log("Starting 5 sec timer");
            // Exercise
            timer = 3f;
            while (timer >= 0)
            {
                UnityEngine.Debug.Log(timer.ToString());
                timer--;
                await UniTask.Delay(TimeSpan.FromSeconds(1));
            }

            UnityEngine.Debug.Log("Completed!");

            SimpleTimer().Forget();
        }
    }
}
