﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ispit.Todo.Data;
using Ispit.Todo.Models;
using Microsoft.AspNetCore.Identity;

namespace Ispit.Todo.Controllers
{
    public class TaskListsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;

        public TaskListsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: TaskLists
        public async Task<IActionResult> Index()
        {
              return _context.TaskList != null ? 
                          View(await _context.TaskList.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TaskList'  is null.");
        }

        // GET: TaskLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TaskList == null)
            {
                return NotFound();
            }

            var taskList = await _context.TaskList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskList == null)
            {
                return NotFound();
            }

            return View(taskList);
        }

        // GET: TaskLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaskLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Akcija,JeRjeseno,Created")] TaskList taskList)
        {
            //if (ModelState.IsValid)
            //{

            //} 

          
            _context.Add(taskList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           
        }

        // GET: TaskLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TaskList == null)
            {
                return NotFound();
            }

            var taskList = await _context.TaskList.FindAsync(id);
            if (taskList == null)
            {
                return NotFound();
            }
            return View(taskList);
        }

        // POST: TaskLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Akcija,JeRjeseno,Created")] TaskList taskList)
        {
            if (id != taskList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskListExists(taskList.Id))
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
            return View(taskList);
        }

        // GET: TaskLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TaskList == null)
            {
                return NotFound();
            }

            var taskList = await _context.TaskList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskList == null)
            {
                return NotFound();
            }

            return View(taskList);
        }

        // POST: TaskLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TaskList == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TaskList'  is null.");
            }
            var taskList = await _context.TaskList.FindAsync(id);
            if (taskList != null)
            {
                _context.TaskList.Remove(taskList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskListExists(int id)
        {
          return (_context.TaskList?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
