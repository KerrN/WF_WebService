using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WF_webService
{
    [ServiceContract]
    public interface WF_Service_Interface
    {
        // Testing methods
        [OperationContract]
        bool checkServiceConn();
        
        [OperationContract]
        bool checkDBConn();

        [OperationContract]
        void deleteImages(string[] urls);

        [OperationContract]
        void deleteImagesIOS(string urls);

        [OperationContract]
        byte[] getImage(String path);

        [OperationContract]
        string[][] SearchCampus();

        [OperationContract]
        int CampusVersion(string CampusID);

        [OperationContract]
        string[][] SearchRooms(string CampusID);

        [OperationContract]
        SortedList<int, string> SearchServices(string CampusID);

        [OperationContract]
        SOAP_ResolvePath ResolvePath(int WaypointID, bool Disability);


        // Testing Methods
        [OperationContract]
        List<List<Waypoint>> GetResolvePathLists(int WaypointID, bool Disability);
        [OperationContract]
        string RunImageProcessor(string roomName, List<Waypoint> sequence, int xanchor, int yacnhor, double scale);
        [OperationContract]
        void UpdateMaps();
    }
}
