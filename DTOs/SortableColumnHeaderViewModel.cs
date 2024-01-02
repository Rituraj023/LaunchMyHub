namespace LaunchMyHub.DTOs
{
    public class SortableColumnHeaderViewModel
    {
        public string HeaderTitle { get; set; }
        public string SortField { get; set; }
        public string CurrentSortField { get; set; }
        public string CurrentSortOrder { get; set; }
        public string CurrentFilter { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public SortableColumnHeaderViewModel(string headerTitle, string sortField, string currentSortField, string currentSortOrder, string currentFilter, int currentPage, int pageSize)
        {
            HeaderTitle = headerTitle;
            SortField = sortField;
            CurrentSortField = currentSortField;
            CurrentSortOrder = currentSortOrder;
            CurrentFilter = currentFilter;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }
    }

}
