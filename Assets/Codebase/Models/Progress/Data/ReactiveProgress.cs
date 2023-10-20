using Assets.Codebase.Models.Progress.Data.TrainingPlans;
using UniRx;

namespace Assets.Codebase.Models.Progress.Data
{
    /// <summary>
    /// Used at runtime.
    /// </summary>
    public class ReactiveProgress
    {
        // All the properties that need to be saved...

        public ReactiveProperty<int> SampleValue;
        public ReactiveProperty<float> SoundVolume;
        public ReactiveProperty<bool> IsTrainingPlanSelected;
        public ReactiveProperty<TrainingPlan> CurrentTrainingPlan;
        public ReactiveProperty<int> CurrentTrainingDay;

        // .

        /// <summary>
        /// Creates new progress with default values.
        /// </summary>
        public ReactiveProgress()
        {
            SampleValue = new ReactiveProperty<int>(0);
            SoundVolume = new ReactiveProperty<float>(0.5f);
            IsTrainingPlanSelected = new ReactiveProperty<bool>(false);
            CurrentTrainingPlan = new ReactiveProperty<TrainingPlan>();
            CurrentTrainingDay = new ReactiveProperty<int>(0);
        }

        /// <summary>
        /// Creates new progress from persistant data.
        /// </summary>
        /// <param name="progress"></param> Progress to initialize from
        public ReactiveProgress(PersistantProgress progress)
        {
            SampleValue = new ReactiveProperty<int>(progress.SampleValue);
            SoundVolume = new ReactiveProperty<float>(progress.SoundVolume);
            IsTrainingPlanSelected = new ReactiveProperty<bool>(progress.IsTrainingPlanSelected);
            CurrentTrainingPlan = new ReactiveProperty<TrainingPlan>(progress.CurrentTrainingPlan);
            CurrentTrainingDay = new ReactiveProperty<int>(progress.CurrentTrainingDay);
        }
    }
}
