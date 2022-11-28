using System;
using Microsoft.AspNetCore.Mvc;
using Room_Service.DTO;
using Room_Service.Entities;

namespace Room_Service.Contracts
{
    public interface IWorkspaceService
    {
        public Task<IEnumerable<WorkspaceDTO>> GetWorkspaces();

        public Task<WorkspaceDTO> UpdateWorkspace(WorkspaceDTO workspaceDTO);

        public Task<string> DeleteWorkspace(string workspaceid);

        public Task<WorkspaceDTO> CreateWorkspace(WorkspaceDTO workspaceDTO);

        public Task<WorkspaceDTO> GetWorkspaceByID(string workspaceid);
    }
}

