using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TheatreWeb.Models;

namespace TheatreWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TheatreWeb.Models.Afisha> Afisha { get; set; }
        public DbSet<TheatreWeb.Models.Actors> Actors { get; set; }
        public DbSet<TheatreWeb.Models.Performances> Performances { get; set; }
        public DbSet<TheatreWeb.Models.Seats> Seats { get; set; }
        public DbSet<TheatreWeb.Models.Halls> Halls { get; set; }
    }
}
