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

        public List<int> PushupAttempts => _pushupAttempts;
        public SerializableDateTime Date => _date;
        public int TotalPushups => _totalPushups;

        public TrainingResult(List<int> attempts, int totalPushups, DateTime date)
        {
            _pushupAttempts = attempts;
            _date = new SerializableDateTime(date);
            _totalPushups = totalPushups;
        }

    }
}
