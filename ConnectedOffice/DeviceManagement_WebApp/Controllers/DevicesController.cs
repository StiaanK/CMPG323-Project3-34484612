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
    public class DevicesController : Controller
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IZoneRepository _zoneRepository;
        public DevicesController(IDeviceRepository deviceRepository, ICategoryRepository categoryRepository, IZoneRepository zoneRepository)
        {
            _deviceRepository = deviceRepository;
            _categoryRepository = categoryRepository;
            _zoneRepository = zoneRepository;
        }


        // Retrieves all the device records from DB
        public async Task<IActionResult> Index()
        {
            return View(_deviceRepository.GetAll());
        }

        // Recieve Details of a device record
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = _deviceRepository.GetById(id);

            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // Adds new device record to DB
        // GET part of Create 
        public IActionResult Create()
        {
           //List<Category> clist = _catagoryRepository;
            
            ViewData["CategoryId"] = new SelectList(_categoryRepository.GetAll() /*,"CategoryID", "CategoryName"*/);
            ViewData["ZoneId"] = new SelectList(_zoneRepository.GetAll()  /*,"ZoneId", "ZoneName"*/);
            return View();
            
        }

        //POST part of Create()
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeviceId, DeviceName, CategoryId, ZoneId, Status, IsActive, DateCreated")] Device device)
        {
            device.DeviceId = Guid.NewGuid();
        
            _deviceRepository.Add(device);
            _deviceRepository.Save();



            return RedirectToAction(nameof(Index));
        }

        // Edits a device record on DB
        // GET part of Edit()
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = _deviceRepository.GetById(id);
            if (zone == null)
            {
                return NotFound();
            }
            return View(zone);
        }

        // POST part of Edit()
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DeviceId,DeviceName,DeviceDescription,DateCreated")] Device device)
        {
            if (id != device.DeviceId)
            {
                return NotFound();
            }

            try
            {
                _deviceRepository.Update(device);
                _deviceRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(device.DeviceId))
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

        // Removes device record from DB
        // GET part of Delete()
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = _deviceRepository.GetById(id);

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
            var device = _deviceRepository.GetById(id);
            _deviceRepository.Remove(device);
            _deviceRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        // Checks if Zone record exists
        private bool DeviceExists(Guid id)
        {
            Device device = _deviceRepository.GetById(id);

            if (device != null)
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
