using EmailConfirmation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmailConfirmation.Context
{
    public class UserContext : DbContext
    {
        public DbSet<User> User { get; set; }
    }
}