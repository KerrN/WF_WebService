using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Services;
using MySql.Data.MySqlClient;
using System.Drawing;
using Microsoft;
using System.IO;
using System.Web;
using System.Web.Hosting;

namespace WF_webService
{
    public class WF_Service : WF_Service_Interface
    {
        DbManager db = new DbManager();

        // TESTING FUNCTIONS
        [WebMethod]
        // Testing Webservice Connection
        public bool checkServiceConn()
        {
            // Attempt to make a bool
            try
            {
                bool result = true;
                return result;
            }
            // Else fail connection test
            catch (Exception)
            {
                return false;
            }
        }

        [WebMethod]
        // Test Database Connection
        public bool checkDBConn()
        {
            return db.checkDBConn();
        }

        [WebMethod]
        public byte[] getImage(String ImageURL)
        {
            // returns an image from the file system as a byte array 
            // ImageURL = HostingEnvironment.ApplicationPhysicalPath + ImageURL;
            //ImageURL += ".png";
          
                System.IO.FileStream fs = System.IO.File.Open(ImageURL, FileMode.Open, FileAccess.Read);
                byte[] result = new byte[fs.Length];
                fs.Read(result, 0, (int)fs.Length);
                fs.Close();
                if(!ImageURL.Contains("Ignore"))
                    System.IO.File.Delete(ImageURL);
                return result;
           
       
        }

        [WebMethod]
        // Returns List of SOAP-Parsed Objects of Campuses with ID, Name, Lat, Long and Zoom data
        public int CampusVersion(string CampusID)
        {
            // Get all campuses from Database
            Campus campus = db.GetCampuses("Campus_ID = '" + CampusID + "'")[CampusID];

            // Return
            return campus.campusVersion;
        }

        [WebMethod]
        // Returns List of SOAP-Parsed Objects of Campuses with ID, Name, Lat, Long and Zoom data
        public string[][] SearchCampus()
        {
            // Get all campuses from Database
            SortedList<string, Campus> campuses = db.GetCampuses(null);
            // Initialise return object
            List<String[]> result = new List<string[]>();
            
            // For each campus found
            foreach(Campus c in campuses.Values)
            {
                // Add to return
                result.Add(new string[]{c.campusID, c.campusName, c.campusVersion.ToString(), c.campusLat.ToString(), c.campusLong.ToString(), c.campusZoom.ToString()});
            }

            // Return
            return result.ToArray();
        }

        [WebMethod]
        // Returns SortedList<int, string> of all WaypointIDs with a RoomName, and thier RoomName
        public string[][] SearchRooms(string CampusID)
        {
            if(CampusID == "")
            {
                CampusID = null;
            }
            // Get all Rooms from Database
            string[][] result = db.GetRooms(CampusID);
            // Return
            return result;
        }

        [WebMethod]
        // Returns SortedList<int, string> of all the flagged WaypointIDs and thier RoomName
        public SortedList<int, string> SearchServices(string CampusID)
        {
            if (CampusID == "")
            {
                CampusID = null;
            }
            // Get all Rooms from Database
            SortedList<int, string> result = db.GetServices(CampusID);
            // Return
            return result;
        }
        
