using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.Localization;
using Assets.Codebase.Models.Progress.Data.TrainingPlans;
using Assets.Codebase.Utils.Extensions;
using Assets.Codebase.Utils.Values;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets.Codebase.Views.PlanPreview
{
    /// <summary>
    /// Training day description in plan preview
    /// </summary>
    public class TrainingDayWidget : MonoBehaviour
    {
        [SerializeField] private TMP_Text _trainingDayId;
        [SerializeField] private TMP_Text _pushupsList;
        [SerializeField] private TMP_Text _restingTime;
        [SerializeField] private TMP_Text _totalPushupsCount;

        //private TrainingDay _trainingDay;

        public void Init(TrainingDay trainingDay, int dayId)
        {
            var localizationService = ServiceLocator.Container.Single<ILocalizationService>();

            _trainingDayId.text = dayId.ToString();
            _pushupsList.text = trainingDay.Pushups.ToPushupsListString();
            _restingTime.text = trainingDay.RestingTime.ToString() + localizationService.LocalizeTextByKey("hours_word");
            _totalPushupsCount.text = localizationService.LocalizeTextByKey(Constants.TotalWithCountKey) + trainingDay.Pushups.Sum().ToString();
        }
    }
}
