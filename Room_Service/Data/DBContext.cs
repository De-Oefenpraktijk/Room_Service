using Microsoft.EntityFrameworkCore;
using Room_Service.DTO;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Driver;
using Room_Service.Entities;

namespace Room_Service.Data
{
    public class DBContext : IDBContext
    {
        public DBContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Workspaces = database.GetCollection<Workspace>(configuration.GetValue<string>("DatabaseSettings:WorkspaceCollectionName"));
            Rooms = database.GetCollection<PrivateRoom>(configuration.GetValue<string>("DatabaseSettings:RoomCollectionName"));
            PublicRooms = database.GetCollection<PublicRoom>(configuration.GetValue<string>("DatabaseSettings:PublicRoomCollectionName"));
        }

        public IMongoCollection<Workspace> Workspaces { get; }

        public IMongoCollection<PrivateRoom> Rooms { get; }

        public IMongoCollection<PublicRoom> PublicRooms { get; }
    }
}
