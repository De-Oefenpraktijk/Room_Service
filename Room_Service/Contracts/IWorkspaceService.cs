using System;
using Microsoft.AspNetCore.Mvc;
using Room_Service.DTO;
using Room_Service.Entities;

namespace Room_Service.Contracts
{
    public interface IWorkspaceService
    {
        public Task<IEnumerable<Workspace>> GetWorkspaces();

        public Task<Workspace> UpdateWorkspace(WorkspaceDTO workspaceDTO);

        public Task<string> DeleteWorkspace(string workspaceid);

        public Task<Workspace> CreateWorkspace(WorkspaceDTO workspaceDTO);

        public Task<Workspace> GetWorkspaceByID(string workspaceid);
    }
}

