using Room_Service.DTO;
using Room_Service.Entities;

namespace Room_Service.Contracts
{
    public interface IRoomService
    {
        public Task<OutputWorkspaceDTO> GetUserRooms(string useriId, string workspaceId);

        public Task<OutputRoomDTO> UpdateRoom(InputRoomDTO roomDTO);

        public Task<string> DeleteRoom(string roomid);

        public Task<OutputRoomDTO> CreateRoom(InputRoomDTO roomDTO);

        public Task<OutputWorkspaceDTO> GetRoomByID(string roomId);
    }
}

