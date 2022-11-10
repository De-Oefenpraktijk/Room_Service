using System;
using AutoMapper;
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

        public async Task<Room> CreateRoom(RoomDTO roomDTO)
        {
            Room room = _mapper.Map<RoomDTO, Room>(roomDTO);
            InviteSelf(room);
            await _context.Rooms.InsertOneAsync(room);
            return room;
        }

        public async Task<string> DeleteRoom(string roomid)
        {
            await _context.Rooms.DeleteOneAsync(x => x.roomId == roomid);
            return roomid;
        }

        public async Task<Workspace> GetRoomByID(string roomid)
        {
            Room room = await _context.Rooms.Find(x => x.roomId == roomid).FirstOrDefaultAsync();
            Workspace workspace = await _workspaceService.GetWorkspaceByID(room.workspaceId);
            workspace.rooms = new List<Room>();
            workspace.rooms.Add(room);
            return workspace;
        }

        public async Task<Workspace> GetUserRooms(string userid, string workspaceid)
        {
            List<Room> rooms = await _context.Rooms.Find(x => (x.hostId == userid || x.invitedIds.Contains(userid))
            && (x.scheduledDate > DateTime.Now.Date && x.scheduledDate < DateTime.Now.Date.AddDays(1))
            && x.workspaceId == workspaceid).ToListAsync();

            Workspace workspace = await _workspaceService.GetWorkspaceByID(workspaceid);

            workspace.rooms = rooms;
            return workspace;
        }

        public async Task<Room> UpdateRoom(RoomDTO roomDTO)
        {
            Room room = _mapper.Map<RoomDTO, Room>(roomDTO);
            await _context.Rooms.ReplaceOneAsync(x => x.roomId == room.roomId, room);
            return room;
        }

        private void InviteSelf(Room room) {
            if (room.invitedIds.Contains(room.hostId)) {
                throw new InvitedYourselfException("Can't invite yourself to a room");
            }
        }
    }
}

