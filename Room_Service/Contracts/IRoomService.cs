using Room_Service.DTO;
using Room_Service.Entities;

namespace Room_Service.Contracts
{
    public interface IRoomService
    {
        public Task<Workspace> GetUserRooms(string userid, string workspaceid);

        public Task<Room> UpdateRoom(RoomDTO roomDTO);

        public Task<string> DeleteRoom(string roomid);

        public Task<Room> CreateRoom(RoomDTO roomDTO);

        public Task<Workspace> GetRoomByID(string roomid);
    }
}

