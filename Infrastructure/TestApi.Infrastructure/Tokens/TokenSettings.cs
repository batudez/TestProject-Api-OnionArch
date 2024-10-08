﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApi.Infrastructure.Tokens
{
	public class TokenSettings
	{
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string SecretKey { get; set; }
        public int TokenValidityInMinutes { get; set; }
    }
}
