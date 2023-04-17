using Room_Service.DTO;

namespace Room_Service.Contracts
{
    public interface IPublicRoomService
    {
        Task<OutputPublicRoomDTO> CreateRoom(InputPublicRoomDTO roomDTO);
        Task<IEnumerable<OutputPublicRoomDTO>> GetPublicRoomsOfWorkspace(string workspaceId);
    }
}