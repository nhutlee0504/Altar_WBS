using ALtar_WBS.Data;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALtar_WBS.Service
{
    public class ServiceClasses : InterfaceClasses
    {
        private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ServiceClasses(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Classes>> GetAllClassesAsync()
        {
            try
            {
                var classes = await _context.classes.ToListAsync();

                if (classes == null || !classes.Any())
                {
                    throw new InvalidOperationException("No classes found.");
                }

                return classes;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving classes: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred: " + ex.Message);
            }
        }

        public async Task<IEnumerable<Classes>> GetClassesByCourseIdAsync(int courseId)
        {
            try
            {
                var classes = await _context.classes
                    .Where(c => c.CourseID == courseId)
                    .ToListAsync();

                if (classes == null || !classes.Any())
                {
                    throw new InvalidOperationException("No classes found for the specified course.");
                }

                return classes;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving classes: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred: " + ex.Message);
            }
        }

        public async Task<Classes> GetClassByIdAsync(int classId)
        {
            try
            {
                var classEntity = await _context.classes
                    .FirstOrDefaultAsync(c => c.ClassID == classId);

                if (classEntity == null)
                {
                    throw new InvalidOperationException("Class not found.");
                }

                return classEntity;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving the class: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred: " + ex.Message);
            }
        }
        public async Task<Classes> CreateClassAsync(int idCourse)
        {
            try
            {
                var course = await _context.courses.FindAsync(idCourse);
                if (course == null)
                {
                    throw new InvalidOperationException("Course not found.");
                }

                var newClass = new Classes
                {
                    CourseID = idCourse
                };

                _context.classes.Add(newClass);
                await _context.SaveChangesAsync();

                return newClass;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("An error occurred while creating the class: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred: " + ex.Message);
            }
        }

        public async Task<Classes> UpdateClassAsync(int classId, int idCourse)
        {
            try
            {
                var classEntity = await _context.classes.FindAsync(classId);
                var course = await _context.courses.FindAsync(idCourse);

                if (classEntity == null)
                {
                    throw new InvalidOperationException("Class not found.");
                }

                if (course == null)
                {
                    throw new InvalidOperationException("Course not found.");
                }

                classEntity.CourseID = idCourse;
                await _context.SaveChangesAsync();

                return classEntity;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("An error occurred while updating the class: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred: " + ex.Message);
            }
        }

        public async Task<bool> DeleteClassAsync(int classId)
        {
            try
            {
                var classEntity = await _context.classes.FindAsync(classId);

                if (classEntity == null)
                {
                    throw new InvalidOperationException("Class not found.");
                }

                _context.classes.Remove(classEntity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("An error occurred while deleting the class: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred: " + ex.Message);
            }
        }
    }
}
