namespace Application.Common.Models
{
    public class BaseSearchModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string[] IncludeList { get; set; }
    }
}