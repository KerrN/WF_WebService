using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Services;
using MySql.Data.MySqlClient;


namespace WF_webService
{
    public class DbManager
    {
        string connStr = "Server=localhost;Database=cf_Wayfinding; Uid=CodeFactory; Pwd=CFPassword.2015;";

        // TESTING METHODS
        // Test Database Connection
        public bool checkDBConn()
        {
            try
            {
                // Attempt to open connection
                using (MySqlConnection cnn = new MySqlConnection(connStr))
                {
                    cnn.Open();
                }
                // If exception not thrown, return true
                return true;
            }
            // If exception gets thrown, return false
            catch (Exception)
            {
                return false;
            }
        }

        // DATABASE METHODS
        // Returns SortedList<string CampusID, Campus> of Campus Objects
        // Allows WHERE clause as input, use 'null' for no where clause
        public SortedList<string, Campus> GetCampuses(string Where)
        {
            // Initialise return objects
            SortedList<string, Campus> result = new SortedList<string, Campus>();

            // Generate SQL statement
            string sql = "SELECT * FROM campuses";
            // If Where argument is not empty
            if (Where != null)
            {
                // Add where syntax to SQL
                sql += " WHERE " + Where;
            }

            // Create try-catch to open connection
            try
            {
                using (MySqlConnection dbConn = new MySqlConnection(connStr))
                {
                    // Initialise
                    MySqlCommand campusSQL = new MySqlCommand(sql, dbConn);
                    MySqlDataReader dbReader;

                    // Open connection
                    dbConn.Open();
                    // Excecute Query
                    dbReader = campusSQL.ExecuteReader();

                    // While the reader has a next-row
                    while (dbReader.Read())
                    {
                        // Get all values as temp values
                        string campusID = (string)dbReader["Campus_ID"];
                        string campusName = (string)dbReader["Campus_Name"];
                        int campusVersion = (int)dbReader["Campus_Version"];
                        double campusLat = (double)dbReader["Campus_Lat"];
                        double campusLong = (double)dbReader["Campus_Long"];
                        float campusZoom = (float)dbReader["Campus_Zoom"];
                        // Then add these into a new object to be added to the return
                        result.Add(campusID, new Campus(campusID, campusName, campusVersion, campusLong, campusLat, campusZoom));
                    }
                }
                // Return
                return result;
            }
            // If connection fails
            catch (Exception ex)
            {
                // Throw exception
                throw ex;
            }
        }

        // Returns SortedList<int BuildingID, Building> of Building Objects
        // Allows WHERE clause as input, use 'null' for no where clause
        public SortedList<int, Building> GetBuildings(string Where)
        {
            // Initialise return objects
            SortedList<int, Building> result = new SortedList<int, Building>();

            // Generate SQL statement
            string sql = "SELECT * FROM Buildings";
            // If Where argument is not empty
            if (Where != null)
            {
                // Add where syntax to SQL
                sql += " WHERE " + Where;
            }

            // Create try-catch to open connection
            try
            {
                using (MySqlConnection dbConn = new MySqlConnection(connStr))
                {
                    // Initialise
                    MySqlCommand campusSQL = new MySqlCommand(sql, dbConn);
                    MySqlDataReader dbReader;

                    // Open connection
                    dbConn.Open();
                    // Excecute Query
                    dbReader = campusSQL.ExecuteReader();

                    // While the reader has a next-row
                    while (dbReader.Read())
                    {
                        // Get all values as temp values
                        int buildingID = (int)dbReader["Building_ID"];
                        string campusID = (string)dbReader["Campus_ID"];
                        string buildingName = (string)dbReader["Building_Name"];
                        string buildingPath = (string)dbReader["Building_Path"];
                        string buildingAddress = (string)dbReader["Building_Address"];
                        double buildingLong = (double)dbReader["Building_Long"];
                        double buildingLat = (double)dbReader["Building_Lat"];
                        // Then add these into a new object to be added to the return
                        result.Add(buildingID, new Building(buildingID, campusID, buildingName, buildingAddress, buildingPath, buildingLong, buildingLat));
                    }
                }
                // Return
                return result;
            }
            // If connection fails
            catch (Exception ex)
            {
                // Throw exception
                throw ex;
            }
        }

