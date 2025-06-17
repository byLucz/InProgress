using System;
using XKANBAN.Models.Project;
using XKANBAN.DTOs.Projects;
using System.Collections.Generic;
using XKANBAN.Models.User;
using System.Security.Claims;

namespace XKANBAN.Services.Interfaces
{
    public interface IProjectService
    {
        void AddProject(Project model);
        void AddDiplomeProject(Project model);
        Project GetProjectById(int id);
        List<Cart> GetCartByProjectIdAndSlot(int projectId, int slot);
        List<Column> GetColumnByProjectIdAndSlot(int projectId, int slot);
        void AddCart(Cart model);
        Cart GetCartById(int id);
        void UpdateCart(Cart cart);
        void UpdateProject(Project project);
    }
}