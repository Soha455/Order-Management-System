﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstractions
{
    public interface IPaymentService
    {
        Task<bool> ProcessPaymentAsync(Order order);
    }
}
