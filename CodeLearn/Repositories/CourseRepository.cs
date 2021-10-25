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
    public class CourseRepository : ICourseRepository
    {
        private readonly IDbContextFactory<ApplicationDBContext> _applicationDbContext;

        public CourseRepository(IDbContextFactory<ApplicationDBContext> applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void UpdateRating(Course course, int value)
        {
            using var context = _applicationDbContext.CreateDbContext();
            if (value > 0)
            {
                //course.TotalRating += value;
                //course.RateCount += 1;

                context.Courses.Update(course);
                context.SaveChanges();
            }
        }

        public void AddCourse(Course course)
        {
            using var context = _applicationDbContext.CreateDbContext();
            context.Courses.Add(course);
            context.SaveChanges();
        }

        public void UpdateCourse(Course course)
        {
            using var context = _applicationDbContext.CreateDbContext();
            context.Courses.Update(course);
            context.SaveChanges();
        }

        public void DeleteCourse(Course course)
        {
            using var context = _applicationDbContext.CreateDbContext();
            context.Courses.Remove(course);
            context.SaveChanges();
        }

        public List<Course> GetAllCourse()
        {
            using var context = _applicationDbContext.CreateDbContext();
            return context.Courses.OrderBy(x => x.Name).ToList();
        }

        public Course GetSingleCourse(string id)
        {
            using var context = _applicationDbContext.CreateDbContext();
            return context.Courses.FirstOrDefault(x => x.Id.ToString() == id);
        }

        public string GetSingleCourseNameById(string id)
        {
            using var context = _applicationDbContext.CreateDbContext();
            return context.Courses.FirstOrDefault(x => x.Id.ToString() == id).Name;
        }

        public List<string> GetCoursesName()
        {
            using var context = _applicationDbContext.CreateDbContext();
            List<string> courseName = new List<string>();
            foreach (var course in context.Courses)
            {
                courseName.Add(course.Name);
            }
            return courseName;
        }

        public List<Guid> GetCourseTypeId()
        {
            using var context = _applicationDbContext.CreateDbContext();
            List<Guid> courseTypeId = new List<Guid>();
            foreach (var item in context.Courses)
            {
                courseTypeId.Add(item.CourseTypeId);
            }
            return courseTypeId;
        }

        
    }
}
