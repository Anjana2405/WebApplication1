using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Data.Entity.Infrastructure;

namespace WebApplication1.Controllers
{
    public class SchedulesController : Controller
    {
        private readonly HospitalContext _context;
      

        public SchedulesController(HospitalContext context)
        {
            _context = context;
        }


        // GET: Schedules
        public async Task<ActionResult> Index()
        {
            var hospitalContext = _context.schedule.Include(s => s.doc);
            return View(await hospitalContext.ToListAsync());
        }

        // GET: Schedules/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.schedule
               .Include(s => s.doc)
               .FirstOrDefaultAsync(m => m.id == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        private ActionResult NotFound()
        {
            throw new NotImplementedException();
        }

        // GET: Schedules/Create
        public ActionResult Create()
        {
            ViewData["docID"] = new SelectList(_context.doctor, "id", "name");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,docID,schedule")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["docID"] = new SelectList(_context.doctor, "id", "name", schedule.docID);
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            var schedule = await _context.schedule.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            ViewData["docID"] = new SelectList(_context.doctor, "id", "name", schedule.docID);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,docID,schedule")] Schedule schedule, int id)
        {
            if (id != schedule.id)
            {
                return NotFound();
            }

                if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.id))
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
            ViewData["docID"] = new SelectList(_context.doctor, "id", "name", schedule.docID);
            return View(schedule);
        }

        private bool ScheduleExists(int id)
        {
            throw new NotImplementedException();
        }

        // GET: Schedules/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.schedule
               .Include(s => s.doc)
               .FirstOrDefaultAsync(m => m.id == id);
            if (schedule == null)
            {
                return NotFound();
            }
            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var schedule = await _context.schedule.FindAsync(id);
            _context.schedule.Remove(schedule);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
