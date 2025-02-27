using BCI_ASSESSET.DB;
using BCI_ASSESSET.Models;
using Microsoft.EntityFrameworkCore;

namespace BCI_ASSESSET.Repositories
{
    public class ProjectRepository: IProjectRepository
    {
        private readonly AppDbContext _context;
        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedResult<Project>> getAll(string name, int page, int recordPerPage)
        {
            var query = _context.Projects.AsQueryable();

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * recordPerPage)
                .Take(recordPerPage)
                .ToListAsync();

            return new PaginatedResult<Project>
            {
                Items = items,
                TotalCount = totalCount,
                RecordPerPage = recordPerPage,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalCount / (double)recordPerPage),
            };
        }
        public async Task<Project> find(string id)
        {
            return await _context.Projects.FindAsync(id);
        }
        public async Task<bool> store(Project project)
        {
            try
            {
                _context.Projects.Add(project);
                await _context.SaveChangesAsync();
                await _context.Entry(project).ReloadAsync();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task<bool> update(Project project)
        {
            _context.Entry(project).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task<bool> destroy(string id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                try
                {
                    _context.Projects.Remove(project);
                    await _context.SaveChangesAsync();

                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
