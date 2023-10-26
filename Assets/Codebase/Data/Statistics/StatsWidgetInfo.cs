namespace Assets.Codebase.Data.Statistics
{
    public class StatsWidgetInfo
    {
        private string _headerTitle;
        private string _totalPushupCount;
        private string _recordPerTrainingCount;
        private string _trainingsCount;
        private string _caloriesCount;

        public string Header => _headerTitle;
        public string TotalPushups => _totalPushupCount;
        public string RecordPerTraining => _recordPerTrainingCount;
        public string TrainingsCount => _trainingsCount;
        public string CaloriesCount => _caloriesCount;

        public StatsWidgetInfo(string header, string totalPushaps, string record, string trainingsCount, string caloriesCount)
        {
            _headerTitle = header;
            _totalPushupCount = totalPushaps;
            _recordPerTrainingCount = record;
            _trainingsCount = trainingsCount;
            _caloriesCount = caloriesCount;
        }
    }
}
