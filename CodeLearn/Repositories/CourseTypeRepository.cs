﻿using CodeLearn.Data;
using CodeLearn.Models;
using CodeLearn.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories
{
    public class CourseTypeRepository : ICourseTypeRepository
    {
        private readonly IDbContextFactory<ApplicationDBContext> _applicationDbContext;
        public CourseTypeRepository(IDbContextFactory<ApplicationDBContext> applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
            
        public void AddCourseType(CourseType courseType)
        {
            using var context = _applicationDbContext.CreateDbContext();
            context.CourseTypes.Add(courseType);
            context.SaveChanges();
        }

        public List<CourseType> GetAllCourseType()
        {
            using var context = _applicationDbContext.CreateDbContext();
            return context.CourseTypes.ToList();
        }
    }
}
