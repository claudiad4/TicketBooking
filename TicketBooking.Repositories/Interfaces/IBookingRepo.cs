﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Entities;

namespace TicketBooking.Repositories.Interfaces
{
    public interface IBookingRepo
    {
     Task<IEnumerable<Booking>>GetTodaysBooking(int id);
    }
}
