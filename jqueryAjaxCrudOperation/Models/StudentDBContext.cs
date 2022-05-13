using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jqueryAjaxCrudOperation.Models
{
    public class StudentDBContext : DbContext
    {
        public StudentDBContext(DbContextOptions<StudentDBContext> options) : base(options) 
        {
        }
        public DbSet<StudentModel> Student { get; set; }  

}
}
