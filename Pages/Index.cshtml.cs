using LaunchMyHub.Data;
using LaunchMyHub.DTOs;
using LaunchMyHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LaunchMyHub.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _context = context;
        }

        public PaginatedList<HubMaster> HubMaster { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string CurrentSort { get; set; }

        [BindProperty(SupportsGet = true)]
        public string CurrentFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string CurrentSortColumn { get; set; }

        public async Task OnGetAsync(string currentFilter, string? searchString, int? pageIndex, string? sortColumn, string? sortOrder)
        {
            try
            {
                if (searchString != null)
                {
                    pageIndex = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                CurrentFilter = searchString;
                CurrentSort = sortOrder ?? "asc";
                CurrentSortColumn = sortColumn ?? "Org";

                
                IQueryable<HubMaster> hubMasterIQ = _context.HubMasters.Include(c=>c.ReferralSource).Include(c=>c.HubType);

                if (!string.IsNullOrEmpty(searchString))
                {
                    hubMasterIQ = hubMasterIQ.Where(s => s.ContactName.Contains(searchString)
                                                       || s.OrganizationName.Contains(searchString));
                }

                hubMasterIQ = SortHubMasters(hubMasterIQ, CurrentSortColumn, CurrentSort);

                HubMaster = await PaginatedList<HubMaster>.CreateAsync(hubMasterIQ.AsNoTracking(), pageIndex ?? 1, PageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching data for HubMasters.");
            }
        }

        private IQueryable<HubMaster> SortHubMasters(IQueryable<HubMaster> query, string sortColumn, string sortOrder)
        {
            switch (sortColumn)
            {
                case "Email":
                    return sortOrder == "asc" ? query.OrderBy(s => s.Email) : query.OrderByDescending(s => s.Email);
                case "Cont":
                    return sortOrder == "asc" ? query.OrderBy(s => s.ContactName) : query.OrderByDescending(s => s.ContactName);
                case "Org":
                    return sortOrder == "asc" ? query.OrderBy(s => s.OrganizationName) : query.OrderByDescending(s => s.ContactName);
                case "Phone":
                    return sortOrder == "asc" ? query.OrderBy(s => s.PhoneNumber) : query.OrderByDescending(s => s.PhoneNumber);
                case "FeeType":
                    return sortOrder == "asc" ? query.OrderBy(s => s.FeeType) : query.OrderByDescending(s => s.FeeType);
                case "NonProfit":
                    return sortOrder == "asc" ? query.OrderBy(s => s.NonProfit) : query.OrderByDescending(s => s.NonProfit);
                case "HubType":
                    return sortOrder == "asc" ? query.OrderBy(s => s.HubType.Name ?? "Other") : query.OrderByDescending(s => s.HubType.Name ?? "Other");
                case "Source":
                    return sortOrder == "asc" ? query.OrderBy(s => s.ReferralSource.Name ?? "Other") : query.OrderByDescending(s => s.ReferralSource.Name ?? "Other");

                // Add cases for other sortable columns
                default:
                    // Default sort logic, if needed
                    break;
            }

            return query; // Return the original query if no sorting is applied
        }
    }

}