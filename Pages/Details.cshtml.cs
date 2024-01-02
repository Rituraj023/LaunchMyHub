using LaunchMyHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LaunchMyHub.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly LaunchMyHub.Data.ApplicationDbContext _context;

        public DetailsModel(LaunchMyHub.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public HubMaster HubMaster { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.HubMasters == null)
            {
                return NotFound();
            }

            var hubmaster = await _context.HubMasters.FirstOrDefaultAsync(m => m.Id == id);
            if (hubmaster == null)
            {
                return NotFound();
            }
            else 
            {
                HubMaster = hubmaster;
            }
            return Page();
        }
    }
}
