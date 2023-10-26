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

        public void SetData(string header, string totalPushaps, string record, string trainingsCount, string caloriesCount)
        {
            _headerTitle.text = header;
            _totalCountText.text = totalPushaps;
            _recordPerTrainingCount.text = record;
            _trainingsCount.text = trainingsCount;
            _caloriesCount.text = caloriesCount;
        }
    }
}
