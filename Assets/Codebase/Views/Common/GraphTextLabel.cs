using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Codebase.Views.Common
{
    public class GraphTextLabel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public void SetText(string labelText)
        {
            _text.text = labelText;
        }
    }
}