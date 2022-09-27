using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceManagement_WebApp.Models;
using DeviceManagement_WebApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagement_WebApp.Repository
{
 

    public interface IZoneRepository : IGenericRepository<Zone>
    {
        IEnumerable<Zone> GetAll();
        void Add(Zone zone);
        Zone GetById(Guid id);
        void Save();



    }

}
