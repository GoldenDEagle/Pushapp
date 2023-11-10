namespace Assets.Codebase.Data.Statistics
{
    public class StatsWidgetInfo
    {
        private string _headerTitle;
        private string _totalPushupCount;
        private string _recordPerTrainingCount;
        private string _trainingsCount;
        private string _caloriesCount;
        private string _maxLevel;
        private string _time;

        public string Header => _headerTitle;
        public string TotalPushups => _totalPushupCount;
        public string RecordPerTraining => _recordPerTrainingCount;
        public string TrainingsCount => _trainingsCount;
        public string CaloriesCount => _caloriesCount;
        public string MaxLevel => _maxLevel;
        public string Time => _time;

        public StatsWidgetInfo(string totalPushaps, string record, string trainingsCount, string caloriesCount, string maxLevel, string time)
        {
            _totalPushupCount = totalPushaps;
            _recordPerTrainingCount = record;
            _trainingsCount = trainingsCount;
            _caloriesCount = caloriesCount;
            _maxLevel = maxLevel;
            _time = time;
        }
    }
}
