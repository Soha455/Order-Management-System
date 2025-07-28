using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Settings
{
    public class JwtSettings
    {
        public string secretkey { get; set; } = string.empty;
        public string issuer { get; set; } = string.empty;
        public string audience { get; set; } = string.empty;
        public int expirationminutes { get; set; }
    }
}