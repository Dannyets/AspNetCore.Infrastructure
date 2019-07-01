using System;

namespace AspNetCore.Infrastructure.EfCore.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUdpatedAt { get; set; }
    }
}
