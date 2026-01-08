using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Core.Entities;

namespace RealEstate.Core.Interfaces
{
    public interface IPropertyRepository
    {
        Task<List<Property>> GetAllPropertiesAsync();
    }
}
