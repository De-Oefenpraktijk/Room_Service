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
            CreateMap<OutputWorkspaceDTO, Workspace>()
               .ReverseMap();
			CreateMap<InputWorkspaceDTO, Workspace>()
				.ReverseMap();
        }
	}
}

