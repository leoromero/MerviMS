﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Mervi.SeedWork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
