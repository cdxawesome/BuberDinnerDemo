﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Common.Interface.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Guid userId, string firstname,string lastname);
    }
}