        [WebMethod]
        // Returns SOAP-Parsed Object with all map-generation data
        public SOAP_ResolvePath ResolvePath(int WaypointID, bool Disability)
        {
            // Get objects containing source
            Waypoint source = db.GetWaypoints("Waypoint_ID = '" + WaypointID +"'")[WaypointID];
            Building sourceBuilding = db.GetBuildings("Building_ID = '" + source.buildingID + "'")[source.buildingID];
            Campus sourceCampus = db.GetCampuses("Campus_ID = '" + sourceBuilding.campusID + "'")[sourceBuilding.campusID];
            
            // Get all collection of objects to be searched once to avoid continious Database queries
            SortedList<int, Waypoint> searchable = db.GetWaypoints("Building_ID = '" + source.buildingID + "'");
            List<Floor> searchableFloors = db.GetFloors("Building_ID = '" + sourceBuilding.buildingID + "'");

            // Initialise loop variables
            List<Waypoint> sequence = new List<Waypoint>();
            Waypoint current = source;

            // While current node to be processed exists
            while (current != null)
            {
                // Add the current node to the sequence
                sequence.Add(current);

                // If Disabled, and the current nodes disabled-previous node is not -1 (aka an int null)
                if (Disability && (current.waypointIDPrevDis != -1))
                {
                    // Set the diabled-previous node to current, by searching for node in SortedList with int key
                    current = searchable[current.waypointIDPrevDis];
                }
                // If Not Disabled, and the current nodes previous node is not -1 (aka an int null)
                else if (!Disability && (current.waypointIDPrev != -1))
                {
                    // Set the previous node to current, by searching for node in SortedList with int key
                    current = searchable[current.waypointIDPrev];
                }
                // Else if a previous node cant be found, then set current to null, to escape as the loop is finished
                else
                {
                    current = null;
                }
            }
            
            // Reverse the sequence to start at enterance
            sequence.Reverse();
            
            // Divide the sequence into thier floors
            // Initialise the loop variables
            List<List<Waypoint>> floorSequence = new List<List<Waypoint>>();
            List<Waypoint> temp = new List<Waypoint>();
            int currentFloor = sequence.First().floorID;

            // Foreach waypoint in the sequence
            foreach (Waypoint w in sequence)
            {
                // If the currents node floor is the same as the test-floor
                if (w.floorID == currentFloor)
                {
                    // Add the node to the current floors List<Waypoint>
                    temp.Add(w);
                }
                // Otherwise the new node is on a new floor
                else
                {
                    // Add the current floor-list to the list of floors
                    floorSequence.Add(temp);
                    // Create a new floor List<Waypoint>
                    temp = new List<Waypoint>();
                    // Change the test floor ID
                    currentFloor = w.floorID;
                }
            }
            // At the end, if the temp List<Waypoint> is not empty there is one last floor to add
            if (temp.Count != 0)
            {
                // Add the last floor
                floorSequence.Add(temp);
            }
            
            // Generate responses, and prepare images
            // Initialise response variables
            string[][] mapData = new string[floorSequence.Count][];
            int i = 0;
            
            //** JACK, THIS SECTION REFERS TO YOU **//
            // For each floors List<Waypoint>
            foreach(List<Waypoint> floorList in floorSequence)
            {
                // Loop to find floor
                // NOTE: this is CPU inefficient to reduce DB queries to find floor
                Floor tempFloor = null;
                foreach(Floor f in searchableFloors)
                {
                    if(f.floorID == floorList.First().floorID)
                    {
                        tempFloor = f;
                    }
                }
                
                // Generate the floor-map name
                string buildingTitle = sourceBuilding.buildingName;
                string floorTitle = "Floor " + tempFloor.floorID.ToString();
                string mapTitle = buildingTitle + ", " + floorTitle;

                // returns the final image
                //string imagePath = "";
                string imagePath = new ImageProccessor().GenerateImage(tempFloor.floorMap, tempFloor.floorColorMap, source.roomName, sequence, 0, 0, 1);

                // Add return data
                mapData[i] = new string[] { mapTitle, imagePath };
                // Increment index
                i++;
                
            }
            
            // Create return object
            SOAP_ResolvePath result = new SOAP_ResolvePath(sourceBuilding.buildingLat, sourceBuilding.buildingLong, sourceBuilding.buildingAddress, sourceBuilding.buildingPath, mapData);

            //Return
            return result;
        }



        [WebMethod]
        // Returns SOAP-Parsed Object with all map-generation data
        // ** indicates sections in progress
        public List<List<Waypoint>> GetResolvePathLists(int WaypointID, bool Disability)
        {
            Waypoint source = db.GetWaypoints("Waypoint_ID = '" + WaypointID + "'")[WaypointID];
            Building sourceBuilding = db.GetBuildings("Building_ID = '" + source.buildingID + "'")[source.buildingID];
            Campus sourceCampus = db.GetCampuses("Campus_ID = '" + sourceBuilding.campusID + "'")[sourceBuilding.campusID];
            SortedList<int, Waypoint> searchable = db.GetWaypoints("Building_ID = '" + source.buildingID + "'");
            List<Floor> searchableFloors = db.GetFloors("Building_ID = '" + sourceBuilding.buildingID + "'");
            List<Waypoint> sequence = new List<Waypoint>();
            Waypoint current = source;
            while (current != null)
            {
                sequence.Add(current);
                if (Disability && (current.waypointIDPrevDis != -1))
                {
                    current = searchable[current.waypointIDPrevDis];
                }
                else if (!Disability && (current.waypointIDPrev != -1))
                {
                    current = searchable[current.waypointIDPrev];
                }
                else
                {
                    current = null;
                }
            }
            sequence.Reverse();
            List<List<Waypoint>> floorSequence = new List<List<Waypoint>>();
            List<Waypoint> temp = new List<Waypoint>();
            int currentFloor = sequence.First().floorID;
            foreach (Waypoint w in sequence)
            {
                if (w.floorID == currentFloor)
                {
                    temp.Add(w);
                }
                else
                {
                    floorSequence.Add(temp);
                    temp = new List<Waypoint>();
                    currentFloor = w.floorID;
                }
            }
            if (temp.Count != 0)
            {
                floorSequence.Add(temp);
            }
            return floorSequence;
        }

