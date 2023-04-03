using AutoMapper;
using Microsoft.CodeAnalysis;
using MongoDB.Driver;
using Room_Service.Contracts;
using Room_Service.Data;
using Room_Service.DTO;
using Room_Service.Entities;

namespace Room_Service.Services.Services
{
    public class PublicRoomService : IPublicRoomService
    {
        private readonly IDBContext _context;
        private readonly IWorkspaceService _workspaceService;
        private readonly IMapper _mapper;

        public PublicRoomService(IDBContext context, IWorkspaceService workspaceService, IMapper mapper)
        {
            _context = context;
            _workspaceService = workspaceService;
            _mapper = mapper;
        }

        public async Task<OutputPublicRoomDTO> CreateRoom(InputPublicRoomDTO roomDTO)
        {
            PublicRoom room = _mapper.Map<InputPublicRoomDTO, PublicRoom>(roomDTO);
            await _context.PublicRooms.InsertOneAsync(room);
            return _mapper.Map<PublicRoom, OutputPublicRoomDTO>(room);
        }
    }
}
