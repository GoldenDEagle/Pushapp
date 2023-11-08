using Assets.Codebase.Utils.CustomTypes;
using System;
using System.Collections.Generic;

namespace Assets.Codebase.Data.Trainings
{
    [Serializable]
    public class TrainingResult
    {
        private List<int> _pushupAttempts;
        private SerializableDateTime _date;
        private int _totalPushups;
        private float _trainingDuration;

        public List<int> PushupAttempts => _pushupAttempts;
        public SerializableDateTime Date => _date;
        public int TotalPushups => _totalPushups;
        public float TrainingDuration => _trainingDuration;

        public TrainingResult(List<int> attempts, int totalPushups, DateTime date, float trainingDuration = 0f)
        {
            _pushupAttempts = attempts;
            _date = new SerializableDateTime(date);
            _totalPushups = totalPushups;
            _trainingDuration = trainingDuration;
        }

    }
}
