using System;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Room_Service.DTO;
using Room_Service.Entities;

namespace Room_Service.Contracts
{
    public interface IWorkspaceService
    {
        public Task<IEnumerable<Workspace>> GetWorkspaces();

        public Task<Workspace> UpdateWorkspace(WorkspaceDTO workspaceDTO);

        public Task<ObjectId> DeleteWorkspace(ObjectId workspaceid);

        public Task<Workspace> CreateWorkspace(WorkspaceDTO workspaceDTO);

        public Task<Workspace> GetWorkspaceByID(ObjectId workspaceid);
    }
}

