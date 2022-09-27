using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using DeviceManagement_WebApp.Repository;
using Microsoft.AspNetCore.Authorization;

namespace DeviceManagement_WebApp.Controllers
{
    [Authorize]
    public class ZonesController : Controller
    {    
        private readonly IZoneRepository _zoneRepository;
        public ZonesController(IZoneRepository zoneRepository)
        {
            _zoneRepository = zoneRepository;
        }


        // Retrieves all the Zone records from DB
        public async Task<IActionResult> Index()
        {
            return View(_zoneRepository.GetAll());
        }

        // Recieve Details of a Zone record
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = _zoneRepository.GetById(id);
                
            if (zone == null)
            {
                return NotFound();
            }

            return View(zone);
        }

        // Adds new Zone record to DB
        // GET part of Create 
        public IActionResult Create()
        {
            return View();
        }

        //POST part of Create()
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone)
        {
            zone.ZoneId = Guid.NewGuid();
            _zoneRepository.Add(zone);
            _zoneRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        // Edits a Zone record on DB
        // GET part of Edit()
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = _zoneRepository.GetById(id);
            if (zone == null)
            {
                return NotFound();
            }
            return View(zone);
        }

        // POST part of Edit()
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone)
        {
            if (id != zone.ZoneId)
            {
                return NotFound();
            }

            try
            {
                _zoneRepository.Update(zone);
                _zoneRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZoneExists(zone.ZoneId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

        }

        // Removes Zone record from DB
        // GET part of Delete()
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = _zoneRepository.GetById(id); 
                
            if (zone == null)
            {
                return NotFound();
            }

            return View(zone);
        }

        // POST part of Delete()
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var zone = _zoneRepository.GetById(id);
            _zoneRepository.Remove(zone);
            _zoneRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        // Checks if Zone record exists
        private bool ZoneExists(Guid id)
        {
            Zone zone = _zoneRepository.GetById(id);

            if (zone != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
