using System;
using UnityEngine;

namespace Assets.Codebase.Data.Achievements
{
    [Serializable]
    public class Achievement
    {
        [SerializeField] private int _id;
        [SerializeField] private string _tag;

        public Achievement(int id, string tag)
        {
            _id = id;
            _tag = tag;
        }
    }
}
