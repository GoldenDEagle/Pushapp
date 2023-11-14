using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Codebase.Data.WarmUp
{
    [Serializable]
    public class SimpleAnimationClip
    {
        [SerializeField] private List<Sprite> _sprites;

        public List<Sprite> Sprites => _sprites;
    }
}
