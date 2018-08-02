﻿using System.Collections.Generic;

namespace Basket.BL.Interfaces
{
    public interface IMapper<T, G>
    {
        G ToDto(T entity);
        T ToEntity(G dto);
        IEnumerable<G> ToDtos(IEnumerable<T> entities);
        IEnumerable<T> ToEntities(IEnumerable<G> dtos);
    }
}
