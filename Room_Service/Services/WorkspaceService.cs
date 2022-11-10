using System;
using AutoMapper;
using MongoDB.Bson;
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

        public async Task<ObjectId> DeleteWorkspace(ObjectId workspaceid)
        {
            await _context.Workspaces.DeleteOneAsync(x => x.id == workspaceid);
            return workspaceid;
        }

        public async Task<Workspace> GetWorkspaceByID(ObjectId workspaceid)
        {
            var workspace = await _context.Workspaces.Find(x => x.id == workspaceid).FirstOrDefaultAsync();
            if (workspace == null)
            {
                throw new Exception("workspace does not exist");
            }
            return workspace;
        }

        public async Task<IEnumerable<Workspace>> GetWorkspaces()
        {
            var result = await _context.Workspaces.Find(_ => true).ToListAsync();
            return result;
        }

        public async Task<Workspace> UpdateWorkspace(WorkspaceDTO workspaceDTO)
        {
            Workspace workspace = _mapper.Map<WorkspaceDTO, Workspace>(workspaceDTO);
            await _context.Workspaces.ReplaceOneAsync(x => x.id == workspace.id, workspace);
            return workspace;
        }
    }
}

