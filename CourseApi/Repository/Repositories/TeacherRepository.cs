using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Repository.Repositories
{
    public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(AppDbContext context) : base(context)
        {

        }
        public async Task<Teacher> GetByIdWithAsync(int? id)
        {
            if (id == null) { throw new ArgumentNullException(nameof(id)); }
            var data = await _context.Teachers.AsNoTracking().Include(s => s.GroupTeachers)
            .ThenInclude(gs => gs.Group).FirstOrDefaultAsync(m => m.Id == id);
            return data;
        }
        public async Task<IEnumerable<Teacher>> GetAllWithAsync()
        {
            var teachers = await _context.Teachers
            .Include(s => s.GroupTeachers)
            .ThenInclude(gs => gs.Group)
            .ToListAsync();


            return teachers;
        }

        public async Task<IEnumerable<Teacher>> GetByNameOrSurname(string nameOrSurname)
        {
            var data = await _context.Teachers.Where(m => m.Name.Trim().Contains(nameOrSurname.Trim())||m.Surname.Contains(nameOrSurname.Trim())).ToListAsync();
            return data;
        }
    }
}
