﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_Commerce.Identity
{
    public class IdentityDataContext :IdentityDbContext<ApplicationUser>
    {
        public IdentityDataContext() :base ("dataConnection")
        {

        }
    }
}