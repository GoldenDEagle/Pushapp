using Assets.Codebase.Data.Tutorial;
using UnityEngine;

namespace Assets.Codebase.Views.Common
{
    public class CanvasWithHoles : MonoBehaviour
    {
        [SerializeField] private RectTransform _step1Holes;
        [SerializeField] private RectTransform _step2Holes;
        [SerializeField] private RectTransform _step3Holes;
        [SerializeField] private RectTransform _step4Holes;

        public void ShowStep(TutorialStep step)
        {
            switch (step)
            {
                case TutorialStep.None:
                    _step1Holes.gameObject.SetActive(false);
                    _step2Holes.gameObject.SetActive(false);
                    _step3Holes.gameObject.SetActive(false);
                    _step4Holes.gameObject.SetActive(false);
                    break;
                case TutorialStep.Step1:
                    _step1Holes.gameObject.SetActive(true);
                    _step2Holes.gameObject.SetActive(false);
                    _step3Holes.gameObject.SetActive(false);
                    _step4Holes.gameObject.SetActive(false);
                    break;
                case TutorialStep.Step2:
                    _step1Holes.gameObject.SetActive(false);
                    _step2Holes.gameObject.SetActive(true);
                    _step3Holes.gameObject.SetActive(false);
                    _step4Holes.gameObject.SetActive(false);
                    break;
                case TutorialStep.Step3:
                    _step1Holes.gameObject.SetActive(false);
                    _step2Holes.gameObject.SetActive(false);
                    _step3Holes.gameObject.SetActive(true);
                    _step4Holes.gameObject.SetActive(false);
                    break;
                case TutorialStep.Step4:
                    _step1Holes.gameObject.SetActive(false);
                    _step2Holes.gameObject.SetActive(false);
                    _step3Holes.gameObject.SetActive(false);
                    _step4Holes.gameObject.SetActive(true);
                    break;
            }
        }
    }
}