        // Returns List<Floor> of Floor Objects
        // - This does not use SortedList<> as there is a dual key
        // Allows WHERE clause as input, use 'null' for no where clause
        public List<Floor> GetFloors(string Where)
        {
            // Initialise return objects
            List<Floor> result = new List<Floor>();

            // Generate SQL statement
            string sql = "SELECT * FROM Floors";
            // If Where argument is not empty
            if (Where != null)
            {
                // Add where syntax to SQL
                sql += " WHERE " + Where;
            }

            // Create try-catch to open connection
            try
            {
                using (MySqlConnection dbConn = new MySqlConnection(connStr))
                {
                    // Initialise
                    MySqlCommand campusSQL = new MySqlCommand(sql, dbConn);
                    MySqlDataReader dbReader;

                    // Open connection
                    dbConn.Open();
                    // Excecute Query
                    dbReader = campusSQL.ExecuteReader();

                    // While the reader has a next-row
                    while (dbReader.Read())
                    {
                        // Get all values as temp values
                        int floorID = (int)dbReader["Floor_ID"];
                        int buildingID = (int)dbReader["Building_ID"];
                        string floorMap = (string)dbReader["Floor_Map"];
                        string floorColorMap = (string)dbReader["Floor_Color_Map"];
                        // Then add these into a new object to be added to the return
                        result.Add(new Floor(floorID, buildingID, floorMap, floorColorMap));
                    }
                }
                // Return
                return result;
            }
            // If connection fails
            catch (Exception ex)
            {
                // Throw exception
                throw ex;
            }
        }

        // Returns SortedList<int WaypointID, Waypoint> of Waypoint Objects
        // Allows WHERE clause as input, use 'null' for no where clause
        public SortedList<int, Waypoint> GetWaypoints(string Where)
        {
            // Initialise return objects
            SortedList<int, Waypoint> result = new SortedList<int, Waypoint>();

            // Generate SQL statement
            string sql = "SELECT * FROM Waypoints";
            // If Where argument is not empty
            if (Where != null)
            {
                // Add where syntax to SQL
                sql += " WHERE " + Where;
            }

            // Create try-catch to open connection
            try
            {
                using (MySqlConnection dbConn = new MySqlConnection(connStr))
                {
                    // Initialise
                    MySqlCommand campusSQL = new MySqlCommand(sql, dbConn);
                    MySqlDataReader dbReader;

                    // Open connection
                    dbConn.Open();
                    // Excecute Query
                    dbReader = campusSQL.ExecuteReader();

                    // While the reader has a next-row
                    while (dbReader.Read())
                    {
                        // Get all values as temp values
                        int waypointID = (int)dbReader["Waypoint_ID"];
                        int buildingID = (int)dbReader["Building_ID"];
                        int floorID = (int)dbReader["Floor_ID"];

                        // Initialise with null value
                        int waypointIDPrev = -1;
                        // If column != null
                        if (!dbReader.IsDBNull(3))
                        {
                            // Replace null value
                            waypointIDPrev = (int)dbReader["Waypoint_ID_Prev"];
                        }

                        // Initialise with null value
                        int waypointIDPrevDis = -1;
                        // If column != null
                        if (!dbReader.IsDBNull(4))
                        {
                            waypointIDPrevDis = (int)dbReader["Waypoint_ID_Prev_Dis"];
                        }

                        double coordX = (double)dbReader["Coord_X"];
                        double coordY = (double)dbReader["Coord_Y"];

                        // Initialise with null value
                        string roomName = "";
                        // If column != null
                        if (!dbReader.IsDBNull(7))
                        {
                            // Replace null value
                            roomName = (string)dbReader["Room_Name"];
                        }

                        // Initialise with null value
                        string transitionMode = "";
                        // If column != null
                        if (!dbReader.IsDBNull(8))
                        {
                            // Replace null value
                            transitionMode = (string)dbReader["Transition_Mode"];
                        }
                        // Then add these into a new object to be added to the return
                        result.Add(waypointID, new Waypoint(waypointID, buildingID, floorID, waypointIDPrev, waypointIDPrevDis, coordX, coordY, roomName, transitionMode));
                    }
                }
                // Return
                return result;
            }
            // If connection fails
            catch (Exception ex)
            {
                // Throw exception
                throw ex;
            }
        }

