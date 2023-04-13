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
        private readonly ISocialServiceData _socialServiceData;

        public PublicRoomService(IDBContext context, IWorkspaceService workspaceService, ISocialServiceData socialServiceData, IMapper mapper)
        {
            _context = context;
            _workspaceService = workspaceService;
            _mapper = mapper;
            _socialServiceData = socialServiceData;
        }

        public async Task<OutputPublicRoomDTO> CreateRoom(InputPublicRoomDTO roomDTO)
        {
            // If date in the past
            // Or if the user doesn't exist
            //Or if the workspace doesn't exist
            if (roomDTO.ScheduledDate < DateTime.UtcNow ||                 
                !(await _socialServiceData.IsUserValid(roomDTO.HostId))||       
                _workspaceService.GetWorkspaceByID(roomDTO.WorkspaceId) == null)    
            {
                //Return null and perform a check in the controller
                return null;    
            }
            PublicRoom room = _mapper.Map<InputPublicRoomDTO, PublicRoom>(roomDTO);
            await _context.PublicRooms.InsertOneAsync(room);
            return _mapper.Map<PublicRoom, OutputPublicRoomDTO>(room);
        }

        public async Task<IEnumerable<OutputPublicRoomDTO>> GetPublicRoomsOfWorkspace(string workspaceId)
        {
            var result = await _context.PublicRooms.Find(r => r.WorkspaceId == workspaceId).ToListAsync();
            return _mapper.Map<IEnumerable<PublicRoom>, IEnumerable<OutputPublicRoomDTO>>(result);
        }
    }
}
