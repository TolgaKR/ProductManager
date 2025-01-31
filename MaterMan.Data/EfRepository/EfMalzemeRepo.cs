﻿using MaterMan.Data.Abstract;
using MaterMan.Data.Concrete;
using MaterMan.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterMan.Data.EfRepository
{
    public class EfMalzemeRepo : GenericRepository<Malzeme>, IMalzemeDal
    {
        private AppDbContext _appDbContext;
        public EfMalzemeRepo(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task MalzemeDeleteAsync(int malzemeId)
        {
            var result = await _appDbContext.Malzemeler.Where(x => x.Id == malzemeId).FirstOrDefaultAsync();
            result.IsActive = false;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<Malzeme>> MalzemeList()
        {
            var result = await _appDbContext.Malzemeler.Where(x => x.IsActive == true).Include(x => x.MalzemeGrup).Include(x => x.MalzemeBirim).ToListAsync();

            return result;
        }

    }
}
