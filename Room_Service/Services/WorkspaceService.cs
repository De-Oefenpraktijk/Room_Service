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

        public async Task<WorkspaceDTO> CreateWorkspace(WorkspaceDTO workspaceDTO)
        {
            Workspace workspace = _mapper.Map<WorkspaceDTO, Workspace>(workspaceDTO);
            await _context.Workspaces.InsertOneAsync(workspace);
            return _mapper.Map<Workspace, WorkspaceDTO>(workspace);
        }

        public async Task<string> DeleteWorkspace(string workspaceid)
        {
            await _context.Workspaces.DeleteOneAsync(x => x.id == workspaceid);
            return workspaceid;
        }

        public async Task<WorkspaceDTO> GetWorkspaceByID(string workspaceid)
        {
            var workspace = await _context.Workspaces.Find(x => x.id == workspaceid).FirstOrDefaultAsync();
            if (workspace == null)
            {
                throw new Exception("workspace does not exist");
            }
            return _mapper.Map<Workspace, WorkspaceDTO>(workspace);
        }

        public async Task<IEnumerable<WorkspaceDTO>> GetWorkspaces()
        {
            var result = await _context.Workspaces.Find(_ => true).ToListAsync();
            return _mapper.Map<IEnumerable<Workspace>, IEnumerable<WorkspaceDTO>>(result);
        }

        public async Task<WorkspaceDTO> UpdateWorkspace(WorkspaceDTO workspaceDTO)
        {
            Workspace workspace = _mapper.Map<WorkspaceDTO, Workspace>(workspaceDTO);
            await _context.Workspaces.ReplaceOneAsync(x => x.id == workspace.id, workspace);
            return _mapper.Map<Workspace, WorkspaceDTO>(workspace);
        }
    }
}

