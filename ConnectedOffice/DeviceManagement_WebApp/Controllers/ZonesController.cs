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
using Microsoft.AspNetCore.Http;
using System.Data;

namespace DeviceManagement_WebApp.Controllers
{
    public class ZonesController : Controller
    {


        protected readonly new IZoneRepository _zoneRepository;
        protected readonly ConnectedOfficeContext _context;

      
        public ZonesController(IZoneRepository zoneRepository, ConnectedOfficeContext context)
        {
            _zoneRepository = zoneRepository;
            _context = context;
        }

        // GET: Zones
        public async Task<IActionResult> Index()
        {
            return View(_zoneRepository.GetAll());

        }

        //GETBYID : Zones
        public ViewResult Details(Guid id)
        {
            Zone zone = _zoneRepository.GetById(id);
            return View(zone);
        }




        /*/create
        [HttpGet]
        public ActionResult Create()
        {
            //Zone add = new Zone { ZoneName = zone.ZoneName, ZoneDescription = zone.ZoneDescription };
            return View();
        }*/


        //[HttpPost]
        public ActionResult Create(Zone zone)
        {
            RedirectToAction("Create");
            Zone add = new Zone { ZoneName = zone.ZoneName, ZoneDescription = zone.ZoneDescription };
            //_zoneRepository.Add(zone);

            _zoneRepository.Save();
           

            
            //try
            //{
                if (ModelState.IsValid)
                {
                    _zoneRepository.Add(add);
                    _zoneRepository.Save();
                    return RedirectToAction("Index");
                }
            //}
            /*catch (DataException  dex)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }*/
            return View(zone);
        }

    }
}

