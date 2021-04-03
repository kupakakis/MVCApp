using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCApp_Entities;
using MVCApp_DAL;
using MVCApp_Utilities;

namespace MVCApp_BLL
{
    public class PatientBLL
    {
        public List<PatientEntities> GetPatients()
        {
            List<PatientEntities> result = new List<PatientEntities>();
            PatientDAL patientDAL = new PatientDAL();
            try
            {
                result = patientDAL.GetPatient.ToList();
            }
            catch (Exception exception)
            {
                
            }
            return result;
        }

        public void AddPatient(PatientEntities patientEntities)
        {
            PatientDAL patientDAL = new PatientDAL();
            patientDAL.CheckPatient(patientEntities);

            if (patientDAL.sameDrug)
            {
                throw new Exception(ValidationMessage.COMPARE_ALREADY_EXIST);
            }
            patientDAL.AddPatient(patientEntities);
        }

    }
}
