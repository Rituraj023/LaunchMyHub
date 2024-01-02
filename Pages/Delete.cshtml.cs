using LaunchMyHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LaunchMyHub.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly LaunchMyHub.Data.ApplicationDbContext _context;

        public DeleteModel(LaunchMyHub.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.HubMasters == null)
            {
                return NotFound();
            }
            var hubmaster = await _context.HubMasters.FindAsync(id);

            if (hubmaster != null)
            {
                HubMaster = hubmaster;
                _context.HubMasters.Remove(HubMaster);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
