using System;
using Microsoft.AspNetCore.Mvc;
using Room_Service.DTO;
using Room_Service.Entities;

namespace Room_Service.Contracts
{
    public interface IWorkspaceService
    {
        public Task<IEnumerable<OutputWorkspaceDTO>> GetWorkspaces();

        public Task<OutputWorkspaceDTO> UpdateWorkspace(InputWorkspaceDTO workspaceDTO);

        public Task<string> DeleteWorkspace(string workspaceId);

        public Task<OutputWorkspaceDTO> CreateWorkspace(InputWorkspaceDTO workspaceDTO);

        public Task<OutputWorkspaceDTO> GetWorkspaceByID(string workspaceId);
    }
}

