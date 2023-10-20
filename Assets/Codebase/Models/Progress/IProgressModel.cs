using Assets.Codebase.Models.Progress.Data;

namespace Assets.Codebase.Models.Progress
{
    /// <summary>
    /// Model that contains all data that needs to be saved.
    /// </summary>
    public interface IProgressModel
    {
        /// <summary>
        /// Contains all progress data and notifies presenters.
        /// </summary>
        public ReactiveProgress ReactiveProgress { get; }

        /// <summary>
        /// Creates a save.
        /// </summary>
        public void SaveProgress();

        /// <summary>
        /// Loads data from saving place.
        /// </summary>
        public void LoadProgress();
    }
}
