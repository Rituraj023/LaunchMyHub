using AutoMapper;
using Humanizer;
using LaunchMyHub.DTOs;
using LaunchMyHub.Enums;
using LaunchMyHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LaunchMyHub.Pages
{
    public class EditModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly Data.ApplicationDbContext _context;

        public EditModel(Data.ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [BindProperty]
        public EditHubMasterViewModel HubMaster { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.HubMasters == null)
            {
                return NotFound();
            }

            var entity =  await _context.HubMasters.FirstOrDefaultAsync(m => m.Id == id);
            
            if (entity == null)
            {
                return NotFound();
            }

            HubMaster = _mapper.Map<EditHubMasterViewModel>(entity);

            if(!HubMaster.HubTypeId.HasValue)
            {
                HubMaster.HubTypeId = -1;
            }

            if (!HubMaster.ReferralSourceId.HasValue)
            {
                HubMaster.ReferralSourceId = -1;
            }

            FillDropDown();

            return Page();
        }

        private void FillDropDown()
        {
            // For HubTypeId
            var hubTypes = _context.HubTypes
                .Select(h => new DropdownViewModel(h.Id.ToString(), h.Name))
                .ToList();

            // Add "Other" option
            hubTypes.Add(new DropdownViewModel("-1", "Other"));

            ViewData["HubTypeId"] = new SelectList(hubTypes, "Id", "Name");

            // For ReferralSourceId
            var referralSources = _context.ReferralSources
                .Select(r => new DropdownViewModel(r.Id.ToString(), r.Name))
                .ToList();

            // Add "Other" option
            referralSources.Add(new DropdownViewModel("-1", "Other"));

            ViewData["ReferralSourceId"] = new SelectList(referralSources, "Id", "Name");

            ViewData["FeeTypeSelectList"] = GetFeeTypeSelectList();
        }

        public SelectList GetFeeTypeSelectList()
        {
            var values = from FeeType e in Enum.GetValues(typeof(FeeType))
                         select new { Id = (int)e, Name = e.ToString() };

            return new SelectList(values, "Id", "Name");
        }
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var hubEntity = await _context.HubMasters.FindAsync(HubMaster.Id);

            if (hubEntity == null)
            {
                return NotFound();
            }

            _mapper.Map<EditHubMasterViewModel, HubMaster>(HubMaster, hubEntity);

            // Clear 'Other' fields if respective 'Id' fields have values
            hubEntity.HubTypeOther = hubEntity.HubTypeId.HasValue ? string.Empty : hubEntity.HubTypeOther;
            hubEntity.ReferralSourceOther = hubEntity.ReferralSourceId.HasValue ? string.Empty : hubEntity.ReferralSourceOther;

            // Set 'Id' fields to null if their values are -1
            hubEntity.HubTypeId = (HubMaster.HubTypeId == -1) ? null : HubMaster.HubTypeId;
            hubEntity.ReferralSourceId = (HubMaster.ReferralSourceId == -1) ? null : HubMaster.ReferralSourceId;

            _context.Attach(hubEntity).State = EntityState.Modified;

            try
            {

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HubMasterExists(hubEntity.Id)) // Assuming hubEntity.Id is the correct reference
                {
                    return NotFound();
                }

                // Optionally, you can add a concurrency error message here if the entity exists
                ModelState.AddModelError(string.Empty, "The record you attempted to edit was modified by another user after you got the original value. Please try again.");
                return Page();
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                // _logger.LogError(ex, "An error occurred while saving the HubMaster.");

                ModelState.AddModelError(string.Empty, "An error occurred while saving the changes. Please try again.");

                // Re-populate any necessary data for the view (like dropdowns)
                FillDropDown();

                return Page();
            }
            

            FillDropDown();

            return RedirectToPage("./Index");
        }

        private bool HubMasterExists(int id)
        {
          return (_context.HubMasters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
