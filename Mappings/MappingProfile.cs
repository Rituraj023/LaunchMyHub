using AutoMapper;
using LaunchMyHub.DTOs;
using LaunchMyHub.Models;

namespace LaunchMyHub.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Create a mapping from the Entity to the ViewModel
            CreateMap<HubMaster, EditHubMasterViewModel>();
            CreateMap<HubMaster, AddHubMasterViewModel>();

            // Create a mapping from the ViewModel to the Entity
            CreateMap<EditHubMasterViewModel, HubMaster>();
            CreateMap<AddHubMasterViewModel, HubMaster>();
        }
    }
}