        // Returns SortedList<int WaypointID, string RoomName> from Waypoints
        // Requires WHERE Campus clause as input
        public SortedList<int, string> GetRooms(string CampusID)
        {
            // Initialise return objects
            SortedList<int, string> rooms = new SortedList<int, string>();

            // Generate SQL statement, This is not the same SQL as normal due to the nature of the specific function
            string sql = "SELECT DISTINCT Waypoint_ID, Room_Name FROM waypoints JOIN buildings ON waypoints.Building_ID = buildings.Building_ID WHERE waypoints.Room_name IS NOT NULL";
            if (CampusID != null)
            {
                sql += " AND buildings.Campus_ID = '" + CampusID + "'";
            }

            // Create try-catch to open connection
            try
            {
                using (MySqlConnection dbConn = new MySqlConnection(connStr))
                {
                    // Initialise
                    MySqlCommand roomSQL = new MySqlCommand(sql, dbConn);
                    MySqlDataReader dbReader;

                    // Open connection
                    dbConn.Open();
                    // Excecute Query
                    dbReader = roomSQL.ExecuteReader();

                    // While the reader has a next-row
                    while (dbReader.Read())
                    {
                        // Get all values as temp values
                        int waypointID = (int)dbReader["Waypoint_ID"];
                        string campusID = (string)dbReader["Room_Name"];
                        // Then add these into a new object to be added to the return
                        rooms.Add(waypointID, campusID);
                    }

                    // Return
                    return rooms;
                }
            }
            // If connection fails
            catch (Exception ex)
            {
                // Throw exception
                throw ex;
            }
        }

        // Returns SortedList<int WaypointID, string RoomName> from Waypoints
        // Requires WHERE Campus clause as input
        public SortedList<int, string> GetServices(string CampusID)
        {
            // Initialise return objects
            SortedList<int, string> rooms = new SortedList<int, string>();

            // Generate SQL statement, This is not the same SQL as normal due to the nature of the specific function
            string sql = "SELECT DISTINCT waypoints.Waypoint_ID, waypoints.Room_Name FROM waypoints JOIN campus_locations ON waypoints.Waypoint_ID = campus_locations.Waypoint_ID";
            if (CampusID != null)
            {
                sql += " WHERE campus_locations.Campus_ID = '" + CampusID + "'";
            }

            // Create try-catch to open connection
            try
            {
                using (MySqlConnection dbConn = new MySqlConnection(connStr))
                {
                    // Initialise
                    MySqlCommand roomSQL = new MySqlCommand(sql, dbConn);
                    MySqlDataReader dbReader;

                    // Open connection
                    dbConn.Open();
                    // Excecute Query
                    dbReader = roomSQL.ExecuteReader();

                    // While the reader has a next-row
                    while (dbReader.Read())
                    {
                        // Get all values as temp values
                        int waypointID = (int)dbReader["Waypoint_ID"];
                        string roomName = (string)dbReader["Room_Name"];
                        // Then add these into a new object to be added to the return
                        rooms.Add(waypointID, roomName);
                    }

                    // Return
                    return rooms;
                }
            }
            // If connection fails
            catch (Exception ex)
            {
                // Throw exception
                throw ex;
            }
        }
    }

