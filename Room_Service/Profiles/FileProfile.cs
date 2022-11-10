using System;
using AutoMapper;
using Room_Service.DTO;
using Room_Service.Entities;

namespace Room_Service.Profiles
{
	public class FileProfile : Profile
	{
		public FileProfile()
		{
            CreateMap<FileDTO, Files>()
               .ForMember(
                   dest => dest.fileName,
                   opt => opt.MapFrom(src => src.FileName)
               ).ForMember(
                   dest => dest.fileUrl,
                   opt => opt.MapFrom(src => src.FileUrl)
               );
        }
	}
}

