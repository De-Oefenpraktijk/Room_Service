using System;
using Microsoft.AspNetCore.Mvc;
using Room_Service.DTO;
using Room_Service.Entities;
using System.Net;

namespace Room_Service.Contracts
{
    public interface IRoomService
    {
        public Task<WorkspaceDTO> GetUserRooms(string userid, string workspaceid);

        public Task<RoomDTO> UpdateRoom(Room room);

        public Task<string> DeleteRoom(string roomid);

        public Task<RoomDTO> CreateRoom(Room room);

        public Task<WorkspaceDTO> GetRoomByID(string roomid);
    }
}

