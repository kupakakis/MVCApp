using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCApp_BLL;
using MVCApp_Entities;

namespace MVCApp.Areas.Patient.Controllers
{
    public class PatientController : Controller
    {
        PatientBLL patientBLL = new PatientBLL();
        // GET: Patient/Patient
        public ActionResult Index()
        {
            List<PatientEntities> patients = patientBLL.GetPatients();
            return View(patients);
        }
        public ActionResult CreatePatient()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePatient(PatientEntities patientEntities)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}