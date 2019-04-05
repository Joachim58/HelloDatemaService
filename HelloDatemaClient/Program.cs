using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace HelloDatemaClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceReference1.HelloDatemaServiceClient proxy = new ServiceReference1.HelloDatemaServiceClient();
            DataSet aDataSet;
            var msg = proxy.GetData(101);
            var msg2 = proxy.GetMOLOKO_VSTableRowsCount("APPOINTMENT_DATA");
            aDataSet = proxy.GetMOLOKO_VSAppointments(1);
            var msg3 = String.Format( "SERVICE on {0} says APPOINTMENT_DATA.COUNT for userid 1 is {1}", aDataSet.Tables["SERVICE"].Rows[0][0], aDataSet.Tables["APPOINTMENT_DATA"].Rows.Count);
           
            aDataSet = proxy.GetMOLOKO_VSAppointments(8);
            var msg4 = String.Format("SERVICE on {0} says APPOINTMENT_DATA.COUNT for userid 8 is {1}", aDataSet.Tables["SERVICE"].Rows[0][0], aDataSet.Tables["APPOINTMENT_DATA"].Rows.Count);

            HelloDatemaService.DSStreckenplan aDSStreckenplan;
            int iUserid = 8;
            aDSStreckenplan = proxy.GetDSStreckenplan(iUserid);
            string sXML = aDSStreckenplan.GetXml(); //Length about 2.500.000 for user 8, 8.000 for user 1'
            var msg5 = String.Format("SERVICE on {0} returns DSStreckenplan for userid {1} with {2} Appointments, XML Size is {3}", 
                aDSStreckenplan.SERVICE[0].MACHINENAME, iUserid, aDSStreckenplan.APPOINTMENT_DATA.Count, sXML.Length);
            var msg6 = string.Empty;
            HelloDatemaService.HelloDatemaService.PutDSStreckenplanResult aPutDSStreckenplanResult;
            try
            {
                aPutDSStreckenplanResult = proxy.PutDSStreckenplan(aDSStreckenplan);
                msg6 = String.Format("Service on {0} recieved {1} Appointments.", aPutDSStreckenplanResult.MachineName, aPutDSStreckenplanResult.Appointment_dataCount);
            }
            catch (System.Exception ex)
            {
                msg6 = ex.ToString();
            }       
            proxy.Close();
            Console.WriteLine(msg);
            Console.WriteLine(msg2);
            Console.WriteLine(msg3);
            Console.WriteLine(msg4);
            Console.WriteLine(msg5);
            Console.WriteLine(msg6);
            Console.ReadKey();
        }
    }
}
