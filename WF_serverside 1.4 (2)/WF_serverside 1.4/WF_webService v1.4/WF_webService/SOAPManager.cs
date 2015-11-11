using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WF_webService
{
    [Serializable]
    public class SOAP_ResolvePath
    {
        public SOAP_ResolvePath(double CampusToLat, double CampusToLong, string BuildingTitle, string BuildingImage, string[][] Maps)
        {
            //campusTo = new double[] { CampusToLat, CampusToLong };
            //buildingTitle = BuildingTitle;
           // buildingImage = BuildingImage;
            maps = Maps;
            getAProperSOAPClient = new string[]{""+CampusToLat,""+CampusToLong,BuildingTitle,BuildingImage};
        }
        //public double[] campusTo { get; set; }
       // public string buildingTitle { get; set; }
       // public string buildingImage { get; set; }
        public string[] getAProperSOAPClient { get; set; }
        public string[][] maps { get; set; }
    }

    [Serializable]
    public class SOAP_Get_Rooms
    {
        public SOAP_Get_Rooms(int WaypointID, string RoomName, int BuildingID, string Image)
        {
            waypointID = WaypointID;
            roomName = RoomName;
            building_ID = BuildingID;
            image = Image;

        }

        public int waypointID { get; set; }
        public string roomName { get; set; }
        public int building_ID { get; set; }
        public string image { get; set; }

    }
}