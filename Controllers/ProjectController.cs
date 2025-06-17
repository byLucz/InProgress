using Microsoft.AspNetCore.Mvc;
using XKANBAN.DTOs.Projects;
using XKANBAN.Models.Project;
using XKANBAN.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using XKANBAN.Contxet;
using System.Linq;
using XKANBAN.Services;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.Build.Evaluation;
using XKANBAN.Models.User;
using DIPLOMKANBAN.Models.Template;

namespace XKANBAN.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;
        private readonly KanBanContext _context;

        public ProjectController(IProjectService projectService, IUserService userService, KanBanContext context)
        {
            _projectService = projectService;
            _userService = userService;
            _context = context;
        }

        public IActionResult Desks(int pageId, string search)
        {
            return View(GetProjects());
        }

        public IActionResult SuperPanel(int pageId, string search)
        {
            return View();
        }

        public IActionResult Info(int pageId, string search)
        {
            return View();
        }

        public IActionResult Announcement(int pageId, string search)
        {
            return View();
        }

        [Route("/Project/Kanban/{id}")]
        public IActionResult Kanban(int id)
        {
            for (int i = 1; i <= 5; i++)
            {
                ViewData[$"Slot{i}"] = _projectService.GetCartByProjectIdAndSlot(id, i);
            }

            for (int i = 1; i <= 5; i++)
            {
                ViewData[$"Column{i}"] = _projectService.GetColumnByProjectIdAndSlot(id, i);
            }
            return View(_projectService.GetProjectById(id));
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(AddProjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Models.Project.Project project = new Models.Project.Project()
            {
                Title = model.Title,
                Description = model.Description,
                CreateDate = DateTime.Now,
                EndDate = model.EndDate,
                UserId = _userService.GetUserIdByEmail(User.Identity.Name),
                IsDelete = false
            };

            _projectService.AddProject(project);

            return Redirect("/Project/Desks");
        }

        [HttpPost]
        public IActionResult CreateDefaultProject()
        {
            var defaultproject = new Models.Project.Project
            {
                Title = "Тестовый шаблон",
                UserId = _userService.GetUserIdByEmail(User.Identity.Name),
                Description = "Автоматически-созданный универсальный шаблон для дипломной работы",
                CreateDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                IsDelete = false
            };

            _projectService.AddProject(defaultproject);

            int projectId = defaultproject.ProjectId;
            CreateDefaultCarts(projectId);

            return Ok();
        }

        public void CreateDefaultCarts(int projectId)
        {
            var defaultCarts = new List<Cart>
            {
                new Cart { Name = "Задание 1", StatusNumber = 1, ProjectId = projectId, Color = "#e1dc6b", Description = "Выполнить задание 1", DeadLineDate = DateTime.Now.AddDays(10), IsDelete = false},
                new Cart { Name = "Задание 2", StatusNumber = 2, ProjectId = projectId, Color = "#BADBAD", Description = "Выполнить задание 2", DeadLineDate = DateTime.Now.AddDays(10), IsDelete = false},
                new Cart { Name = "Задание 3", StatusNumber = 3, ProjectId = projectId, Color = "#FE8535", Description = "Выполнить задание 3", DeadLineDate = DateTime.Now.AddDays(10), IsDelete = false},
                new Cart { Name = "Задание 4", StatusNumber = 1, ProjectId = projectId, Color = "#e1dc6b", Description = "Выполнить задание 4", DeadLineDate = DateTime.Now.AddDays(60), IsDelete = false},
                new Cart { Name = "Задание 5", StatusNumber = 2, ProjectId = projectId, Color = "#FE8535", Description = "Выполнить задание 5", DeadLineDate = DateTime.Now.AddDays(60), IsDelete = false},
                new Cart { Name = "Задание 6", StatusNumber = 3, ProjectId = projectId, Color = "#BADBAD", Description = "Выполнить задание 6", DeadLineDate = DateTime.Now.AddDays(100), IsDelete = false}
            };

            _context.Carts.AddRange(defaultCarts);
            _context.SaveChanges();
        }

        [HttpPost]
        public IActionResult CreateDiplomeProject()
        {
            var defaultproject = new Models.Project.Project
            {
                Title = "Подготовка и написание ВКР",
                UserId = _userService.GetUserIdByEmail(User.Identity.Name),
                Description = "Автоматически-созданный универсальный шаблон",
                CreateDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(6),
                IsDelete = false
            };

            _projectService.AddDiplomeProject(defaultproject);

            int projectId = defaultproject.ProjectId;
            CreateDiplomeCarts(projectId);

            return Ok();
        }

        public void CreateDiplomeCarts(int projectId)
        {
            var defaultCarts = new List<Cart>
            {
                new Cart { Name = "Выбор темы и утверждение научным руководителем", StatusNumber = 1, ProjectId = projectId, Color = "#f9f9f9", Description = "Выполнить задание 1", DeadLineDate = DateTime.Now.AddDays(30), IsDelete = false},
                new Cart { Name = "Анализ существующих решений", StatusNumber = 1, ProjectId = projectId, Color = "#f9f9f9", Description = "Выполнить задание 2", DeadLineDate = DateTime.Now.AddDays(30), IsDelete = false},
                new Cart { Name = "Анализ и выбор технологий", StatusNumber = 1, ProjectId = projectId, Color = "#f9f9f9", Description = "Выполнить задание 3", DeadLineDate = DateTime.Now.AddDays(30), IsDelete = false},
                new Cart { Name = "Создание презентации для представления темы диплома", StatusNumber = 1, ProjectId = projectId, Color = "#f9f9f9", Description = "Выполнить задание 4", DeadLineDate = DateTime.Now.AddDays(30), IsDelete = false},
                new Cart { Name = "Создание первой версии проекта", StatusNumber = 1, ProjectId = projectId, Color = "#f9f9f9", Description = "Выполнить задание 5", DeadLineDate = DateTime.Now.AddDays(30), IsDelete = false},
                new Cart { Name = "Дизайн", StatusNumber = 1, ProjectId = projectId, Color = "#f9f9f9", Description = "Выполнить задание 6", DeadLineDate = DateTime.Now.AddDays(30), IsDelete = false},
                new Cart { Name = "Создание финальной версии проекта", StatusNumber = 1, ProjectId = projectId, Color = "#f9f9f9", Description = "Выполнить задание 7", DeadLineDate = DateTime.Now.AddDays(80), IsDelete = false },
                new Cart { Name = "Тестирование и исправление ошибок", StatusNumber = 1, ProjectId = projectId, Color = "#f9f9f9", Description = "Выполнить задание 8", DeadLineDate = DateTime.Now.AddDays(80), IsDelete = false},
                new Cart { Name = "Подготовка презентации для защиты", StatusNumber = 1, ProjectId = projectId, Color = "#f9f9f9", Description = "Выполнить задание 9", DeadLineDate = DateTime.Now.AddDays(80), IsDelete = false },
                new Cart { Name = "Сбор и анализ источников", StatusNumber = 1, ProjectId = projectId, Color = "#f9f9f9", Description = "Выполнить задание 10", DeadLineDate = DateTime.Now.AddDays(80), IsDelete = false },
                new Cart { Name = "Написание первых двух глав", StatusNumber = 1, ProjectId = projectId, Color = "#f9f9f9", Description = "Выполнить задание 11", DeadLineDate = DateTime.Now.AddDays(140), IsDelete = false },
                new Cart { Name = "Написание третьей главы и заключения", StatusNumber = 1, ProjectId = projectId, Color = "#f9f9f9", Description = "Выполнить задание 12", DeadLineDate = DateTime.Now.AddDays(140), IsDelete = false },
                new Cart { Name = "Редактирование текста", StatusNumber = 1, ProjectId = projectId, Color = "#f9f9f9", Description = "Выполнить задание 13", DeadLineDate = DateTime.Now.AddDays(140), IsDelete = false },
                new Cart { Name = "Отправка черновика на проверку научному руководителю", StatusNumber = 1, ProjectId = projectId, Color = "#f9f9f9", Description = "Выполнить задание 14", DeadLineDate = DateTime.Now.AddDays(160), IsDelete = false },
                new Cart { Name = "Сдача диплома", StatusNumber = 1, ProjectId = projectId, Color = "#f9f9f9", Description = "Выполнить задание 15", DeadLineDate = DateTime.Now.AddDays(160), IsDelete = false }
            };

            _context.Carts.AddRange(defaultCarts);
            _context.SaveChanges();
        }

        public IActionResult TemplateEditor()
        {
            var templates = _context.Templates
                .Include(t => t.CartTemplates)
                .Include(t => t.ColumnTemplates)
                .Select(t => new TemplateViewModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CreateDate = t.CreateDate,
                    EndDate = t.EndDate,
                    IsDelete = t.IsDelete,
                    CartTemplates = t.CartTemplates.Select(c => new CartTemplateViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        Color = c.Color,
                        DeadLineDate = c.DeadLineDate,
                        IsDelete = c.IsDelete
                    }).ToList(),
                    ColumnTemplates = t.ColumnTemplates.Select(c => new ColumnTemplateViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        SlotNumber = c.SlotNumber,
                        IsActive = c.IsActive
                    }).ToList()
                }).ToList();

            return View(templates);
        }

        [HttpPost]
        public IActionResult SaveTemplate(TemplateViewModel templateViewModel)
        {
            const int maxColumns = 5;
            const string customColumnPrefix = "Custom";

            if (templateViewModel.Id == 0)
            {
                var template = new Template
                {
                    Title = templateViewModel.Title,
                    Description = templateViewModel.Description,
                    CreateDate = DateTime.Now,
                    EndDate = templateViewModel.EndDate,
                    IsDelete = templateViewModel.IsDelete,
                    CartTemplates = templateViewModel.CartTemplates.Select(c => new CartTemplate
                    {
                        Name = c.Name,
                        Description = c.Description,
                        Color = "#f9f9f9",
                        DeadLineDate = DateTime.Now,
                        IsDelete = c.IsDelete
                    }).ToList(),
                    ColumnTemplates = templateViewModel.ColumnTemplates.Select(c => new ColumnTemplate
                    {
                        Name = c.Name,
                        SlotNumber = c.SlotNumber,
                        IsActive = c.IsActive
                    }).ToList()
                };

                int existingColumnsCount = template.ColumnTemplates.Count;
                for (int i = existingColumnsCount; i < maxColumns; i++)
                {
                    template.ColumnTemplates.Add(new ColumnTemplate
                    {
                        Name = $"{customColumnPrefix} {i - 2}",
                        SlotNumber = i + 1,
                        IsActive = false
                    });
                }

                _context.Templates.Add(template);
            }
            else
            {
                var existingTemplate = _context.Templates
                    .Include(t => t.CartTemplates)
                    .Include(t => t.ColumnTemplates)
                    .FirstOrDefault(t => t.Id == templateViewModel.Id);
                if (existingTemplate != null)
                {
                    existingTemplate.Title = templateViewModel.Title;
                    existingTemplate.Description = templateViewModel.Description;
                    existingTemplate.EndDate = templateViewModel.EndDate;
                    existingTemplate.IsDelete = templateViewModel.IsDelete;

                    foreach (var cartTemplate in templateViewModel.CartTemplates)
                    {
                        var existingCart = existingTemplate.CartTemplates.FirstOrDefault(c => c.Id == cartTemplate.Id);
                        if (existingCart != null)
                        {
                            existingCart.Name = cartTemplate.Name;
                            existingCart.Description = cartTemplate.Description;
                            existingCart.Color = cartTemplate.Color;
                            existingCart.DeadLineDate = cartTemplate.DeadLineDate;
                            existingCart.IsDelete = cartTemplate.IsDelete;
                        }
                        else
                        {
                            if (!cartTemplate.IsDelete)
                            {
                                existingTemplate.CartTemplates.Add(new CartTemplate
                                {
                                    Name = cartTemplate.Name,
                                    Description = cartTemplate.Description,
                                    Color = cartTemplate.Color,
                                    DeadLineDate = cartTemplate.DeadLineDate,
                                    IsDelete = cartTemplate.IsDelete
                                });
                            }
                        }
                    }

                    foreach (var existingCart in existingTemplate.CartTemplates.ToList())
                    {
                        if (templateViewModel.CartTemplates.All(c => c.Id != existingCart.Id))
                        {
                            _context.Remove(existingCart);
                        }
                    }

                    foreach (var columnTemplate in templateViewModel.ColumnTemplates)
                    {
                        var existingColumn = existingTemplate.ColumnTemplates.FirstOrDefault(c => c.Id == columnTemplate.Id);
                        if (existingColumn != null)
                        {
                            existingColumn.Name = columnTemplate.Name;
                            existingColumn.SlotNumber = columnTemplate.SlotNumber;
                            existingColumn.IsActive = columnTemplate.IsActive;
                        }
                        else
                        {
                            if (!columnTemplate.IsDelete)
                            {
                                existingTemplate.ColumnTemplates.Add(new ColumnTemplate
                                {
                                    Name = columnTemplate.Name,
                                    SlotNumber = columnTemplate.SlotNumber,
                                    IsActive = columnTemplate.IsActive
                                });
                            }
                        }
                    }

                    foreach (var existingColumn in existingTemplate.ColumnTemplates.ToList())
                    {
                        if (templateViewModel.ColumnTemplates.All(c => c.Id != existingColumn.Id))
                        {
                            _context.Remove(existingColumn);
                        }
                    }

                    int existingColumnsCount = existingTemplate.ColumnTemplates.Count;
                    for (int i = existingColumnsCount; i < maxColumns; i++)
                    {
                        existingTemplate.ColumnTemplates.Add(new ColumnTemplate
                        {
                            Name = $"{customColumnPrefix} {i - 2}",
                            SlotNumber = i + 1,
                            IsActive = false
                        });
                    }

                    _context.Templates.Update(existingTemplate);
                }
            }
            _context.SaveChanges();
            return RedirectToAction("TemplateEditor");
        }

        [HttpPost]
        public IActionResult DeleteTemplate(int id)
        {
            var template = _context.Templates.Find(id);
            if (template != null)
            {
                _context.Templates.Remove(template);
                _context.SaveChanges();
            }
            return RedirectToAction("TemplateEditor");
        }

        [HttpGet]
        public IActionResult GetTemplateById(int id)
        {
            var template = _context.Templates
                .Include(t => t.CartTemplates)
                .Include(t => t.ColumnTemplates)
                .Where(t => t.Id == id)
                .Select(t => new TemplateViewModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CreateDate = t.CreateDate,
                    EndDate = t.EndDate,
                    IsDelete = t.IsDelete,
                    CartTemplates = t.CartTemplates.Select(c => new CartTemplateViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        Color = c.Color,
                        DeadLineDate = c.DeadLineDate,
                        IsDelete = c.IsDelete
                    }).ToList(),
                    ColumnTemplates = t.ColumnTemplates.Select(c => new ColumnTemplateViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        SlotNumber = c.SlotNumber,
                        IsActive = c.IsActive
                    }).ToList()
                }).FirstOrDefault();

            if (template == null)
            {
                return NotFound();
            }
            return Json(template);
        }

        [HttpGet]
        public IActionResult GetTemplates()
        {
            var templates = _context.Templates
                .Select(t => new { t.Id, t.Title })
                .ToList();

            return Json(templates);
        }

        [HttpPost]
        public IActionResult CreateProjectFromTemplate(int templateId)
        {
            var template = _context.Templates
                .Include(t => t.CartTemplates)
                .Include(t => t.ColumnTemplates)
                .FirstOrDefault(t => t.Id == templateId);

            if (template == null)
            {
                return BadRequest("Шаблон не найден");
            }

            var newProject = new Models.Project.Project
            {
                Title = template.Title,
                Description = template.Description,
                CreateDate = DateTime.Now,
                EndDate = template.EndDate,
                IsDelete = false,
                UserId = _userService.GetUserIdByEmail(User.Identity.Name)
            };

            _context.Projects.Add(newProject);
            _context.SaveChanges();

            foreach (var cartTemplate in template.CartTemplates)
            {
                var newCart = new Cart
                {
                    Name = cartTemplate.Name,
                    Description = cartTemplate.Description,
                    Color = cartTemplate.Color,
                    DeadLineDate = cartTemplate.DeadLineDate,
                    IsDelete = cartTemplate.IsDelete,
                    StatusNumber = 1,
                    ProjectId = newProject.ProjectId
                };
                _context.Carts.Add(newCart);
            }

            foreach (var columnTemplate in template.ColumnTemplates)
            {
                var newColumn = new Column
                {
                    Name = columnTemplate.Name,
                    SlotNumber = columnTemplate.SlotNumber,
                    IsActive = columnTemplate.IsActive,
                    ProjectId = newProject.ProjectId
                };
                _context.CustomColumns.Add(newColumn);
            }

            _context.SaveChanges();

            return Ok();
        }
        public IActionResult AddCart(int id)
        {
            ViewBag.ProjectId = id;
            return View();
        }

        [HttpPost]
        public IActionResult AddCart(AddCartViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Cart cart = new Cart()
            {
                Name = model.Name,
                ProjectId = model.ProjectId,
                IsDelete = false,
                DeadLineDate = DateTime.Now,
                StatusNumber = 1
            };

            _projectService.AddCart(cart);

            return Redirect($"/Project/Kanban/{model.ProjectId}");
        }

        public IActionResult GoToNextLevel(int id)
        {
            var cart = _projectService.GetCartById(id);
            var activatedColumnsCount = _context.CustomColumns.Count(c => c.ProjectId == cart.ProjectId && c.IsActive);

            if (activatedColumnsCount == cart.StatusNumber)
            {
                DeleteCart(id);
            }
            else if (cart.StatusNumber < activatedColumnsCount)
            {
                cart.StatusNumber++;
                _projectService.UpdateCart(cart);
            }

            return Redirect($"/Project/Kanban/{cart.ProjectId}");
        }

        public IActionResult GoToPreviousLevel(int id)
        {
            var cart = _projectService.GetCartById(id);
            var activatedColumnsCount = _context.CustomColumns.Count(c => c.ProjectId == cart.ProjectId && c.IsActive);

            if (cart.StatusNumber > 1)
            {
                cart.StatusNumber--;
                _projectService.UpdateCart(cart);
            }

            return Redirect($"/Project/Kanban/{cart.ProjectId}");
        }

        public IActionResult DeleteCart(int id)
        {
            var cart = _projectService.GetCartById(id);
            cart.IsDelete = true;
            _projectService.UpdateCart(cart);
            return Redirect($"/Project/Kanban/{cart.ProjectId}");
        }

        public IActionResult DeleteProject(int id)
        {
            var project = _projectService.GetProjectById(id);
            project.IsDelete = true;
            _projectService.UpdateProject(project);
            return Redirect("/Project/Desks");
        }

        public IActionResult EditProject(int id)
        {
            var project = _projectService.GetProjectById(id);
            return View(project);
        }

        [HttpPost]
        public IActionResult EditProject(Models.Project.Project model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            
            var existingProject = _projectService.GetProjectById(model.ProjectId);


            model.CreateDate = existingProject.CreateDate;
            model.AssingIds = existingProject.AssingIds;

            _projectService.UpdateProject(model);

            return Redirect("/Project/Desks");
        }

        [HttpGet]
        public IActionResult GetCardsForProject(int projectId)
        {
            try
            {

                var carts = _context.Carts
                    .Where(c => c.ProjectId == projectId)
                    .Select(c => new { id = c.CartId, color = c.Color }) 
                    .ToList();

                return Json(new { success = true, carts = carts });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetCartDetails(int cartId)
        {
            try
            {
                var cart = _context.Carts.FirstOrDefault(c => c.CartId == cartId);
                if (cart == null)
                {
                    return Json(new { success = false, message = "Карточка не найдена." });
                }

                return Json(new { success = true, name = cart.Name, color = cart.Color, description = cart.Description, deadline = cart.DeadLineDate });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Ошибка: " + ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CreateAnnouncement(AddAnnouncementViewModel model)
        {
            if (ModelState.IsValid)
            {
                var announcement = new SAnnouncement
                {
                    Title = model.Title,
                    Content = model.Content,
                    Author = GetUserLogin(),
                    CreatedAt = DateTime.Now
                };

                _context.Announcement.Add(announcement);
                _context.SaveChanges();

                var announcements = _context.Announcement.OrderByDescending(a => a.CreatedAt).ToList();
                return Json(announcements);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete]
        public IActionResult DeleteAnnouncement(int id)
        {
            var announcement = _context.Announcement.FirstOrDefault(a => a.Id == id);
            if (announcement == null)
            {
                return NotFound();
            }

            _context.Announcement.Remove(announcement);
            _context.SaveChanges();

            var announcements = _context.Announcement.OrderByDescending(a => a.CreatedAt).ToList();
            return Json(announcements);
        }

        public IActionResult GetAnnouncements()
        {
            var announcements = _context.Announcement.OrderByDescending(a => a.CreatedAt).ToList();
            return Json(announcements);
        }

        [HttpPost]
        public IActionResult RequestMoveCard([FromBody] CartDetailsUpdateModel model)
        {
            var cart = _context.Carts.Find(model.CartId);
            if (cart == null)
            {
                return NotFound();
            }

            cart.RequestedColumnId = model.newColumnId;
            _context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IActionResult ApproveMoveCard([FromBody] CartDetailsUpdateModel model)
        {
            var cart = _context.Carts.Find(model.CartId);
            if (cart == null)
            {
                return NotFound();
            }

            if (cart.RequestedColumnId.HasValue)
            {
                cart.StatusNumber = cart.RequestedColumnId.Value;
                cart.RequestedColumnId = null;
                _context.SaveChanges();
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult RejectMoveCard([FromBody] CartDetailsUpdateModel model)
        {
            var cart = _context.Carts.Find(model.CartId);
            if (cart == null)
            {
                return NotFound();
            }

            cart.RequestedColumnId = null;
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public IActionResult Search(string query)
        {
            var projects = _context.Projects
                .Include(p => p.User)
                .Where(p => p.Title.Contains(query) || p.User.Username.Contains(query))
                .Select(p => new ShowProjectsViewModel
                {
                    ProjectId = p.ProjectId,
                    Title = p.Title,
                    Creator = p.User.Username
                })
                .ToList();

            return View("Desks", projects);
        }
        public class CartDetailsUpdateModel
        {
            public int CartId { get; set; }
            public string NewName { get; set; }
            public string NewColor { get; set; }
            public string Description { get; set; }
            public int newColumnId { get; set; }
            public DateTime? Deadline { get; set; }
        }
        public class CartDetailsViewModel
        {
            public string Name { get; set; }
            public string Color { get; set; }
            public string Description { get; set; }
            public DateTime? Deadline { get; set; }
        }

        public class AddUserToProjectModel
        {
            public int UserId { get; set; }
            public int ProjectId { get; set; }
        }

        public class UpdateColumnModel
        {
            public int ColumnId { get; set; }
            public string NewName { get; set; }
        }

        public class ProjIdModel
        {
            public int ProjectId { get; set; }
        }

        [HttpPost]
        public IActionResult ActivateColumns([FromBody] ProjIdModel model)
        {
            var columns = _context.CustomColumns.Where(c => c.ProjectId == model.ProjectId && c.IsActive).ToList();

            if (columns.Count >= 5)
            {
                return Json(new { success = false, message = "TooManyColumns" });
            }

            var columnToActivate = _context.CustomColumns
                                            .Where(c => c.ProjectId == model.ProjectId && !c.IsActive)
                                            .OrderBy(c => c.ColumnId)
                                            .FirstOrDefault(c => c.ColumnId >= 4);


            if (columnToActivate != null)
            {
                columnToActivate.IsActive = true;
                _context.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult DeactivateColumns([FromBody] ProjIdModel model)
        {
            var columns = _context.CustomColumns.Where(c => c.ProjectId == model.ProjectId && c.IsActive).ToList();

            if (columns.Count <= 3)
            {
                return Json(new { success = false, message = "TooFewColumns" });
            }

            var columnToDeactivate = _context.CustomColumns
                                             .Where(c => c.ProjectId == model.ProjectId && c.IsActive)
                                             .OrderByDescending(c => c.SlotNumber)
                                             .FirstOrDefault();

            if (columnToDeactivate != null)
            {
                columnToDeactivate.IsActive = false;
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> updateColumnName([FromBody] UpdateColumnModel model)
        {
            var column = await _context.CustomColumns.FindAsync(model.ColumnId);
            if (column == null)
            {
                return NotFound(new { success = false, message = "Колонка не найдена." });
            }

            column.Name = model.NewName;
            await _context.SaveChangesAsync();

            return Ok(new { success = true });
        }

        [HttpPost]
        public IActionResult UpdateCartDetails([FromBody] CartDetailsUpdateModel model)
        {
            try
            {
                var cart = _context.Carts.FirstOrDefault(c => c.CartId == model.CartId);
                if (cart == null)
                {
                    return Json(new { success = false, message = "Карточка не найдена." });
                }

                cart.Name = model.NewName;
                cart.Color = model.NewColor;
                cart.Description = model.Description;

                
                if (model.Deadline.HasValue)
                {
                    cart.DeadLineDate = model.Deadline.Value;
                }

                _context.SaveChanges();


                return Json(new
                {
                    success = true,
                    message = "Данные карточки успешно обновлены.",
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Ошибка: " + ex.Message });
            }
        }

        [HttpGet]
        public ActionResult GetTaskName(int cartId)
        {
            var task = _context.Carts.FirstOrDefault(t => t.CartId == cartId);
            if (task != null)
            {
                return Json(new { success = true, name = task.Name });
            }
            return Json(new { success = false, message = "Task not found." });
        }


        [HttpPost]
        public async Task<IActionResult> SendMessageWithFile(int cartId, IFormFile file, string message)
        {
            try
            {
                var cart = _context.Carts.FirstOrDefault(c => c.CartId == cartId);
                if (cart == null)
                {
                    return NotFound("Cart not found");
                }

                var now = DateTime.Now;
                var timeWithoutSeconds = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);

                if (message == null)
                {
                    message = "⠀";
                }

                string filePath = null;
                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var safeFileName = Path.GetRandomFileName() + Path.GetExtension(fileName); 
                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", safeFileName);
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    filePath = "/uploads/" + safeFileName; 
                }

                var chatMessage = new ChatMessage
                {
                    CardId = cartId,
                    Text = message,
                    TimeSent = timeWithoutSeconds,
                    UserName = GetUserLogin(), 
                    FilePath = filePath 
                };

                _context.ChatMessages.Add(chatMessage);
                _context.SaveChanges();

                return Ok(new { Success = true, Message = "Message sent successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error sending message: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetMessagesForCart(int cartId)
        {
            try
            {
                var messages = _context.ChatMessages
                    .Where(m => m.CardId == cartId)
                    .Select(m => new
                    {
                        Id = m.ChatMessageId,
                        UserName = m.UserName,
                        Text = m.Text,
                        TimeSent = m.TimeSent.ToString("yyyy-MM-dd HH:mm"),
                        FileUrl = m.FilePath != null ? Url.Content(m.FilePath) : null
                    })
                    .ToList();

                return Json(new { Success = true, Messages = messages });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error getting messages: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetGroups()
        {
            var groups = await _context.Users
                .Where(u => !string.IsNullOrEmpty(u.Group))
                .Select(u => u.Group)
                .Distinct()
                .OrderBy(g => g)
                .ToListAsync();
            return Json(groups);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserProjects()
        {
            var loggedInUserId = _context.Users
                .Where(u => u.Email == User.Identity.Name) 
                .Select(u => u.UserId)
                .FirstOrDefault();

            var supervisor = _context.Users
                .Where(u => u.Role == "Supervisor")
                .FirstOrDefault();

            if (loggedInUserId == default(int))
            {
                return Json(new { success = false, message = "Пользователь не найден." });
            }


            if (loggedInUserId != null && supervisor.UserId == loggedInUserId)
            {
                var projects = await _context.Projects
                    .Where(p => !p.IsDelete)
                    .Select(p => new { p.ProjectId, p.Title })
                    .ToListAsync();
                return Json(projects);
            }
            else
            {
                var allProjects = await _context.Projects
                   .Where(p => !p.IsDelete)
                   .ToListAsync();

                var userProjects = allProjects
                    .Where(p => p.UserId == loggedInUserId ||
                               (!string.IsNullOrEmpty(p.AssingIds) && p.AssingIds.Split(',').Contains(loggedInUserId.ToString())))
                    .Select(p => new { p.ProjectId, p.Title })
                    .ToList();

                return Json(userProjects);
            }
        }

        [HttpGet]
        public IActionResult GetProjectsByUserId(int userId)
        {
            var projects = _context.Projects
                .Where(p => p.UserId == userId)
                .Select(p => new
                {
                    p.ProjectId,
                    p.Title
                })
                .ToList();

            return Json(projects);
        }
        public List<ShowProjectsViewModel> GetProjects()
        {
            var user = _context.Users
                .Where(u => u.Email == User.Identity.Name)
                .FirstOrDefault();
            if (user == null)
            {
                return new List<ShowProjectsViewModel>();
            }

            List<Models.Project.Project> projects;
            if (user.Role == "Student" || user.Role == "Teacher" || user.Role == "Consultant")
            {
                projects = _context.Projects
                    .Include(p => p.User)
                    .AsEnumerable() // Извлечение данных для обработки в клиентской памяти
                    .Where(p => p.User.UserId == user.UserId ||
                                (p.AssingIds != null && p.AssingIds.Split(',').Contains(user.UserId.ToString())))
                    .ToList();
            }
            else if (user.Role == "Supervisor")
            {
                projects = _context.Projects
                    .Include(p => p.User)
                    .AsEnumerable() 
                    .ToList();
            }
            else
            {
                // Обработка непредвиденных значений роли пользователя
                return new List<ShowProjectsViewModel>();
            }

            return projects.OrderByDescending(p => p.CreateDate)
                .Select(p => new ShowProjectsViewModel()
                {
                    Title = p.Title,
                    Descrioption = p.Description,
                    Creator = p.User.Username,
                    AssingsIds = GetAssignLogins(p.AssingIds),
                    CreateDate = p.CreateDate,
                    EndDate = p.EndDate,
                    ProjectId = p.ProjectId
                }).ToList();
        }

        public String GetUserLogin()
        {
            var user = _context.Users
                .Where(u => u.Email == User.Identity.Name)
                .FirstOrDefault();
            return user.Username;
        }

        public String GetAssignLogins(string assignIds)
        {
            if (string.IsNullOrEmpty(assignIds))
            {
                return string.Empty;
            }

            var ids = assignIds.Split(',');

            var logins = _context.Users
                .Where(u => ids.Contains(u.UserId.ToString()))
                .Select(u => u.Username)
                .ToList();

            return string.Join(", ", logins);
        }
        [HttpPost]
        public IActionResult RemoveAssignedUser(int projectId, string login)
        {
            var project = _context.Projects.FirstOrDefault(p => p.ProjectId == projectId);
            if (project == null)
            {
                return NotFound("Project not found");
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == login);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var userIds = project.AssingIds.Split(',').ToList();

            userIds.Remove(user.UserId.ToString()); // Предполагается, что UserId это int, преобразуем в строку для удаления

            project.AssingIds = String.Join(",", userIds);

            _context.SaveChanges();
            return Ok(new { success = true });
        }


        public IActionResult GetAssignedUsers(string assignIds)
        {
            // Проверяем, что строка с ID не пуста
            if (string.IsNullOrEmpty(assignIds))
            {
                return NotFound("Список предоставленного доступа пуст.");
            }

            // Разделяем строку на отдельные ID
            var ids = assignIds.Split(',');

            // Ищем пользователей по этим ID и получаем их логины
            var logins = _context.Users
                .Where(u => ids.Contains(u.UserId.ToString()))
                .Select(u => u.Username)
                .ToList();

            return Ok(logins);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersByRoleAndGroup(string role, string group)
        {
            var usersQuery = _context.Users.Where(u => u.Role == role);

            if (role == "Student" && !string.IsNullOrEmpty(group))
            {
                usersQuery = usersQuery.Where(u => u.Group == group);
            }

            var users = await usersQuery.Select(u => new { u.UserId, DisplayName = $"{u.FullName} ({u.Username})" }).ToListAsync();
            return Json(users);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToProject([FromBody] AddUserToProjectModel model)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == model.ProjectId);
            if (project == null)
            {
                return NotFound("Project not found.");
            }

            // Преобразование существующих ID в список
            var ids = new List<string>(project.AssingIds?.Split(',') ?? new string[0]);
            if (!ids.Contains(model.UserId.ToString()))
            {
                ids.Add(model.UserId.ToString());
                project.AssingIds = String.Join(",", ids);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Пользователь добавлен к проекту" });
            }
            else
            {
                return Json(new { success = false, message = "Пользователь уже назначен на этот проект" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> LoadProjectActivityGraph(int projectId)
        {
            try
            {
                var totalTasks = await _context.Carts
                .Where(c => c.ProjectId == projectId)
                .CountAsync();

                var projectStartDate = await _context.Projects
                    .Where(p => p.ProjectId == projectId)
                    .Select(p => p.CreateDate)
                    .FirstOrDefaultAsync();

                // Загружаем дату завершения проекта
                var projectEndDate = await _context.Projects
                    .Where(p => p.ProjectId == projectId)
                    .Select(p => p.EndDate)
                    .FirstOrDefaultAsync();

                if (projectEndDate == null)
                {
                    return NotFound("Project end date is not set.");
                }

                // Загружаем данные активности по задачам
                var data = await _context.Carts
                   .Where(t => t.ProjectId == projectId && t.DeadLineDate != null)
                   .GroupBy(t => t.DeadLineDate.Date)
                   .OrderBy(g => g.Key)  // Сортировка групп по ключу (дата)
                   .Select(g => new { Date = g.Key.ToString("yyyy-MM-dd"), Remaining = g.Count() })
                   .ToListAsync();


                // Возвращаем данные задач и дату завершения проекта
                return Ok(new { Tasks = data, DeadlineDate = projectEndDate.ToString("yyyy-MM-dd"), Total = totalTasks, CreateDate = projectStartDate.ToString("yyyy-MM-dd") });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error loading project activity graph data: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> LoadTasksByColumns(int projectId)
        {
            try
            {
                var carts = await _context.Carts
                .Where(c => c.ProjectId == projectId)
                .ToListAsync();

                var columns = await _context.CustomColumns
                    .Where(c => c.ProjectId == projectId && c.IsActive)
                    .ToListAsync();

                // Связываем данные с помощью LINQ, где SlotNumber в Column соответствует StatusNumber в Cart
                var data = columns
                    .GroupJoin(carts,
                               column => column.SlotNumber,  // Используйте SlotNumber из Column
                               cart => cart.StatusNumber,  // Используйте StatusNumber из Cart
                               (column, matchingCarts) => new {
                                   ColumnName = column.Name,
                                   Count = matchingCarts.Count()  // Подсчитываем количество соответствующих карточек
                               })
                    .ToList();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error loading task data by columns: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> LoadCompletedTasksOverTime(int projectId)
        {
            try
            {
                // Получаем максимальный активный SlotNumber
                var maxActiveSlotNumber = await _context.CustomColumns
                    .Where(c => c.ProjectId == projectId && c.IsActive)
                    .Select(c => c.SlotNumber)
                    .MaxAsync();

                // Получаем задачи с максимальным активным SlotNumber
                var completedTasks = await _context.Carts
                    .Where(t => t.ProjectId == projectId && t.StatusNumber == maxActiveSlotNumber && t.DeadLineDate != null)
                    .GroupBy(t => t.DeadLineDate.Date)
                    .OrderBy(g => g.Key)
                    .Select(g => new
                    {
                        Date = g.Key.ToString("yyyy-MM-dd"),
                        Count = g.Count()
                    })
                    .ToListAsync();

                // Получаем дату создания проекта
                var projectStartDate = await _context.Projects
                    .Where(p => p.ProjectId == projectId)
                    .Select(p => p.CreateDate)
                    .FirstOrDefaultAsync();

                // Получаем дату окончания проекта
                var projectEndDate = await _context.Projects
                    .Where(p => p.ProjectId == projectId)
                    .Select(p => p.EndDate)
                    .FirstOrDefaultAsync();

                if (projectEndDate == null)
                {
                    return NotFound("Project end date is not set.");
                }

                return Ok(new
                {
                    Tasks = completedTasks,
                    DeadlineDate = projectEndDate.ToString("yyyy-MM-dd"),
                    Total = completedTasks.Sum(t => t.Count),  // Общее количество выполненных задач
                    CreateDate = projectStartDate.ToString("yyyy-MM-dd")
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error loading completed tasks over time data: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult CheckUnreadMessages()
        {
            var currentUserName = GetUserLogin();
            if (string.IsNullOrEmpty(currentUserName))
            {
                return Unauthorized();
            }

            var unreadMessages = _context.ChatMessages
                .Where(m => !m.ReadByUsernames.Contains(currentUserName) || string.IsNullOrEmpty(m.ReadByUsernames))
                .GroupBy(m => m.CardId)
                .Select(group => new { CartId = group.Key, UnreadCount = group.Count() })
                .ToList();

            return Ok(unreadMessages.ToDictionary(x => x.CartId, x => x.UnreadCount));
        }

        [HttpPost]
        public IActionResult MarkAsRead(int messageId)
        {
            var currentUserName = GetUserLogin();
            if (string.IsNullOrEmpty(currentUserName))
            {
                return Json(new { error = "Unauthorized access" }); // Возвращать JSON
            }

            var message = _context.ChatMessages.FirstOrDefault(m => m.ChatMessageId == messageId);
            if (message == null)
            {
                return Json(new { error = "Message not found" }); // Возвращать JSON
            }

            var readByUsernames = string.IsNullOrEmpty(message.ReadByUsernames)
                                  ? new List<string>()
                                  : message.ReadByUsernames.Split(',').ToList();

            if (readByUsernames.Contains(currentUserName))
            {
                return Json(new { message = "Message already marked as read by this user." });
            }

            readByUsernames.Add(currentUserName);
            message.ReadByUsernames = String.Join(",", readByUsernames);
            _context.SaveChanges();

            return Json(new { success = "Message marked as read." }); 
        }
    }
}
