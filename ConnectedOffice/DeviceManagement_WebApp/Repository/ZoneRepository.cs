using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceManagement_WebApp.Models;
using DeviceManagement_WebApp.Repository;
using DeviceManagement_WebApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagement_WebApp.Repository
{
    public class ZoneRepository : GenericRepository<Zone>, IZoneRepository
    {
        public ZoneRepository(ConnectedOfficeContext context) : base(context)
        {
        }

        public void Save()
        {
            //_context.SaveChanges();
        }

        public IEnumerable<Zone>GetAll()
        {
            return _context.Zone.ToList();
        }
        
        [HttpPost]
        public void Add(Zone zone)
        {
            //Zone add = new Zone { ZoneName = zone.ZoneName, ZoneDescription = zone.ZoneDescription };
            
            _context.Zone.Add(zone);
         }

        public Zone GetByID(Guid id)
        {
            return _context.Zone.Find(id);
        }

      
      
      



    }


   

    
    // TO DO: Add ‘Create’
  

    // TO DO: Add ‘Edit’
    // TO DO: Add ‘Delete’
    // TO DO: Add ‘Exists’
}


