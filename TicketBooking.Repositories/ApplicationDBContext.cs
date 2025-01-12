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
        public DbSet<MiejscaDetail> MiejscaKoncertDetails { get; set; }
        public DbSet<Booking> Booking { get; set; }
    }
}
