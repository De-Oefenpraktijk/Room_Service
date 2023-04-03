using AutoMapper;
using Microsoft.CodeAnalysis;
using MongoDB.Driver;
using Room_Service.Contracts;
using Room_Service.Data;
using Room_Service.DTO;
using Room_Service.Entities;

namespace Room_Service.Services.Services
{
    public class RoomService : IRoomService
    {
        private readonly IDBContext _context;
        private readonly IWorkspaceService _workspaceService;
        private readonly IMapper _mapper;

        public RoomService(IDBContext context, IWorkspaceService workspaceService, IMapper mapper)
        {
            _context = context;
            _workspaceService = workspaceService;
            _mapper = mapper;
        }

        public async Task<OutputRoomDTO> CreateRoom(InputRoomDTO roomDTO)
        {
            Room room = _mapper.Map<InputRoomDTO, Room>(roomDTO);
            InviteSelf(room);
            await _context.Rooms.InsertOneAsync(room);
            return _mapper.Map<Room, OutputRoomDTO>(room);
        }

        public async Task<string> DeleteRoom(string roomId)
        {
            await _context.Rooms.DeleteOneAsync(x => x.roomId == roomId);
            return roomId;
        }

        public async Task<OutputWorkspaceDTO> GetRoomByID(string roomId)
        {
            Room room = await _context.Rooms.Find(x => x.roomId == roomId).FirstOrDefaultAsync();
            OutputWorkspaceDTO workspace = await _workspaceService.GetWorkspaceByID(room.workspaceId);
            workspace.rooms = new List<OutputRoomDTO>
            {
                _mapper.Map<Room, OutputRoomDTO>(room)
            };
            return workspace;
        }

        public async Task<OutputWorkspaceDTO> GetUserRooms(string userid, string workspaceid)
        {
            List<Room> rooms = await _context.Rooms.Find(x => (x.hostId == userid || x.invitedIds.Contains(userid))
            && (x.scheduledDate > DateTime.Now.Date && x.scheduledDate < DateTime.Now.Date.AddDays(1))
            && x.workspaceId == workspaceid).ToListAsync();

            OutputWorkspaceDTO workspace = await _workspaceService.GetWorkspaceByID(workspaceid);

            workspace.rooms = _mapper.Map<List<Room>, List<OutputRoomDTO>>(rooms);
            return workspace;
        }

        public async Task<OutputRoomDTO> UpdateRoom(InputRoomDTO roomDTO)
        {
            Room room = _mapper.Map<InputRoomDTO, Room>(roomDTO);
            await _context.Rooms.ReplaceOneAsync(x => x.roomId == room.roomId, room);
            return _mapper.Map<Room, OutputRoomDTO>(room);
        }

        private void InviteSelf(Room room) {
            if (room.invitedIds.Contains(room.hostId)) {
                throw new InvitedYourselfException("Can't invite yourself to a room");
            }
        }
    }
}

