using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace HelloDatemaService
{
    public class HelloDatemaService : IHelloDatemaService
    {
        public string GetData(int value)
        {
            string sMachineName = string.Empty;
            try {
                sMachineName = Environment.MachineName;
                return string.Format("Datema Server {0} says: You entered: {1}", sMachineName, value); 
            }
            catch (System.Exception ex) {
                throw ex;
            }
        }
        public string GetMOLOKO_VSTableRowsCount(string sTableName)
        {
            int iCount = -1;
            SqlConnection aConn = null;
            SqlCommand aCmd = null;
            string sConnectionString = string.Empty;
            string sSQL = string.Empty;
            string sMachineName = string.Empty;
            try {
                sMachineName = Environment.MachineName;
                sSQL = String.Format("SELECT COUNT(*) cnt FROM {0}", sTableName);
                sConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseMOLOKO_VS"].ConnectionString;
                aConn = new SqlConnection(sConnectionString);
                aConn.Open();
                aCmd = new SqlCommand(sSQL, aConn);
                iCount = (int)aCmd.ExecuteScalar();
                aCmd.Dispose();
                aConn.Close();
                return string.Format("Datema Server {0} says: Table {1} has {2} rows", sMachineName, sTableName, iCount);
            }
            catch (System.Exception ex) {
                throw ex;
            }
  
        }
        public DataSet GetMOLOKO_VSAppointments(int iMitarbeiter_id) 
        {
            DataSet aDataSet;
            aDataSet = new DataSet();
            SqlDataAdapter aSQLDataAdapter;
            string sConnectionString = string.Empty;
            string sSQL = string.Empty;
            string sMachineName = string.Empty;
            try
            {
                sMachineName = Environment.MachineName;
                sSQL = String.Format("SELECT * FROM appointment_data WHERE userid = {0}", iMitarbeiter_id);
                sConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseMOLOKO_VS"].ConnectionString;
                aSQLDataAdapter = new SqlDataAdapter(sSQL, sConnectionString);
                aSQLDataAdapter.Fill(aDataSet);
                aDataSet.Tables[0].TableName = "APPOINTMENT_DATA";
                DataTable aTable = aDataSet.Tables.Add("SERVICE");
                aTable.Columns.Add("MACHINENAME");
                DataRow aDataRow = aTable.NewRow();
                aDataRow.ItemArray =new Object[] { sMachineName };
                aTable.Rows.Add(aDataRow);

                return aDataSet; 
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public DSStreckenplan GetDSStreckenplan(int iMitarbeiter_id)
        {
            DSStreckenplan aDSStreckenplan;
            aDSStreckenplan = new DSStreckenplan();
            string sMachineName = string.Empty;
            try
            {
                sMachineName = Environment.MachineName;
                DSStreckenplan.SERVICERow aSERVICERow = aDSStreckenplan.SERVICE.NewSERVICERow();
                aSERVICERow.MACHINENAME = sMachineName;
                aDSStreckenplan.SERVICE.AddSERVICERow(aSERVICERow);

                DSStreckenplanTableAdapters.APPOINTMENT_DATATableAdapter adap;
                adap = new DSStreckenplanTableAdapters.APPOINTMENT_DATATableAdapter();
                adap.FillByUSERID(aDSStreckenplan.APPOINTMENT_DATA, iMitarbeiter_id);

                return aDSStreckenplan;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public PutDSStreckenplanResult PutDSStreckenplan(DSStreckenplan aDSStreckenplan)
        {

            PutDSStreckenplanResult aPutDSStreckenplanResult;
            aPutDSStreckenplanResult = new PutDSStreckenplanResult();
            try
            {
                aPutDSStreckenplanResult.MachineName = Environment.MachineName;
                aPutDSStreckenplanResult.Appointment_dataCount = aDSStreckenplan.APPOINTMENT_DATA.Count;
                return aPutDSStreckenplanResult;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public class PutDSStreckenplanResult
        {
            public string MachineName = string.Empty;
            public int Appointment_dataCount = -1;
        }

    }
}
