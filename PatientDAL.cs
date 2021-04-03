using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MVCApp_Entities;

namespace MVCApp_DAL
{
    public class PatientDAL
    {
		public IEnumerable<PatientEntities> GetPatient
		{
			get
			{
				string connectionString = ConfigurationManager.ConnectionStrings["PatientDB"].ConnectionString;

				List<PatientEntities> patients = new List<PatientEntities>();

				using (SqlConnection con = new SqlConnection(connectionString))
				{
					SqlCommand cmd = new SqlCommand("spGetPatient", con);
					cmd.CommandType = CommandType.StoredProcedure;
					con.Open();
					SqlDataReader rdr = cmd.ExecuteReader();
					while (rdr.Read())
					{
						PatientEntities patient = new PatientEntities();
						patient.Id = Convert.ToInt32(rdr["Id"]);
						patient.PatientName = rdr["PatientName"].ToString();
						patient.Drug = rdr["Drug"].ToString();
						patient.Dosage = Convert.ToDecimal(rdr["Dosage"]);
						patient.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);

						patients.Add(patient);
					}
				}

				return patients;
			}
		}

		public void AddPatient (PatientEntities patient)
		{
			string connectionString = ConfigurationManager.ConnectionStrings["PatientDB"].ConnectionString;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand("spAddPatient", con);
				cmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramPatients = new SqlParameter();
				paramPatients.ParameterName = "@Patients";
				paramPatients.Value = patient.PatientName;
				cmd.Parameters.Add(paramPatients);

				SqlParameter paramDosage = new SqlParameter();
				paramDosage.ParameterName = "@Dosage";
				paramDosage.Value = patient.Dosage;
				cmd.Parameters.Add(paramDosage);

				SqlParameter paramDrug = new SqlParameter();
				paramDrug.ParameterName = "@Drug";
				paramDrug.Value = patient.Drug;
				cmd.Parameters.Add(paramDrug);

				con.Open();
				cmd.ExecuteNonQuery();
			}
		}

		public void CheckPatient (PatientEntities patient)
        {
			string connectionString = ConfigurationManager.ConnectionStrings["PatientDB"].ConnectionString;
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand("spCheckPatient", con);
				cmd.CommandType = CommandType.StoredProcedure;

				string parameterName = ifnull(patient.PatientName, "");

				SqlParameter paramPatientName = new SqlParameter();
				paramPatientName.ParameterName = "@PatientName";
				paramPatientName.Value = parameterName;
				cmd.Parameters.Add(paramPatientName);

				string parameterDrug = ifnull(patient.Drug, "");

				SqlParameter paramDrug = new SqlParameter();
				paramDrug.ParameterName = "@Drug";
				paramDrug.Value = parameterDrug;
				cmd.Parameters.Add(paramDrug);

				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				if (reader.HasRows)
				{
					sameDrug = true;
				}
				else
				{
					sameDrug = false;
				}

			}
		}

		private string ifnull(string text, string nullValue)
		{
			if (string.IsNullOrEmpty(text))
			{
				text = nullValue;
			}
			return text;
		}

		public bool sameDrug;
	}
}
