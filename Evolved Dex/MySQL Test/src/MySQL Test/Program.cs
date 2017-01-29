using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MySQL_Test
{
    public class User
    {
        public int UserId { get; set; }

        [MaxLength(64)]
        public string Name { get; set; }
    }

    public class Blog
    {
        public Guid Id { get; set; }

        [MaxLength(32)]
        public string Title { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public string Content { get; set; }

        public JsonObject<List<string>> Tags { get; set; } // Json storage (MySQL 5.7 only)
    }

    public class MyContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseMySql(@"Server=localhost;database=efMySqlTest;uid=root;pwd=Nalyd1$cool;");
    }

    public class Program
    {
        public static void Main()
        {
            using (var context = new MyContext())
            {
                // Create database
                context.Database.EnsureCreated();

                // Init sample data
                var user = new User { Name = "Yuuko" };
                context.Add(user);
                var blog1 = new Blog
                {
                    Title = "Title #1",
                    UserId = user.UserId,
                    Tags = new List<string>() { "ASP.NET Core", "MySQL", "Pomelo" }
                };
                context.Add(blog1);
                var blog2 = new Blog
                {
                    Title = "Title #2",
                    UserId = user.UserId,
                    Tags = new List<string>() { "ASP.NET Core", "MySQL" }
                };
                context.Add(blog2);
                context.SaveChanges();

                // Changing and save json object #1
                blog1.Tags.Object.Clear();
                context.SaveChanges();

                // Changing and save json object #2
                blog1.Tags.Object.Add("Pomelo");
                context.SaveChanges();

                // Output data
                var ret = context.Blogs
                    .Where(x => x.Tags.Object.Contains("Pomelo"))
                    .ToList();
                foreach (var x in ret)
                {
                    Console.WriteLine($"{ x.Id } { x.Title }");
                    Console.Write("[Tags]: ");
                    foreach (var y in x.Tags.Object)
                        Console.Write(y + " ");
                    Console.WriteLine();
                }
            }
            Console.Read();
        }
    }

}
