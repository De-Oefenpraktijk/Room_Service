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

        public async Task<RoomDTO> CreateRoom(RoomDTO roomDTO)
        {
            Room room = _mapper.Map<RoomDTO, Room>(roomDTO);
            InviteSelf(room);
            await _context.Rooms.InsertOneAsync(room);
            return _mapper.Map<Room, RoomDTO>(room);
        }

        public async Task<string> DeleteRoom(string roomid)
        {
            await _context.Rooms.DeleteOneAsync(x => x.roomId == roomid);
            return roomid;
        }

        public async Task<WorkspaceDTO> GetRoomByID(string roomid)
        {
            Room room = await _context.Rooms.Find(x => x.roomId == roomid).FirstOrDefaultAsync();
            WorkspaceDTO workspace = await _workspaceService.GetWorkspaceByID(room.workspaceId);
            workspace.rooms = new List<RoomDTO>();
            workspace.rooms.Add(_mapper.Map<Room, RoomDTO>(room));
            return workspace;
        }

        public async Task<WorkspaceDTO> GetUserRooms(string userid, string workspaceid)
        {
            List<Room> rooms = await _context.Rooms.Find(x => (x.hostId == userid || x.invitedIds.Contains(userid))
            && (x.scheduledDate > DateTime.Now.Date && x.scheduledDate < DateTime.Now.Date.AddDays(1))
            && x.workspaceId == workspaceid).ToListAsync();

            WorkspaceDTO workspace = await _workspaceService.GetWorkspaceByID(workspaceid);

            workspace.rooms = _mapper.Map<List<Room>, List<RoomDTO>>(rooms);
            return workspace;
        }

        public async Task<RoomDTO> UpdateRoom(RoomDTO roomDTO)
        {
            Room room = _mapper.Map<RoomDTO, Room>(roomDTO);
            await _context.Rooms.ReplaceOneAsync(x => x.roomId == room.roomId, room);
            return _mapper.Map<Room, RoomDTO>(room);
        }

        private void InviteSelf(Room room) {
            if (room.invitedIds.Contains(room.hostId)) {
                throw new InvitedYourselfException("Can't invite yourself to a room");
            }
        }
    }
}

