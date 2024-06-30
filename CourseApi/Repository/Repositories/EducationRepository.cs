﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class EducationRepository : BaseRepository<Education>, IEducationRepository
    {
        public EducationRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Education>> GetByName(string name)
        {
            var data = await _context.Educations.Where(m => m.Name.Trim().Contains(name.Trim())).ToListAsync();
            return data;
        }

    }
}