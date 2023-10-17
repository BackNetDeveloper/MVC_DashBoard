﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DataAccessLayer_DAL_.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
        public bool IsAgree { get; set; }
    }
}
