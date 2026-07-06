using MovieManager.DAL.Data;
using MovieManager.DAL.Entities;
using MovieManager.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
