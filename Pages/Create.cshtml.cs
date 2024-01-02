using AutoMapper;
using LaunchMyHub.DTOs;
using LaunchMyHub.Enums;
using LaunchMyHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LaunchMyHub.Pages
{
    [IgnoreAntiforgeryToken]
    public class CreateModel : PageModel
    {
        private IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly Data.ApplicationDbContext _context;

        public CreateModel(Data.ApplicationDbContext context, 
            IMapper mapper,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [BindProperty]
        public bool OpenParcial { get; set; } = default!;

       
        public IActionResult OnGet(string? code)
        {
                      
            if(code == "5750465A-BFB8-44CD-A67B-E72050A06D9E")
            {
                this.OpenParcial = true;             
                
            }

            this.FillDropDown();
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

        [BindProperty]

        public AddHubMasterViewModel HubMaster { get; set; } = default!;
        


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.HubMasters == null || HubMaster == null)
            {
                FillDropDown();
                return Page();
            }

            var hubEntity = _mapper.Map<HubMaster>(HubMaster);

            // Clear 'Other' fields if respective 'Id' fields have values
            hubEntity.HubTypeOther = hubEntity.HubTypeId.HasValue ? string.Empty : hubEntity.HubTypeOther;
            hubEntity.ReferralSourceOther = hubEntity.ReferralSourceId.HasValue ? string.Empty : hubEntity.ReferralSourceOther;

            // Set 'Id' fields to null if their values are -1
            hubEntity.HubTypeId = (HubMaster.HubTypeId == -1) ? null : HubMaster.HubTypeId;
            hubEntity.ReferralSourceId = (HubMaster.ReferralSourceId == -1) ? null : HubMaster.ReferralSourceId;

            try
            {

                _context.HubMasters.Add(hubEntity);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while saving the changes. Please try again.");

                FillDropDown();

                return Page();
            }

            if (OpenParcial)
            {
                string returnUrl = _configuration["Client:ReturnUrl"];

                // Redirect to the ReturnUrl
                return Redirect(returnUrl);
            }

            return RedirectToPage("./Index");
        }
    }
}
