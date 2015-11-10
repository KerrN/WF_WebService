using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using webServiceTestForm.wf_service;

namespace webServiceTestForm
{
    public partial class Form1 : Form
    {
        Waypoint[][] testImgWaypoints;

        public Form1()
        {
            InitializeComponent();
        }
        int selectedImage = 0;

        private void Form1_Load(object sender, EventArgs e)
        {}


        private void btnTestService_Click(object sender, EventArgs e)
        {
            txtOutput.Clear();
            WF_Service_InterfaceClient service = new WF_Service_InterfaceClient();
            try
            {
                if (service.checkServiceConn())
                {
                    txtOutput.Text = "Web Service Connected!";
                }
                else
                {
                    txtOutput.Text = "WS Connection error!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnTestDB_Click(object sender, EventArgs e)
        {
            txtOutput.Clear();
            WF_Service_InterfaceClient service = new WF_Service_InterfaceClient();
            if (service.checkDBConn())
            {
                txtOutput.Text = "Database Connected!";
            }
            else
            {
                txtOutput.Text = "DB Connection error!";
            }
        }

        private void btnGetCampuses_Click(object sender, EventArgs e)
        {
            txtOutput.Clear();
            WF_Service_InterfaceClient service = new WF_Service_InterfaceClient();
            string[][] result = service.SearchCampus();
            string text = "";
            foreach(string[] c in result)
            {
                text += "CampusID: " + c[0] + ", CampusName: " + c[1] + ", CampusVersion: " + c[2] + Environment.NewLine;
                text += "CampusLat-Long: " + c[3] + ", " + c[4] + ", CampusZoom: " + c[5] + Environment.NewLine;
                text += Environment.NewLine;
            }
            txtOutput.Text = text;
        }

        private void btnGetRooms_Click(object sender, EventArgs e)
        {
            txtOutput.Clear();
            WF_Service_InterfaceClient service = new WF_Service_InterfaceClient();

            string campus = txtRoomCampus.Text;
            string[][] result = service.SearchRooms(campus);

            string text = "";
            foreach (string[] r in result)
            {
                text += "WaypointID: " + r[0] + ", RoomName: " + r[1] + " , BuildingID: "+r[2] + " , image: "+r[3];
                text += Environment.NewLine;
            }
            txtOutput.Text = text;
        }
        List<Image> images = new List<Image>();
        private void btnResolvePath_Click(object sender, EventArgs e)
        {
            txtOutput.Clear();
            WF_Service_InterfaceClient service = new WF_Service_InterfaceClient();

            //DISABILITY CURRENTLY DISABLED DUE TO LACK OF DATA
            //bool dis = rbPath_Dis.Checked;
            bool dis = false;
            string text = "";
            try
            {
                int waypoint = Int32.Parse(txtPath_Waypoint.Text);
                SOAP_ResolvePath result = service.ResolvePath(waypoint, dis);
                testImgWaypoints = service.GetResolvePathLists(waypoint, dis);
                text += "CampusTo: " + result.campusTok__BackingField[0] + ", " + result.campusTok__BackingField[1] + Environment.NewLine;
                text += "Maps: " + Environment.NewLine;
                foreach (string[] m in result.mapsk__BackingField)
                {
                    text += "Name: " + m[0] + ", Path: " + m[1] + Environment.NewLine;
                    images.Add(imageFromStream(service.getImage(m[1])));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            txtOutput.Text = text;

            setImage(selectedImage);
        }

        private void txtRoom_Campus_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            txtOutput.Clear();
            WF_Service_InterfaceClient service = new WF_Service_InterfaceClient();
            int index = Int32.Parse(TestImgIndex.Text);
            
            int len = 0;
            
            //Quick double check there isnt nulls at the end
            for(int i = 0; i < testImgWaypoints[index].Length; i++)
            {
                if(testImgWaypoints[index][i] != null)
                {
                    len++;
                }
            }
              
            Waypoint[] temp = new Waypoint[len];
            
            for(int j = 0; j < temp.Length; j++)
            {
                temp[j] = testImgWaypoints[index][j];
            }
            
            string text = service.RunImageProcessor(RoomName.Text, temp,0,17,1);
            txtOutput.Text = text;
            dbImage.BackgroundImage = imageFromStream(service.getImage(text));
        }

        private Image imageFromStream(byte[] byteArrayIn)
        {
            using (var ms = new MemoryStream(byteArrayIn))
            {
                return Image.FromStream(ms);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void setImage(int i)
        {
            dbImage.BackgroundImage = images[i];
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void Left_Click(object sender, EventArgs e)
        {

        }

        private void Left_MouseDown(object sender, MouseEventArgs e)
        {
            if (selectedImage > 0)
                selectedImage--;
            else
                selectedImage = images.Count - 1;

            setImage(selectedImage);
        }

        private void right_MouseDown(object sender, MouseEventArgs e)
        {
            if (selectedImage < images.Count)
                selectedImage++;
            else
                selectedImage = 0;

            setImage(selectedImage);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            txtOutput.Clear();
            WF_Service_InterfaceClient service = new WF_Service_InterfaceClient();
            int result = service.CampusVersion(txtCampusVer.Text);
            string text = "Campus: " + txtCampusVer.Text + ", Version: " + result;
            txtOutput.Text = text;
        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtOutput.Clear();
            WF_Service_InterfaceClient service = new WF_Service_InterfaceClient();

            string campus = txtGetService.Text;
            SortedList<int, string> result = service.SearchServices(campus);

            string text = "";
            foreach (int k in result.Keys)
            {
                text += "WaypointID: " + k + ", RoomName: " + result[k];
                text += Environment.NewLine;
            }
            txtOutput.Text = text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
