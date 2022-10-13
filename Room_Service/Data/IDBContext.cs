using MongoDB.Driver;
using Room_Service.Entities;

namespace Room_Service.Data;

public interface IDBContext
{
    IMongoCollection<Workspace> Workspaces { get; }
}