    // Serialized Object Classes
    [Serializable]
    public class Campus
    {
        public Campus(string CampusID, string CampusName, int CampusVersion, double CampusLong, double CampusLat, float CampusZoom)
        {
            campusID = CampusID;
            campusName = CampusName;
            campusVersion = CampusVersion;
            campusLong = CampusLong;
            campusLat = CampusLat;
            campusZoom = CampusZoom;
        }
        public string campusID { get; set; }
        public string campusName { get; set; }
        public int campusVersion { get; set; }
        public double campusLong { get; set; }
        public double campusLat { get; set; }
        public float campusZoom { get; set; }
    }

    [Serializable]
    public class Building
    {
        public Building(int BuildingID, string CampusID, string BuildingName, string BuildingAddress, string BuildingPath, double BuildingLong, double BuildingLat)
        {
            buildingID = BuildingID;
            campusID = CampusID;
            buildingName = BuildingName;
            buildingAddress = BuildingAddress;
            buildingPath = BuildingPath;
            buildingLong = BuildingLong;
            buildingLat = BuildingLat;
        }
        public int buildingID { get; set; }
        public string campusID { get; set; }
        public string buildingName { get; set; }
        public string buildingAddress { get; set; }
        public string buildingPath { get; set; }
        public double buildingLong { get; set; }
        public double buildingLat { get; set; }
    }

    [Serializable]
    public class Floor
    {
        public Floor(int FloorID, int BuildingID, string FloorMap, string FloorColorMap)
        {
            floorID = FloorID;
            buildingID = BuildingID;
            floorMap = FloorMap;
            floorColorMap = FloorColorMap;
        }
        public int floorID { get; set; }
        public int buildingID { get; set; }
        public string floorMap { get; set; }
        public string floorColorMap { get; set; }
    }

    [Serializable]
    public class Waypoint
    {
        public Waypoint(int WaypointID, int BuildingID, int FloorID, int WaypointIDPrev, int WaypointIDPrevDis, double CoordX, double CoordY, string RoomName, string TransitionMode)
        {
            waypointID = WaypointID;
            buildingID = BuildingID;
            floorID = FloorID;
            waypointIDPrev = WaypointIDPrev;
            waypointIDPrevDis = WaypointIDPrevDis;
            coordX = CoordX;
            coordY = CoordY;
            roomName = RoomName;
            transitionMode = TransitionMode;
        }
        public int waypointID { get; set; }
        public int buildingID { get; set; }
        public int floorID { get; set; }
        public int waypointIDPrev { get; set; }
        public int waypointIDPrevDis { get; set; }
        public double coordX { get; set; }
        public double coordY { get; set; }
        public string roomName { get; set; }
        public string transitionMode { get; set; }
    }

    // Waypoint Data class is only used in the UpdateMaps() Method as it needs to store extra data for processing
    [Serializable]
    public class WaypointData
    {
        public WaypointData()
        { }
        public WaypointData(int WaypointID, int BuildingID, int FloorID, int WaypointIDPrev, int WaypointIDPrevDis, double CoordX, double CoordY, string RoomName, string TransitionMode)
        {
            waypointID = WaypointID;
            buildingID = BuildingID;
            floorID = FloorID;
            waypointIDPrev = WaypointIDPrev;
            waypointIDPrevDis = WaypointIDPrevDis;
            coordX = CoordX;
            coordY = CoordY;
            roomName = RoomName;
            transitionMode = TransitionMode;
        }
        public int waypointID { get; set; }
        public int buildingID { get; set; }
        public int floorID { get; set; }
        public int waypointIDPrev { get; set; }
        public int waypointIDPrevDis { get; set; }
        public double coordX { get; set; }
        public double coordY { get; set; }
        public string roomName { get; set; }
        public string transitionMode { get; set; }

        public double minDist = 0;
        public double minDistDis = 0;
        public List<int> edgeKeys = new List<int>();

        public Waypoint ToWaypoint()
        {
            return new Waypoint(waypointID, buildingID, floorID, waypointIDPrev, waypointIDPrevDis, coordX, coordY, roomName, transitionMode);
        }
    }
}