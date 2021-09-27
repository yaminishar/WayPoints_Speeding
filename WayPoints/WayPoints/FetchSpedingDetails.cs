using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Device.Location;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WayPoints
{
    public partial class FetchSpedingDetails : Form
    {
        
        List<WayPoints> wayPoints = new List<WayPoints>();
        double speedingDistance = 0.0; //in meters
        double speedingTime = 0.0; //in seconds
        double totalTime = 0.0;
        double totalDistance = 0.0;
        public FetchSpedingDetails()
        {
            InitializeComponent();
        }

        private void FetchSpedingDetails_Load(object sender, EventArgs e)
        {
            try
            {
                lblError.Visible = false;
                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                using (StreamReader r = new StreamReader(Path.GetFullPath(Path.Combine(path,@"..\..\waypoints.json"))))
                {
                    string json = r.ReadToEnd();
                    wayPoints = JsonConvert.DeserializeObject<List<WayPoints>>(json);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = wayPoints;
                    dataGridView1.Refresh();
                }
                CalculateSpeedingTimeandDistance();
                CalculateTotalTimeandDistance();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while loading the form:" + ex.Message);
                lblError.Text = "Error while loading the form:" + ex.Message;
                lblError.Visible = true;
            }
        }

        private void CalculateTotalTimeandDistance()
        {
            try
            {
                for (int i = 0; i < wayPoints.Count() - 1; i++)
                {
                    var sCoord = new GeoCoordinate(wayPoints[i].Position.Latitude, wayPoints[i].Position.Longitude);
                    var eCoord = new GeoCoordinate(wayPoints[i + 1].Position.Latitude, wayPoints[i + 1].Position.Longitude);
                    totalDistance = totalDistance + sCoord.GetDistanceTo(eCoord);
                    totalTime = totalTime + (wayPoints[i + 1].Timestamp.Subtract(wayPoints[i].Timestamp).TotalSeconds);
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error while calculate Total Time and Distance:" + ex.Message;
                lblError.Visible = true;
                MessageBox.Show("Error while calculate Total Time and Distance:" + ex.Message);
            }
        }

        private void CalculateSpeedingTimeandDistance()
        {
            try
            {
                for (int i = 0; i < wayPoints.Count(); i++)
                {
                    if (wayPoints[i].Speed > wayPoints[i].SpeedLimit)
                    {
                        var sCoord = new GeoCoordinate(wayPoints[i - 1].Position.Latitude, wayPoints[i - 1].Position.Longitude);
                        var eCoord = new GeoCoordinate(wayPoints[i].Position.Latitude, wayPoints[i].Position.Longitude);
                        speedingDistance = speedingDistance + sCoord.GetDistanceTo(eCoord);
                        speedingTime = speedingTime + (wayPoints[i].Timestamp.Subtract(wayPoints[i - 1].Timestamp).TotalSeconds);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while calculate Speeding Time and Distance:" + ex.Message);
                lblError.Text = "Error while calculate Speeding Time and Distance:" + ex.Message;
                lblError.Visible = true;
            }
        }
        private void btnDistanceSpeeding_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Visible = false;
                lblDistanceSpeeding.Text = Math.Round(speedingDistance,2).ToString() + " meters";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while clicking DIstance Speeding button:" + ex.Message);
                lblError.Text = "Error while calculate DIstance Speeding button:" + ex.Message;
                lblError.Visible = true;
            }
        }
        private void btnDurationSpeeding_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Visible = false;
                lblDurationSpeeding.Text = Math.Round(speedingTime,2).ToString() + " seconds";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while clicking Duration Speeding button:" + ex.Message);
                lblError.Text = "Error while clicking Duration Speeding button:" + ex.Message;
                lblError.Visible = true;
            }
        }
        private void btnTotalDistance_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Visible = false;
                
                lblTotalDistance.Text = Math.Round(totalDistance,2).ToString() + " meters";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while clicking total distance button:" + ex.Message);
                lblError.Text = "Error while clicking total distance button:" + ex.Message;
                lblError.Visible = true;
            }
        }

        private void btnTotalDuration_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Visible = false;
                lblTotalDuration.Text = Math.Round(totalTime,2).ToString() + " seconds";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while clicking total Duration button:" + ex.Message);
                lblError.Text = "Error while clicking total Duration button:" + ex.Message;
                lblError.Visible = true;
            }
        }

        
    }
}
