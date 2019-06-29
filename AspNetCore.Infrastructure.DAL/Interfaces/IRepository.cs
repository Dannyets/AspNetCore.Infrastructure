using BaseRepositories.EntityFrameworkCore.Interfaces;
using BaseRepositories.EntityFrameworkCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Infrastructure.DAL.Interfaces
{
    public interface IRepository<T> : IEfRepository<T> where T : BaseEntity
    {

    }
}
