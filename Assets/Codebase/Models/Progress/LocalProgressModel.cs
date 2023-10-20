using Assets.Codebase.Models.Base;
using Assets.Codebase.Models.Progress.Data;
using Assets.Codebase.Utils.Extensions;
using UnityEngine;

namespace Assets.Codebase.Models.Progress
{
    /// <summary>
    /// Model for local saving case (PlayerPrefs).
    /// </summary>
    public class LocalProgressModel : BaseModel, IProgressModel
    {
        private const string ProgressKey = "Progress";

        public ReactiveProgress ReactiveProgress { get; private set; }

        private PersistantProgress _persistantProgress;

        protected override void InitModel()
        {
            LoadProgress();
        }

        protected bool CanFindSave()
        {
            return PlayerPrefs.HasKey(ProgressKey);
        }

        protected void CreateNewProgress()
        {
            ReactiveProgress = new ReactiveProgress();
        }

        public void SaveProgress()
        {
            // New PersistantProgress is created on first save
            if (_persistantProgress == null)
            {
                _persistantProgress = new PersistantProgress();
            }

            _persistantProgress.SetValues(ReactiveProgress);
            PlayerPrefs.SetString(ProgressKey, _persistantProgress.ToJson());
        }

        public void LoadProgress()
        {
            if (CanFindSave())
            {
                GetProgressFromPrefs();
            }
            else
            {
                CreateNewProgress();
            }
        }

        private void GetProgressFromPrefs()
        {
            var progress = PlayerPrefs.GetString(ProgressKey).ToDeserealized<PersistantProgress>();
            ReactiveProgress = new ReactiveProgress(progress);
        }
    }
}
