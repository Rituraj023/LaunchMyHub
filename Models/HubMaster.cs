using LaunchMyHub.Enums;
using System.ComponentModel.DataAnnotations;

namespace LaunchMyHub.Models
{
    using System.ComponentModel.DataAnnotations;

    public class HubMaster
    {
        [Key]
        public int Id { get; set; }

        [Required, EmailAddress, StringLength(100), Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required, StringLength(255), Display(Name = "Password")]
        public string Password { get; set; }

        [Required, StringLength(100), Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        [Required, StringLength(100), Display(Name = "Organization Name")]
        public string OrganizationName { get; set; }

        [Required, StringLength(20), Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Fee Type")]
        public FeeType FeeType { get; set; }

        [Url, StringLength(100), Display(Name = "Website URL")]
        public string? Website { get; set; }

        [Display(Name = "Non-Profit Organization")]
        public bool NonProfit { get; set; }

        [StringLength(50), Display(Name = "Preferred Subdomain")]
        public string? PreferredSubdomain { get; set; }

        [StringLength(1000), Display(Name = "Brief Description")]
        public string? BriefDescription { get; set; }

        [Display(Name = "Hub Type")]
        public int? HubTypeId { get; set; }
        public HubType? HubType { get; set; }

        [StringLength(100), Display(Name = "Other Hub Type")]
        public string? HubTypeOther { get; set; }

        [Display(Name = "Referral Source")]
        public int? ReferralSourceId { get; set; }
        public ReferralSource? ReferralSource { get; set; }

        [StringLength(100), Display(Name = "Other Referral Source")]
        public string? ReferralSourceOther { get; set; }
    }


}
