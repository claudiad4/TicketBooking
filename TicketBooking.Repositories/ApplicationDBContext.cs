﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketBooking.Entities;

namespace TicketBooking.Repositories
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        
        }

        public DbSet<Koncert> KoncertInfo { get; set; }
        public DbSet<MiejscaDetails> MiejscaKoncertDetails { get; set; }
        public DbSet<KupBilet> KupBilet {  get; set; }
      
    }
}
