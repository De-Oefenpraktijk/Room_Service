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

        public async Task<OutputWorkspaceDTO> CreateWorkspace(InputWorkspaceDTO workspaceDTO)
        {
            Workspace workspace = _mapper.Map<InputWorkspaceDTO, Workspace>(workspaceDTO);
            await _context.Workspaces.InsertOneAsync(workspace);
            return _mapper.Map<Workspace, OutputWorkspaceDTO>(workspace);
        }

        public async Task<string> DeleteWorkspace(string workspaceId)
        {
            await _context.Workspaces.DeleteOneAsync(x => x.id == workspaceId);
            return workspaceId;
        }

        public async Task<OutputWorkspaceDTO> GetWorkspaceByID(string workspaceId)
        {
            var workspace = await _context.Workspaces.Find(x => x.id == workspaceId).FirstOrDefaultAsync();
            if (workspace == null)
            {
                throw new Exception("workspace does not exist");
            }
            return _mapper.Map<Workspace, OutputWorkspaceDTO>(workspace);
        }

        public async Task<IEnumerable<OutputWorkspaceDTO>> GetWorkspaces()
        {
            var result = await _context.Workspaces.Find(_ => true).ToListAsync();
            return _mapper.Map<IEnumerable<Workspace>, IEnumerable<OutputWorkspaceDTO>>(result);
        }

        public async Task<OutputWorkspaceDTO> UpdateWorkspace(InputWorkspaceDTO workspaceDTO)
        {
            Workspace workspace = _mapper.Map<InputWorkspaceDTO, Workspace>(workspaceDTO);
            await _context.Workspaces.ReplaceOneAsync(x => x.id == workspace.id, workspace);
            return _mapper.Map<Workspace, OutputWorkspaceDTO>(workspace);
        }
    }
}

