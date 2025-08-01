﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicesAbstractions.Enums;

namespace ServicesAbstractions
{
    public interface IPaymentServiceFactory
    {
        IPaymentService GetPaymentService(PaymentMethod method);
    }

}
