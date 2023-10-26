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
        [SerializeField] private TMP_Text _headerTitle;
        [SerializeField] private TMP_Text _totalCountText;
        [SerializeField] private TMP_Text _recordPerTrainingCount;
        [SerializeField] private TMP_Text _trainingsCount;
        [SerializeField] private TMP_Text _caloriesCount;

        public StatsPeriod Period => _period;

        public void SetData(StatsWidgetInfo data)
        {
            _headerTitle.text = data.Header;
            _totalCountText.text = data.TotalPushups;
            _recordPerTrainingCount.text = data.RecordPerTraining;
            _trainingsCount.text = data.TrainingsCount;
            _caloriesCount.text = data.CaloriesCount;
        }
    }
}
