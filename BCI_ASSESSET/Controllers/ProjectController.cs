using BCI_ASSESSET.DB;
using BCI_ASSESSET.Models;
using BCI_ASSESSET.Repositories;
using BCI_ASSESSET.Requests.Projects;
using BCI_ASSESSET.Response;
using BCI_ASSESSET.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BCI_ASSESSET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ILogger<ProjectController> _logger;
        public ProjectController(IProjectRepository projectRepository, ILogger<ProjectController> logger)
        {
            _projectRepository = projectRepository;
            _logger = logger;
        }

        [HttpPost()]
        //[Authorize]
        public async Task<IActionResult> GetAll([FromBody] ProjectListRequest request)
        {
            try
            {
                _logger.LogInformation("Fetching all project by: {Name}", request.Name);
                var payload = await _projectRepository.getAll(request.Name, request.Page, request.RecordPerPage);

                return new ResultActionService(true, "Successfully fetching the project", payload);
            }
            catch (Exception e)
            {
                _logger.LogError("Error Fetching all project with message: {Message}", e.Message);
                return new ResultActionService(false, e.Message, null);
            }
        }

        [HttpPost("get")]
        //[Authorize]
        public async Task<IActionResult> GetById([FromBody] ProjectByIdRequest request)
        {
            try
            {
                _logger.LogInformation("Fetching project by id: {ID}", request.Id);
                var project = await _projectRepository.find(request.Id);
                if(project == null)
                {
                    return new ResultActionService(true, "Data project not found", null);
                }

                return new ResultActionService(true, "Successfully get the project", project);
            }
            catch (Exception e)
            {
                _logger.LogError("Error get project with message: {Message}", e.Message);
                return new ResultActionService(false, e.Message, null);
            }
        }

        [HttpPost("create")]
        //[Authorize]
        public async Task<IActionResult> Store([FromBody] CreateProjectRequest request)
        {
            try
            {
                _logger.LogInformation("Storing project with name: {Name}", request.Name);
                await _projectRepository.store(new Project
                {
                    Name = request.Name,
                    Stage = request.Stage,
                    Category = request.Category,
                    Others = request.Others,
                    StartDate = request.StartDate,
                    Description = request.Description,
                    CreatedBy = 1,
                    LastUpdatedBy = 1
                });

                return new ResultActionService(true, "Successfully create the project", null);
            }
            catch (Exception e)
            {
                _logger.LogError("Error create project with message: {Message}", e.Message);
                return new ResultActionService(false, e.Message, null);
            }
        }

        [HttpPost("update")]
        //[Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateProjectRequest request)
        {
            try
            {
                _logger.LogInformation("Updating project with name: {Name}", request.Name);
                await _projectRepository.update(new Project
                {
                    Id = request.Id,
                    Name = request.Name,
                    Stage = request.Stage,
                    Category = request.Category,
                    Others = request.Others,
                    StartDate = request.StartDate,
                    Description = request.Description,
                    CreatedBy = 1,
                    LastUpdatedBy = 1
                });

                return new ResultActionService(true, "Successfully update the project", null);
            }
            catch (Exception e)
            {
                _logger.LogError("Error update project with message: {Message}", e.Message);
                return new ResultActionService(false, e.Message, null);
            }
        }

        [HttpPost("delete")]
        //[Authorize]
        public async Task<IActionResult> RemoveById([FromBody] ProjectByIdRequest request)
        {
            try
            {
                _logger.LogInformation("Removing project with id: {Id}", request.Id);
                await _projectRepository.destroy(request.Id);

                return new ResultActionService(true, "Successfully remove the project", null);
            }
            catch (Exception e)
            {
                _logger.LogError("Error remove project with message: {Message}", e.Message);
                return new ResultActionService(false, e.Message, null);
            }
        }
    }
}
