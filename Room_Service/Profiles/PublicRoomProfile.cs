using System;
using AutoMapper;
using Microsoft.Azure.Cosmos;
using Room_Service.DTO;
using Room_Service.Entities;

namespace Room_Service.Profiles
{
    public class PublicRoomProfile : Profile
    {
        public PublicRoomProfile()
        {
            CreateMap<OutputPublicRoomDTO, PublicRoom>()
                .ReverseMap();
            CreateMap<InputPublicRoomDTO, PublicRoom>()
                .ReverseMap();
        }
    }
}
