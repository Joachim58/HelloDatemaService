
using System.ServiceModel;
using System.Data;


namespace HelloDatemaService
{
    
    [ServiceContract]
    public interface IHelloDatemaService
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        string GetMOLOKO_VSTableRowsCount(string sTableName);

        [OperationContract]
        DataSet GetMOLOKO_VSAppointments(int iMitarbeiter_id);

        [OperationContract]
        DSStreckenplan GetDSStreckenplan(int iMitarbeiter_id);

        [OperationContract]
        HelloDatemaService.PutDSStreckenplanResult PutDSStreckenplan(DSStreckenplan aDSStreckenplan);
        
    }

   
}
