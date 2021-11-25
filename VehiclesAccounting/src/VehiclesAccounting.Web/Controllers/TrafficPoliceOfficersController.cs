﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehiclesAccounting.Core.Interfaces;
using VehiclesAccounting.Core.ProjectAggregate;
using VehiclesAccounting.Core.Services;
using VehiclesAccounting.Web.ViewModels;
using VehiclesAccounting.Web.ViewModels.TrafficPoliceOfficers;

namespace VehiclesAccounting.Web.Controllers
{
    [Authorize(Roles = "admin, moder")]
    public class TrafficPoliceOfficersController : Controller
    {
        private readonly ITrafficPoliceOfficerServiceAsync _service;
        public TrafficPoliceOfficersController(ITrafficPoliceOfficerServiceAsync service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(SortState sortOrder, int page = 1)
        {
            int pageSize = 20;
            IEnumerable<TrafficPoliceOfficer> trafficPoliceOfficers = await _service.Sort(sortOrder);
            int count = trafficPoliceOfficers.ToList().Count;
            List<TrafficPoliceOfficer> items = trafficPoliceOfficers.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            TrafficPoliceOfficerViewModel viewModel = new()
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                TrafficPoliceOfficers = items
            };
            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            IEnumerable<TrafficPoliceOfficer> trafficPoliceOfficers = await _service.ReadAllAsync();
            TrafficPoliceOfficer trafficPoliceOfficer = trafficPoliceOfficers.ToList().FirstOrDefault(t => t.Id == id);
            if (trafficPoliceOfficer == null)
            {
                return NotFound();
            }
            return View(trafficPoliceOfficer);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Patronymic,Birthday,Post")] TrafficPoliceOfficer trafficPoliceOfficer)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(trafficPoliceOfficer);
                return RedirectToAction(nameof(Index));
            }
            return View(trafficPoliceOfficer);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var trafficPoliceOfficer = await _service.UpdateByIdAsync((int)id);
            if (trafficPoliceOfficer == null)
            {
                return NotFound();
            }
            return View(trafficPoliceOfficer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Patronymic,Birthday,Post")] TrafficPoliceOfficer trafficPoliceOfficer)
        {
            if (id != trafficPoliceOfficer.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(trafficPoliceOfficer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrafficPoliceOfficerExists(trafficPoliceOfficer.Id))
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
            return View(trafficPoliceOfficer);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            TrafficPoliceOfficer trafficPoliceOfficer = await _service.GetByIdAsync((int)id);
            if (trafficPoliceOfficer == null)
            {
                return NotFound();
            }
            return View(trafficPoliceOfficer);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trafficPoliceOfficer = await _service.GetByIdAsync(id);
            await _service.DeleteAsync(trafficPoliceOfficer);
            return RedirectToAction(nameof(Index));
        }
        private bool TrafficPoliceOfficerExists(int id)
        {
            return _service.GetByIdAsync(id) is not null;
        }
    }
}
