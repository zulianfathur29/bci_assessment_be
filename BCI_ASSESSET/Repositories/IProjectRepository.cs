using BCI_ASSESSET.Models;

namespace BCI_ASSESSET.Repositories
{
    public interface IProjectRepository
    {
        Task<PaginatedResult<Project>> getAll(string name, int offset, int limit);
        Task<Project> find(string id);
        Task<bool> store(Project project);
        Task<bool> update(Project project);
        Task<bool> destroy(string id);
    }
}
