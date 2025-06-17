using XKANBAN.Models.Project;
using XKANBAN.Services.Interfaces;
using XKANBAN.Contxet;
using System.Linq;
using XKANBAN.DTOs.Projects;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Razor;
using XKANBAN.DTOs.Users;
using XKANBAN.Models.User;

namespace XKANBAN.Services
{
    public class ProjectService : IProjectService
    {
        KanBanContext _context;

        public ProjectService(KanBanContext context, IUserService users)
        {
            _context = context;
        }

        public void AddCart(Cart model)
        {
            _context.Carts.Add(model);
            _context.SaveChanges();
        }

        public void AddProject(Project model)
        {
            _context.Projects.Add(model);
            _context.SaveChanges();
            CreateDefaultColumns(model.ProjectId);
        }

        public void AddDiplomeProject(Project model)
        {
            _context.Projects.Add(model);
            _context.SaveChanges();
            CreateDiplomeColumns(model.ProjectId);
        }

        public Cart GetCartById(int id)
        {
            return _context.Carts.SingleOrDefault(c => c.CartId == id);
        }

        public List<Cart> GetCartByProjectIdAndSlot(int projectId, int slot)
        {
            return  _context.Carts.Where(c => c.ProjectId == projectId && c.StatusNumber == slot).ToList();
        }

        public void CreateDefaultColumns(int projectId)
        {
            var defaultColumns = new List<Column>
            {
                new Column { Name = "To Do", SlotNumber = 1, ProjectId = projectId, IsActive = true },
                new Column { Name = "In Progress", SlotNumber = 2, ProjectId = projectId, IsActive = true },
                new Column { Name = "Done", SlotNumber = 3, ProjectId = projectId, IsActive = true },
                new Column { Name = "Custom 1", SlotNumber = 4, ProjectId = projectId, IsActive = false }, 
                new Column { Name = "Custom 2", SlotNumber = 5, ProjectId = projectId, IsActive = false }
            };

            _context.CustomColumns.AddRange(defaultColumns);
            _context.SaveChanges();
        }

        public void CreateDiplomeColumns(int projectId)
        {
            var defaultColumns = new List<Column>
            {
                new Column { Name = "К выполнению", SlotNumber = 1, ProjectId = projectId, IsActive = true },
                new Column { Name = "В работе", SlotNumber = 2, ProjectId = projectId, IsActive = true },
                new Column { Name = "На проверке", SlotNumber = 3, ProjectId = projectId, IsActive = true },
                new Column { Name = "Выполнено", SlotNumber = 4, ProjectId = projectId, IsActive = true },
                new Column { Name = "Custom 2", SlotNumber = 5, ProjectId = projectId, IsActive = false }
            };

            _context.CustomColumns.AddRange(defaultColumns);
            _context.SaveChanges();
        }

        public List<Cart> GetCartsByProjectId(int projectId)
        {
            return _context.Carts.Where(c => c.ProjectId == projectId).ToList();
        }

        public List<Column> GetColumnByProjectIdAndSlot(int projectId, int slot)
        {
            return _context.CustomColumns.Where(c => c.ProjectId == projectId && c.SlotNumber == slot && c.IsActive).ToList();
        }

        public Project GetProjectById(int id)
        {
            return _context.Projects.Find(id);
        }

        public void UpdateCart(Cart cart)
        {
            _context.Carts.Update(cart);
            _context.SaveChanges();
        }

        public void UpdateProject(Project project)
        {
            var existingProject = _context.Projects.Local.FirstOrDefault(p => p.ProjectId == project.ProjectId);
            if (existingProject != null)
            {
                _context.Entry(existingProject).State = EntityState.Detached;
            }

            _context.Projects.Attach(project);
            _context.Entry(project).State = EntityState.Modified;

            _context.SaveChanges();
        }
    }
}