using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly HospitalContext _context;
       

        public AppointmentsController(HospitalContext context)
        {
            _context = context;
        }
        // GET: Appointments
        public async Task<ActionResult> Index()
        {
            var hospitalContext = _context.appointment.Include(a => a.doctor).Include(a => a.patient);
            return View(await hospitalContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.appointment
               .Include(a => a.doctor)
               .Include(a => a.patient)
               .FirstOrDefaultAsync(m => m.id == id);

            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        private ActionResult NotFound()
        {
            throw new NotImplementedException();
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            var specList = _context.doctor.Select(e => new
            {
                id = e.speciality,
                name = e.speciality
            });
            ViewData["docspeciality"] = new SelectList(specList.Distinct(), "id", "name");
            ViewData["docID"] = new SelectList(_context.doctor, "id", "name");
            ViewData["ptID"] = new SelectList(_context.patient, "id", "name");
            return (IActionResult)View();
        }
        public JsonResult FetchDocs(string splty)
        {
            var ddl = _context.doctor.Where(e => e.speciality == splty).Select(e => new
            {
                text = e.name,
                attr = e.id
             });
            return Json(ddl);
        }

            // POST: Appointments/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,docID,ptID,date")] Appointment appointment)
        {
            string DOW = appointment.date.ToString("ddd");
            string schedule = _context.schedule.Single(e => e.docID == appointment.docID).schedule;
            string[] sch = schedule.Split(',');
            string docSchedule = "";
            foreach (string str in sch) //Mon-9_18
            {
                string[] str1 = str.Split('-');
                if (str1[0] == DOW)
                {
                    docSchedule = str1[1];
                    break;
                }
            }

            string[] startend = docSchedule.Split('_');
            int start = int.Parse(startend[0]);
            int end = int.Parse(startend[1]);
            int[] timings = new int[end - start];
            for (int i = 0; i < end - start; i++)
            {
                timings[i] = start + i;
            }
            ViewData["timings"] = new SelectList(timings);

            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["docID"] = new SelectList(_context.doctor, "id", "name", appointment.docID);
            ViewData["ptID"] = new SelectList(_context.patient, "id", "name", appointment.ptID);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var appointment = await _context .appointment.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["docID"] = new SelectList(_context.doctor, "id", "name", appointment.docID);
            ViewData["ptID"] = new SelectList(_context.patient, "id", "name", appointment.ptID);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,docID,ptID,date")] Appointment appointment, int id)
        {
           
            if (id != appointment.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.id))
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
            ViewData["docID"] = new SelectList(_context.doctor, "id", "name", appointment.docID);
            ViewData["ptID"] = new SelectList(_context.patient, "id", "name", appointment.ptID);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var appointment = await _context.appointment
                .Include(a => a.doctor)
                .Include(a => a.patient)
                .FirstOrDefaultAsync(m => m.id == id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
           var appointment = await _context.appointment.FindAsync(id);
            _context.appointment.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
private bool AppointmentExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
