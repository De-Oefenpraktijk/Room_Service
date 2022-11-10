using System;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public WorkspaceService(IDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Workspace> CreateWorkspace(WorkspaceDTO workspaceDTO)
        {
            Workspace workspace = _mapper.Map<WorkspaceDTO, Workspace>(workspaceDTO);
            await _context.Workspaces.InsertOneAsync(workspace);
            return workspace;
        }

        public async Task<string> DeleteWorkspace(string workspaceid)
        {
            await _context.Workspaces.DeleteOneAsync(x => x.Id == workspaceid);
            return workspaceid;
        }

        public async Task<Workspace> GetWorkspaceByID(string workspaceid)
        {
            var workspace = await _context.Workspaces.Find(x => x.id == workspaceid).FirstOrDefaultAsync();
            if (workspace == null)
            {
                throw new Exception("workspace does not exist");
            }
            return new Workspace
        }

        public async Task<IEnumerable<Workspace>> GetWorkspaces()
        {
            var result = await _context.Workspaces.Find(_ => true).ToListAsync();
            return result
        }

        public async Task<Workspace> UpdateWorkspace(WorkspaceDTO workspaceDTO)
        {
            Workspace workspace = _mapper.Map<WorkspaceDTO, Workspace>(workspaceDTO);
            await _context.Workspaces.ReplaceOneAsync(x => x.id == workspace.id, workspace);
            return workspace;
        }
    }
}

