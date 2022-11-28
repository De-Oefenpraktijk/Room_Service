using Room_Service.DTO;
using Room_Service.Entities;

namespace Room_Service.Contracts
{
    public interface IRoomService
    {
        public Task<WorkspaceDTO> GetUserRooms(string userid, string workspaceid);

        public Task<RoomDTO> UpdateRoom(RoomDTO roomDTO);

        public Task<string> DeleteRoom(string roomid);

        public Task<RoomDTO> CreateRoom(RoomDTO roomDTO);

        public Task<WorkspaceDTO> GetRoomByID(string roomid);
    }
}

