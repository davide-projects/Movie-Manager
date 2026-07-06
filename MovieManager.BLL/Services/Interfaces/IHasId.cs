using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.BLL.Services.Interfaces
{
    /// <summary>
    /// Interface that defines an entity with an Id property.
    /// </summary>
    public interface IHasId
    {
        int Id { get; }
    }
}
