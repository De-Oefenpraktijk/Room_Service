using System;
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
        public RoomService(IDBContext context, IWorkspaceService workspaceService)
        {
            _context = context;
            _workspaceService = workspaceService;
        }

        public async Task<RoomDTO> CreateRoom(Room room)
        {
            await _context.Rooms.InsertOneAsync(room);
            return new RoomDTO(room);
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
            workspace.rooms.Add(new RoomDTO(room));
            return workspace;
        }

        public async Task<WorkspaceDTO> GetUserRooms(string userid, string workspaceid)
        {
            List<Room> rooms = await _context.Rooms.Find(x => (x.hostId == userid || x.invitedIds.Contains(userid))
            && (x.scheduledDate > DateTime.Now.Date && x.scheduledDate < DateTime.Now.Date.AddDays(1))
            && x.workspaceId == workspaceid).ToListAsync();

            WorkspaceDTO workspace = await _workspaceService.GetWorkspaceByID(workspaceid);

            workspace.rooms = rooms.Select(x => new RoomDTO(x)).ToList();
            return workspace;
        }

        public async Task<RoomDTO> UpdateRoom(Room room)
        {
            await _context.Rooms.ReplaceOneAsync(x => x.roomId == room.roomId, room);
            return new RoomDTO(room);
        }
    }
}

