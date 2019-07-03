using AspNetCore.Infrastructure.Repositories.EntityFrameworkCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.EntityFrameworkCore.Models
{
    public abstract class DbIdEntity : BaseDbEntity
    {
        public virtual int Id { get; set; }
    }
}
