namespace HR.WebApi.ModelView
{
    public class SearchBy
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string Parameter { get; set; }
        public string OrderBy { get; set; }
        public int RecordLimit { get; set; } = 500;
    }
}
