using System;
using MongoDB.Driver;
using Room_Service.Contracts;
using Room_Service.Data;
using Room_Service.DTO;
using Room_Service.Entities;

namespace Room_Service.Services.Services
{
    public class WorkspaceService : IWorkspaceService
    {
        private readonly IDBContext _context;
        public WorkspaceService(IDBContext context)
        {
            _context = context;
        }

        public async Task<WorkspaceDTO> CreateWorkspace(Workspace workspace)
        {
            await _context.Workspaces.InsertOneAsync(workspace);
            return new WorkspaceDTO(workspace);
        }

        public async Task<string> DeleteWorkspace(string workspaceid)
        {
            await _context.Workspaces.DeleteOneAsync(x => x.Id == workspaceid);
            return workspaceid;
        }

        public async Task<WorkspaceDTO> GetWorkspaceByID(string workspaceid)
        {
            var workspace = await _context.Workspaces.Find(x => x.Id == workspaceid).FirstOrDefaultAsync();
            if (workspace == null)
            {
                throw new Exception("workspace does not exist");
            }
            return new WorkspaceDTO(workspace);
        }

        public async Task<IEnumerable<WorkspaceDTO>> GetWorkspaces()
        {
            var result = await _context.Workspaces.Find(_ => true).ToListAsync();
            return result.Select(x => new WorkspaceDTO(x)).ToList();
        }

        public async Task<WorkspaceDTO> UpdateWorkspace(Workspace workspace)
        {
            await _context.Workspaces.ReplaceOneAsync(x => x.Id == workspace.Id, workspace);
            return new WorkspaceDTO(workspace);
        }
    }
}

