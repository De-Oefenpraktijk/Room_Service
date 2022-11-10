using System;
using AutoMapper;
using Room_Service.DTO;
using Room_Service.Entities;

namespace Room_Service.Profiles
{
	public class WorkspaceProfile : Profile
	{
		public WorkspaceProfile()
		{
            CreateMap<WorkspaceDTO, Workspace>()
               .ForMember(
                   dest => dest.id,
                   opt => opt.MapFrom(src => src.Id)
               ).ForMember(
                   dest => dest.name,
                   opt => opt.MapFrom(src => src.Name)
               ).ForMember(
                   dest => dest.imageFile,
                   opt => opt.MapFrom(src => src.ImageFile)
               ).ForMember(
                   dest => dest.files,
                   opt => opt.MapFrom(src => src.Files)
               );
        }
	}
}

