using System;
using AutoMapper;
using Microsoft.Azure.Cosmos;
using Room_Service.DTO;
using Room_Service.Entities;

namespace Room_Service.Profiles
{
	public class RoomProfile : Profile
	{
		public RoomProfile()
		{
            CreateMap<RoomDTO, Room>()
               .ForMember(
                   dest => dest.hostId,
                   opt => opt.MapFrom(src => src.HostUser)
               )
               .ForMember(
                   dest => dest.invitedIds,
                   opt => opt.MapFrom(src => src.InvitedUsers)
               ).ForMember(
                   dest => dest.scheduledDate,
                   opt => opt.MapFrom(src => src.ScheduledDate)
               ).ForMember(
                   dest => dest.workspaceId,
                   opt => opt.MapFrom(src => src.WorkspaceId)
               );
        }
	}
}

