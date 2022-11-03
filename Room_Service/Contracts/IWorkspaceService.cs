using System;
using Microsoft.AspNetCore.Mvc;
using Room_Service.DTO;
using Room_Service.Entities;

namespace Room_Service.Contracts
{
    public interface IWorkspaceService
    {
        public Task<IEnumerable<WorkspaceDTO>> GetWorkspaces();

        public Task<WorkspaceDTO> UpdateWorkspace(Workspace workspace);

        public Task<string> DeleteWorkspace(string workspaceid);

        public Task<WorkspaceDTO> CreateWorkspace(Workspace workspace);

        public Task<WorkspaceDTO> GetWorkspaceByID(string workspaceid);
    }
}

