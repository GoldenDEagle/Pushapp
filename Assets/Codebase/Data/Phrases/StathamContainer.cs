using System.Collections.Generic;
using UnityEngine;

namespace Assets.Codebase.Data.Phrases
{
    [CreateAssetMenu(menuName = "Create Statham phrases", fileName = "StathamPhrases", order = 51)]
    public class StathamContainer : ScriptableObject
    {
        [SerializeField] private List<string> _phrases;

        public List<string> Phrases => _phrases;
    }
}
