using System;
using Microsoft.AspNetCore.Mvc;
using Room_Service.DTO;
using Room_Service.Entities;
using System.Net;
using MongoDB.Bson;

namespace Room_Service.Contracts
{
    public interface IRoomService
    {
        public Task<Workspace> GetUserRooms(string userid, ObjectId workspaceid);

        public Task<Room> UpdateRoom(RoomDTO roomDTO);

        public Task<ObjectId> DeleteRoom(ObjectId roomid);

        public Task<Room> CreateRoom(RoomDTO roomDTO);

        public Task<Workspace> GetRoomByID(ObjectId roomid);
    }
}

