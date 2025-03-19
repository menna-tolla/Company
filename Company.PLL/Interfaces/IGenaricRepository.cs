﻿using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IGenaricRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();

        T? Get(int Id);

        int Add(T model);

        int Update(T model);

        int Delete(T model);
    }
}
