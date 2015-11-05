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
            campusTo = new double[] { CampusToLat, CampusToLong };
            buildingTitle = BuildingTitle;
            buildingImage = BuildingImage;
            maps = Maps;
        }
        public double[] campusTo { get; set; }
        public string buildingTitle { get; set; }
        public string buildingImage { get; set; }
        public string[][] maps { get; set; }
    }
}