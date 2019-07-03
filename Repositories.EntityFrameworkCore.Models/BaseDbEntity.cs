using System;

namespace AspNetCore.Infrastructure.Repositories.EntityFrameworkCore.Models
{
    public abstract class BaseDbEntity
    {
        public DateTime CreatedAt { get; set; }

        public DateTime LastUdpatedAt { get; set; }
    }
}
