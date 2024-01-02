using LaunchMyHub.Models;

namespace LaunchMyHub.DTOs
{
    public class PaginationViewModel
    {
        public PaginatedList<HubMaster> HubMaster { get; set; }
        public string CurrentFilter { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public PaginationViewModel(PaginatedList<HubMaster> hubMaster, string currentFilter)
        {
            HubMaster = hubMaster;
            CurrentFilter = currentFilter;
            CurrentPage = hubMaster.PageIndex;
            TotalPages = hubMaster.TotalPages;
        }
    }

}