        // TEST METHOD FOR JACK
        [WebMethod]
        public string RunImageProcessor(string roomName, List<Waypoint> sequence,int xanchor, int yanchor, double scale)
        {

            try
            {

                Waypoint source = sequence.First();
                Floor temp = db.GetFloors("Building_ID = '" + source.buildingID + "' AND Floor_ID = '" + source.floorID + "'").First();
                return new ImageProccessor().GenerateImage(temp.floorMap, temp.floorColorMap, roomName, sequence, xanchor, yanchor, scale);
            }catch(Exception e)
            {
                return e.StackTrace;
            }
        }

        // Content Manager Update Script
        // Currently not a WebMethod as incomplete
        public void UpdateMaps()
        {
            //GET AND PROCESS DATA INTO WaypointData dataSource AND SortedList<int, WaypointData> data

            // FAKE DATA - FAILS DUE TO EMPTY
            WaypointData dataSource = new WaypointData();
            SortedList<int, WaypointData> data = new SortedList<int, WaypointData>();

            // Initialise test variables
            SortedList<int, WaypointData> dataEdges = new SortedList<int, WaypointData>();
            SortedList<int, WaypointData> dataEdgesDis = new SortedList<int, WaypointData>();

            // Set min path dist to 0 for source node
            dataSource.minDist = 0f;
            // Add source node to process que
            dataEdges.Add(dataSource.waypointID, dataSource);

            // Until the path que is empty
            while (dataEdges.Count != 0)
            {
                // Get the waypointID of the current node
                int key = dataEdges.First().Key;
                // Then get the current node
                WaypointData current = dataEdges[key];

                // For every node that current is linked to
                foreach (int i in current.edgeKeys)
                {
                    // Find the edge node
                    WaypointData edge = data[i];
                    // Get dist using Tan(adj/opp)
                    double distX = current.coordX - edge.coordX;
                    double distY = current.coordY - edge.coordY;
                    double dist = Math.Tan(distX / distY);
                    // Get total distance from source to edge node
                    double edgeDist = current.minDist + dist;

                    // If the distance along this path is shorter than the current path for the edge node
                    if (edgeDist < edge.minDist)
                    {
                        // Remove the edge node from the que
                        dataEdges.Remove(i);
                        // Update the local-stored edge node
                        edge.minDist = edgeDist;
                        edge.waypointIDPrev = key;
                        // Update global node
                        data[i] = edge;
                        // Reinsert edge node to que with updated values
                        dataEdges.Add(i, edge);
                    }
                }

                // When all processing for the current node is done remove from the processing que
                dataEdges.Remove(key);
            }

            // Set min disabled path dist to 0 for source node
            dataSource.minDistDis = 0f;
            // Add source node to disabled process que
            dataEdgesDis.Add(dataSource.waypointID, dataSource);

            // Until the disabled path que is empty
            while (dataEdgesDis.Count != 0)
            {
                // Get the waypointID of the current node
                int key = dataEdgesDis.First().Key;
                // Then get the current node
                WaypointData current = dataEdgesDis[key];

                // For every node that current is linked to
                foreach (int i in current.edgeKeys)
                {
                    // Find the edge node
                    WaypointData edge = data[i];
                    // Get dist using Tan(adj/opp)
                    double distX = current.coordX - edge.coordX;
                    double distY = current.coordY - edge.coordY;
                    double dist = Math.Tan(distX / distY);
                    // Get total distance from source to edge node
                    double edgeDist = current.minDistDis + dist;

                    // If the distance along this path is shorter than the current path for the edge node
                    if (edgeDist < edge.minDistDis)
                    {
                        // Remove the edge node from the que
                        dataEdgesDis.Remove(i);
                        // Update the local-stored edge node
                        edge.minDist = edgeDist;
                        edge.waypointIDPrevDis = key;
                        // Update global node
                        data[i] = edge;
                        // Reinsert edge node to que with updated values
                        dataEdgesDis.Add(i, edge);
                    }
                }

                // When all processing for the current node is done remove from the processing que
                dataEdgesDis.Remove(key);
            }

            // Transform all the WaypointData objects to regular Waypoints
            List<Waypoint> result = new List<Waypoint>();
            foreach (WaypointData wd in data.Values)
            {
                result.Add(wd.ToWaypoint());
            }

            //UPDATE DATABASE
        }
    }
}