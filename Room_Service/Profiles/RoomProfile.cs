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
            CreateMap<OutputRoomDTO, PrivateRoom>()
				.ReverseMap();
            CreateMap<InputRoomDTO, PrivateRoom>()
                .ReverseMap();
        }
    }
}

