﻿using CodeLearn.Data;
using CodeLearn.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IDbContextFactory<ApplicationDBContext> _context;
        private readonly RoleManager<IdentityRole> _roleManager; 
        public RoleRepository(RoleManager<IdentityRole> roleManager,IDbContextFactory<ApplicationDBContext> context)
        {
            _context = context;
            _roleManager = roleManager;
        }
        public async Task<IdentityRole> Add(IdentityRole role)
        {
            var result = await _roleManager.CreateAsync(role);
            return role; 
        }

        public async Task Delete(IdentityRole role)
        {
            await _roleManager.DeleteAsync(role);
        }

        public Task<IdentityRole> Edit(IdentityRole role)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityRole> FindByName(string name)
        {
            var role = _roleManager.FindByNameAsync(name);
            return role; 
        }

        public async Task<IEnumerable<IdentityRole>> GetAllRole()
        {
            var listRole = await _roleManager.Roles.ToListAsync();
            return listRole;
        }
    }
}