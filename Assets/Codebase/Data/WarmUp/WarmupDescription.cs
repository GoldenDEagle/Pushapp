using System.Collections.Generic;
using UnityEngine;

namespace Assets.Codebase.Data.WarmUp
{
    [CreateAssetMenu(menuName = "Create Warmup Description", fileName = "WarmupDescription", order = 51)]
    public class WarmupDescription : ScriptableObject
    {
        [SerializeField] private List<WarmupStep> _steps;

        public List<WarmupStep> Steps => _steps;
    }
}
