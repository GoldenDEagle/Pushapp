using Assets.Codebase.Data.Statistics;
using TMPro;
using UnityEngine;

namespace Assets.Codebase.Views.Statistics
{
    public class StatsWidget : MonoBehaviour
    {
        [Header("Select period")]
        [SerializeField] private StatsPeriod _period;
        [Header("Info fields")]
        [SerializeField] private TMP_Text _totalCountText;
        [SerializeField] private TMP_Text _recordPerTrainingCount;
        [SerializeField] private TMP_Text _trainingsCount;
        [SerializeField] private TMP_Text _caloriesCount;
        [SerializeField] private TMP_Text _maxLevel;
        [SerializeField] private TMP_Text _totalTime;

        public StatsPeriod Period => _period;

        public void SetData(StatsWidgetInfo data)
        {
            _totalCountText.text = data.TotalPushups;
            _recordPerTrainingCount.text = data.RecordPerTraining;
            _trainingsCount.text = data.TrainingsCount;
            _caloriesCount.text = data.CaloriesCount;
            _maxLevel.text = data.MaxLevel;
            _totalTime.text = data.Time;
        }
    }
}
