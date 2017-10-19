using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controller.ControllerServiceReference;
using System.ServiceModel;
using System.Net.Sockets;
using System.Threading;
using System.Reflection;
using System.IO;

namespace Controller
{

    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    //[CallbackBehaviorAttribute(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class Form1 : Form, IControllerCallback
    {
        private Task<Airplane[]> TasklistOfPlanes;
        private List<Airplane> listOfPlanes; //List of planes

        private ControllerClient proxy;
        private bool Switch; // To see if the server is BACK online to reconnect the plane

        // To cancel threads   -Jose
        private CancellationTokenSource CancelTokenSource; 
        private CancellationToken Token;

        //instance used for callbacks
        private InstanceContext context;
        // starting locations for the planes (animation) - Marta
        Point startR1;
        Point startR2;
        Point startR3;
        Point startR4;
        
        private Point emergencyLocation;
       
        //variables which store what kind of action is taken for the specific runway with the given flight; take off, landing...
        //check backgroundworkercompleted
        string[] flightstatus;
        

        Dictionary<AStatus, string> statusDict;
        Dictionary<Command, string> commandDict;

        Thread tForR1, tforR2, tForR3, tforR4;

        int emergencyTicks;
        PictureBox emergencyPlane;

        public Form1()
        {
            InitializeComponent();
            emergencyTicks = 0;
            emergencyPlane = null;
            this.flightstatus = new string[3];
            for (int i = 0; i < flightstatus.Length; i++) flightstatus[i] = "";
            this.context = new InstanceContext(this);
            this.proxy = new ControllerClient(context);
            Switch = false;
            this.timer1.Interval = 2000;
            this.timer2.Interval = 1000;
            this.proxy.SubscribeEvent();
            this.lbAirportName.Text = proxy.GetAirportName();


            CancelTokenSource = new CancellationTokenSource();
            Token = CancelTokenSource.Token;
           
            listOfPlanes = this.proxy.GetAirplaneList().ToList();
           

            timer1.Start();
            timer2.Start();
            //set starting points of planes for animation    
            startR1 = new Point(50, 50);
            startR2 = new Point(50, 100);
            startR3 = new Point(50, 190);
            startR4 = new Point(50, 315);
            // hide all planes and labels
            hidePlanes();
            lblR1.Hide(); lblR2.Hide(); lblR3.Hide(); lblR4.Hide();
            //only one item can be selected from the listView (for command pusposes), it returns an array of selected items so the one we look for is on index 0
            listView_flight.MultiSelect = false;
            //all runways are free 
            prepareRunway("R1");
            prepareRunway("R2");
            prepareRunway("R3");

            statusDict = proxy.GetStatusDictionaryController();
            commandDict = proxy.GetCommandDictionary();

            
        }

        //
        //serie of messaging events handlers!
        //
        ///Structure:
        ///Add to list message
        ///     Helper to check if in the list
        ///     Helper to add to list
        ///Abort message
        ///     Helper for land abort
        ///     Helper for air abort
        ///Cancel message
        ///Docked message
        ///Standard case update

        //ADD TO LISTVIEW
        public void AddToListMessage(string flightNumber)
        {
            Airplane newPlane = this.proxy.GetAirplane(flightNumber);
            //it might be the case that item is already in the listView and server doesnt know
            if (!HelperAddCheck(flightNumber)) this.AddToListView(newPlane);
            else  UpdateListView(flightNumber, newPlane.Status);           
        }

        //checks if the flight is in the listview
        public bool HelperAddCheck(string flight)
        {
            foreach(ListViewItem l in listView_flight.Items)
            {
                if (l.SubItems[0].Text == flight) return true; //it is in the listview
            }
            return false; //it is not in the listview
        }

        //adds to listview
        public void AddToListView(Airplane newPlane)
        {

            //standard stuff           
            string[] st = new string[2];
            st[0] = newPlane.FuelLevel + "%";
            st[1] = statusDict[newPlane.Status];

            //Create ListView item
            ListViewItem item = new ListViewItem(newPlane.FlightNumber);
            item.SubItems.AddRange(st);// string array to listView added as subitems  
            item.BackColor = Color.Yellow; //change color to yellow
            item.Tag = newPlane;

            this.listView_flight.Items.Add(item);

            //undocking request from pilot undock method, dock process should be started 
            //and the plane becomes docked if no action is taken
            if (newPlane.Status == AStatus.On_land) this.proxy.LetPilotKnow(newPlane.FlightNumber, newPlane.Status);
        }


        //ABORT MESSAGE
        public void AbortMessage(string flightNumber)
        {
            Airplane newPlane = this.proxy.GetAirplane(flightNumber);

            foreach(ListViewItem l in listView_flight.Items)
            {
                if (l.SubItems[2].Text.Substring(0, 5) == "Abort")
                {
                    if (newPlane.Status == AStatus.On_land) HelperLandAbort(newPlane, l);
                    if (newPlane.Status == AStatus.In_air) HelperAirAbort(flightNumber, l);
                }
            }         
        }

        //helpers for abort
        public void HelperLandAbort(Airplane newPlane, ListViewItem l)
        {
            //update status and start the docking process by calling LetPilotKnow
            AbortComplete(newPlane.FlightNumber, newPlane.Status);
            l.SubItems[1].Text = newPlane.FuelLevel.ToString();
            l.SubItems[2].Text = statusDict[newPlane.Status];
            this.proxy.LetPilotKnowAsync(newPlane.FlightNumber, newPlane.Status);
        }

        public void HelperAirAbort(string flightNumber, ListViewItem l)
        {
            listView_flight.Items.Remove(l);
            string label = locatePlane(flightNumber);
            prepareRunway(label);
        }

        //CANCEL MESSAGE
        public void RemoveMessage(string flightNumber)
        {
            this.RemoveFromView(flightNumber);
            this.proxy.UpdateStatusFromController(flightNumber, AStatus.In_air);
        }

        //DOCKED MESSAGE
        public void DockedMessage(string flightNumber)
        {
            this.RemoveFromView(flightNumber);                   
            string label = locatePlane(flightNumber);
            prepareRunway(label);
                
        }

        //REGULAR UPDATE
        public void RegularUpdate(string flightNumber)
        {
            Airplane newPlane = this.proxy.GetAirplane(flightNumber);

            foreach (ListViewItem l in listView_flight.Items)
                if (l.SubItems[0].Text == flightNumber)
                {
                    System.Diagnostics.Debug.WriteLine("enters condition for update listview event");
                    l.SubItems[1].Text = newPlane.FuelLevel.ToString();
                    l.SubItems[2].Text = statusDict[newPlane.Status];
                }
            System.Diagnostics.Debug.WriteLine("enters general event");
            //for docking process to start when the airplane is on land
            if (newPlane.Status == AStatus.On_land) this.proxy.LetPilotKnow(newPlane.FlightNumber, newPlane.Status);
        }

        public void NewPilotRequest(string flightNumber)
        {
            int index = 0;//for the cancel of the request
            bool found = false; // if false, the request has to added
            Airplane newPlane = this.proxy.GetAirplane(flightNumber);
            
            // string array for fuel and status
            string[] st = new string[2];
            st[0] = newPlane.FuelLevel + "%";
            st[1] = statusDict[newPlane.Status];

            //check whether the request is cancel or not and address the 2 situations differently
            if (newPlane.Status != AStatus.Null)
            {


                //update if in the list
                foreach (ListViewItem l in listView_flight.Items)
                    if (l.SubItems[0].Text == flightNumber)
                    {
                        found = true;

                        //check if it's the abort complete and update if on land; remove otherwise


                        //abort, MOVED!!

                        if (l.SubItems[2].Text.Substring(0, 5) == "Abort")
                        {

                            if (newPlane.Status == AStatus.On_land)
                            {

                                //update status and start the docking process by calling LetPilotKnow
                                AbortComplete(newPlane.FlightNumber, newPlane.Status);
                                l.SubItems[1].Text = newPlane.FuelLevel.ToString();
                                l.SubItems[2].Text = statusDict[newPlane.Status];
                                this.proxy.LetPilotKnowAsync(newPlane.FlightNumber, newPlane.Status);

                            }
                            if (newPlane.Status == AStatus.In_air)
                            {

                                //remove
                                listView_flight.Items.Remove(l);
                                string label = locatePlane(flightNumber);
                                prepareRunway(label);
                                break;

                            }
                        }
                        else
                        {   //else, it is not abort but docked
                            if (newPlane.Status == AStatus.Docked)
                            {
                                //docked, MOVED

                                ////we remove it as well
                                listView_flight.Items.Remove(l);
                                string label = locatePlane(flightNumber);
                                prepareRunway(label);
                                break;
                            }

                            else
                            {
                                //standard case, MOVED

                                //finally, standard case; simple update
                                l.SubItems[1].Text = newPlane.FuelLevel.ToString();
                                l.SubItems[2].Text = statusDict[newPlane.Status];
                            }

                        }

                    }
                //add if not in the list
                if (!found)
                {
                    //Create ListView item

                    //add to list, MOVED

                    ListViewItem item = new ListViewItem(newPlane.FlightNumber);
                    item.SubItems.AddRange(st);// string array to listView added as subitems  
                    item.BackColor = Color.Yellow; //change color to yellow
                    item.Tag = newPlane;

                    this.listView_flight.Items.Add(item);

                    //undocking request from pilot undock method, dock process should be started 
                    //and the plane becomes docked if no action is taken
                    if (newPlane.Status == AStatus.On_land) this.proxy.LetPilotKnow(newPlane.FlightNumber, newPlane.Status);

                }

            }

            //cancel message, MOVED!!
            else {
                for (int i = 0; i < listView_flight.Items.Count; i++)
                    if (listView_flight.Items[i].SubItems[0].Text == flightNumber)
                    {
                        index = i;
                        break;
                    }
                listView_flight.Items[index].Remove();
            }
        }

        //Method to help splitting command line text 
        private void CommandLineHelper(out string flight, out string RunwayId, out string request)
        {
            Airplane a = this.proxy.GetAirplane(tb_command.Text.Substring(0,6));

            string commandLine = tb_command.Text;
            string[] aliases = commandLine.Split(' ');


            if(a.Status == AStatus.Emergency) {
                flight = aliases[0];
                if (aliases[1] == "land") RunwayId = "R4";
                else RunwayId = aliases[1];
                request = "land";
                
            }

            else {

                try
            {

                flight = aliases[0];
                RunwayId = aliases[1];
                request = aliases[2];

                if (this.proxy.GetAirplane(flight) == null)
                    flight = null;

            }
            catch
            {
                flight = request = RunwayId = null;
            }
            }
            
        }

        private bool CommandExecutor(string flight, string RunwayId, string request)
        {
            try
            {
        
                switch (request)
                {
                    case "land":
                        this.proxy.ApproveLandRequestAsync(flight, RunwayId);
                        break;   
                        
                    case "takeoff":                       
                        this.proxy.ApproveTakeOffRequestAsync(flight, RunwayId);                         
                        break;

                    case "taxi":
                        this.proxy.sendTaxiCommandAsync(flight, RunwayId);
                        break;

                    default:
                        lb_message.Text = flight + ", not a valid command, Please enter again.";
                        break;
                }
            }
            catch
            {
                lb_message.Text = "Something went wrong , Please try again.";
            }
            finally
            {
                this.tb_command.Clear();
                this.tb_command.Focus();

            }

        
            return true;

        }

        //event handlers
        private void btnEnter_Click(object sender, EventArgs e)
        {

            
        }

        
        private void btn_land_Click(object sender, EventArgs e)
        {
            tb_command.Text += " land";
            //string command = tb_command.Text;
            //string flight, RunwayId, request;

            //this.CommandLineHelper(out flight, out RunwayId, out request);
            //if (flight != null)
            //{
            //    if (this.CommandExecutor(flight, RunwayId, request))
            //    {   

            //        lb_message.Text = "Landing approval sent to " + flight + " on Runway " + RunwayId;
            //    }

            //}
            //else
            //    lb_message.Text = "Flight does not exist.";
        }

        private void btn_taxi_Click(object sender, EventArgs e)
        {
            tb_command.Text += " taxi";            
            //string flight, RunwayId, request;
            //this.CommandLineHelper(out flight, out RunwayId, out request);
            //if (flight != null)
            //{
            //    if (this.CommandExecutor(flight, RunwayId, request))
            //        lb_message.Text = "Taxi commant sent to " + flight + " to Runway " + RunwayId;

            //}
            //else
            //    lb_message.Text = "Flight doesnot exist.";
        }

        private void btn_takeoff_Click(object sender, EventArgs e)
        {
            tb_command.Text += " takeoff";
            //string flight, RunwayId, request;
            //this.CommandLineHelper(out flight, out RunwayId, out request);
            //if (flight != null)
            //{
            //    if (this.CommandExecutor(flight, RunwayId, request))
            //    {
            //        lb_message.Text = "Takeoff approvel sent to " + flight + " from Runway " + RunwayId;
            //    }

            //}
            //else
            //    lb_message.Text = "Flight doesnot exist.";
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.proxy.UnSubscribeEvent();
        }

        private void btn_abort_Click(object sender, EventArgs e)
        {
            try
            {
                string flight = tb_command.Text.Substring(0, 6);
                //because the commandlinehelper was returning null for this reqest (same for emergency) i first check if the plane exist in our list 
                //and if it's either taxied or about to land (so displayed on runway)
                foreach (Airplane a in listOfPlanes)
                {
                    if (a.FlightNumber == flight)
                    {
                        if (a.Status == AStatus.Taking_Off
                            || a.Status == AStatus.Landing
                            || statusDict[a.Status].Substring(0, 4) == commandDict[Command.Taxi]) 
                        {
                            tb_command.Text += " abort";
                            string RunwayId = locatePlane(flight);
                            if (RunwayId != null)
                            {
                                //switch (RunwayId)
                                //{

                                //    case "R1": backgroundWorker1.CancelAsync(); break;
                                //    case "R2": backgroundWorker2.CancelAsync(); break;
                                //    case "R3": backgroundWorker3.CancelAsync(); break;
                                //}

                                proxy.AbortInTheServerAsync(flight, RunwayId);

                                
                                return;
                            }
                            else { lb_message.Text = "This plane doesn't have approved request to abort."; return; }
                        }
                    }
                    else
                        lb_message.Text = "Flight does not exist.";
                }
            }
            catch (Exception) { lb_message.Text = "No flight selected"; }
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Check priodically for the update of a plane in the list
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
              TasklistOfPlanes = this.proxy.GetAirplaneListAsync();
                TasklistOfPlanes.ContinueWith((i) =>
                {
                    if (!Token.IsCancellationRequested)
                    {
                        try
                        {
                            if (!Token.IsCancellationRequested)
                            {
                                listOfPlanes = i.Result.ToList();
                            }
                           // TasklistOfPlanes.Dispose();
                        }
                        catch (AggregateException)
                        {
                            listOfPlanes = new List<Airplane>();
                        }
                    }
                    
                },Token);
            }
            catch (Exception)
            {
                
            }
            try
            {
                if (listOfPlanes.Count > 0)
                {
                    Task<int> t = proxy.GetUpdatePositionAsync();
                    int counter = -1;
                    t.ContinueWith((i) =>
                    {
                        try
                        {
                            if (!Token.IsCancellationRequested)
                            {
                                counter = i.Result;
                            }
                            t.Dispose();
                        }
                        catch (AggregateException)
                        {
                            counter = -1;
                            t.Dispose();
                        }
                    }, Token);
                    try
                    {
                        t.Wait(2000, Token);
                    }
                    catch (AggregateException)
                    {
                        counter = -1;
                        t.Dispose();
                    }


                    if (counter > -1)
                    {
                        string[] st = new string[2];
                        st[0] = listOfPlanes[counter].FuelLevel.ToString() + "%";
                        st[1] = statusDict[listOfPlanes[counter].Status];
                        ListViewItem item = new ListViewItem();
                        item.Text = listOfPlanes[counter].FlightNumber;
                        item.SubItems.Add(st[0]);

                        //the status remains unchanged if it is aborting, otherwise it changes
                        if (listOfPlanes[counter].Status == AStatus.Abort_landing
                            || listOfPlanes[counter].Status == AStatus.Abort_takeoff)
                            item.SubItems.Add(listView_flight.Items[counter].SubItems[2]);
                        else item.SubItems.Add(st[1]);

                        if (listView_flight.Items.Count != 0)
                        {
                            this.listView_flight.Items[counter] = item;
                        }
                        proxy.ResetUpdatePositionAsync();
                    }
                }
            }
            catch(Exception)
            {

            }
        }
        
      
        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                TcpClient tcp = new TcpClient(proxy.Endpoint.Address.Uri.Host, Convert.ToInt32(proxy.Endpoint.Address.Uri.OriginalString.TrimStart('h', 't', 't', 'p', '/', '/', 'l', 'o', 'c', 'a', 'l', 'h', 'o', 's', 't', ':').Split('/')[0]));
                lbl_serverStatus.Text = " I ";
                lbl_serverStatus.BackColor = Color.Green;
                if(Switch == true)
                {
                    this.context = new InstanceContext(this);
                    this.proxy = new ControllerClient(context);
                    proxy.SubscribeEvent();
                    CancelTokenSource = new CancellationTokenSource();
                    Token = CancelTokenSource.Token;
                    timer1.Start();
                    Switch = false;
                }
            }
            catch (Exception)
            {
                CancelTokenSource.Cancel();
                lbl_serverStatus.Text = "O";
                lbl_serverStatus.BackColor = Color.Red;
                if (Switch == false)
                {
                    
                    timer1.Stop();
                    Switch = true;
                }
            }
            // timer1.Stop();
            // IPAddress ip =  Dns.GetHostAddresses(proxy.Endpoint.Address.Uri.Host)[0];
            //PingReply reply = ping.Send(ip);
            // pining = reply.Status == IPStatus.Success;
            // MessageBox.Show(pining.ToString());
            // timer1.Start();
        }



        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }


        private void hidePlanes()
        {
            planeR1.Hide();
            planeR2.Hide();
            planeR3.Hide();
            pictureBox10.Hide();

        }

        private void listView_flight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_flight.SelectedItems.Count != 0)
            {
                string flightNr = listView_flight.SelectedItems[0].Text.ToString();
                tb_command.Text = flightNr;
                lb_message.Text = flightNr + " is selected.";
            }

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            tb_command.Text += " R1";
            lb_message.Text = "Runway R1 is selected.";
        }

        private void R2_green_Click(object sender, EventArgs e)
        {
            tb_command.Text += " R2";
            lb_message.Text = "Runway R2 is selected.";
        }

        private void R3_green_Click(object sender, EventArgs e)
        {
            tb_command.Text += " R3";
            lb_message.Text = "Runway R4 is selected.";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tb_command.Text = "";
        }

        private bool taxi(string runway)
        {
            PictureBox plane = null;
            Point location = new Point(0, 0);
            switch (runway)
            {
                case "R1":
                    plane = planeR1;
                    location = startR1;
                    break;
                case "R2":
                    plane = planeR2;
                    location = startR2;
                    break;
                case "R3":
                    plane = planeR3;
                    location = startR3;
                    planeR3.Hide();
                    break;
            }

            if (plane != null)
            {
                if (plane.Visible) return false;
                plane.Location = location;
                plane.Show();
                return true;

            }
            return false;
        }


        bool R1 = false, R2 = false, R3 = false, R4 = false;
        public void UserInterface(string flight, int progress)
        {
            try
            {
                Airplane a = proxy.GetAirplane(flight);
                Runway r = a.Runway;
                if (r.RunwayID.Equals("R1"))
                {
                    tForR1 = new Thread(() => UpdateForR1(flight, progress));
                    tForR1.Start();
                }
                if (r.RunwayID.Equals("R2"))
                {

                    tforR2 = new Thread(() => UpdateForR2(flight, progress));
                    tforR2.Start();
                }

                if (r.RunwayID.Equals("R3"))
                {

                    tForR3 = new Thread(() => UpdateForR3(flight, progress));
                    tForR3.Start();
                }
                if (r.RunwayID.Equals("R4"))
                {
                    tforR4 = new Thread(() => UpdateForR4(flight, progress));
                    tforR4.Start();
                }
            }
            catch (Exception)
            { }
         
        }

        public void UpdateForR1(string flight, int progress)
        {
            Airplane a = proxy.GetAirplane(flight);
            bool emergencyFlag = false;
            //if its the first time, it sets the U interface
            if (!R1)
            {
                R1 = true;

                if (a.Status == AStatus.Emergency_landing) emergencyFlag = true;
                attachLabel(flight, "R1", emergencyFlag);

                Point start = new Point(23, 50);

                //if the plane wasnt taxied we have to display it on the runway
                this.Invoke((MethodInvoker)delegate
                {
                    planeR1.Location = start;
                    planeR1.Show();
                    lblR1.Location = new Point(18, 75);
                    R1_red.Show();
                    attachLabel(flight, "R1", emergencyFlag);
                });
            }

            //otherwise, keep updating
            this.Invoke((MethodInvoker)delegate
            {
                planeR1.Location = new Point(23 + progress * 60, 50);
                lblR1.Location = new Point(23 + progress * 55, 75);
                planeR1.Show();
            });

            //if the progress is 100% (progress = 10) than the animation is stopped
            if(progress == 10)
            {
                //unless it was emergency landing then we occupy the runway for 10 more seconds
                if (emergencyFlag) { emergencyPlane = planeR1; timerEmergency.Start(); return; }

                else
                {
                    this.Invoke((MethodInvoker)delegate
                        {
                            //or prepareRunway method
                            R1_red.Hide();
                            R1_green.Show();
                            planeR1.Hide();
                            R1 = false;
                        });
                }

                //set status of runway and prepare it for future use
                //Airplane a = this.proxy.GetAirplane(flight);
                //a.Runway.Status = true;
                //prepareRunway("R1");
            }
        }

        public void UpdateForR2(string flight, int progress)
        {
            Airplane a = proxy.GetAirplane(flight);
            bool emergencyFlag = false;
            //first time the pilot updates, the interface is initiated
            if (!R2)
            {
                R2 = true;
                if (a.Status == AStatus.Emergency_landing) emergencyFlag = true;
                attachLabel(flight, "R2", emergencyFlag);

                this.Invoke((MethodInvoker)delegate
                {
                    planeR2.Location = startR2;
                    planeR2.Show(); 
                    lblR2.Location = new Point(startR2.X, startR2.Y + 25);
                    R2_red.Show();
                });
            }

            //animation is produced
            this.Invoke((MethodInvoker)delegate
            {
                planeR2.Location = new Point(startR2.X + progress * 50, startR2.Y);
                lblR2.Location = new Point(startR2.X + progress * 50, startR2.Y + 25);
            });


            //at the end
            if (progress == 10)
            {
                if (emergencyFlag) { timerEmergency.Start(); emergencyPlane = planeR2; return; }

                else
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        //or prepareRunway method
                        
                        R2_red.Hide();
                        R2_green.Show();
                        planeR2.Hide();
                        R2 = false;

                 
                    });
                }

                //set status of runway and prepare it for future use
                //Airplane a = this.proxy.GetAirplane(flight);
                //a.Runway.Status = true;
                //prepareRunway("R2");
            }
        }

        public void UpdateForR3(string flight, int progress)
        {
            Airplane a = proxy.GetAirplane(flight);
            bool emergencyFlag = false;
            //first time
            if (!R3)
            {
                R3 = true;
                if (a.Status == AStatus.Emergency_landing) emergencyFlag = true;
                attachLabel(flight, "R3", emergencyFlag);

                this.Invoke((MethodInvoker)delegate
                {
                    planeR3.Location = startR3;
                    lblR3.Location = new Point(startR3.X, startR3.Y + 25);
                    R3_red.Show();
                });
            }

            //subsequent times
            this.Invoke((MethodInvoker)delegate
            {
                planeR3.Location = new Point(startR3.X + progress * 50, startR3.Y);
                lblR3.Location = new Point(startR3.X + progress * 50, startR3.Y + 25);
            });

            //at the end
            if (progress == 10)
            {
                if (emergencyFlag) { timerEmergency.Start(); emergencyPlane = planeR3; return; }

                else
                {  //set interface for end of animation
                    this.Invoke((MethodInvoker)delegate
                    {
                        R3_red.Hide();
                        R3_green.Show();
                        lblR3.Hide();
                        planeR3.Hide();
                        R3 = false;
                        //or prepareRunway method

                    });
                }
               

                //set status of runway and prepare it for future use
                //Airplane a = this.proxy.GetAirplane(flight);
                //a.Runway.Status = true;
                //prepareRunway("R3");
            }

        }

        //FOR EMERGENCY LANDING
        public void UpdateForR4(string flight, int progress)
        {
            Airplane a = proxy.GetAirplane(flight);
            bool emergencyFlag = true;

            //if its first time
            if (!R4)
            {
                R4 = true;
                attachLabel(flight, "R4", emergencyFlag);

                this.Invoke((MethodInvoker)delegate
                {
                 
                    //plane set
                    pictureBox10.Location = startR4;
                    pictureBox10.Show();
                    //label R4
                    lblR4.Location = new Point(startR4.X, startR4.Y + 25);
                    //hide green lane
                    pictureBox15.Hide();
                    //show red lane
                    pictureBox14.Show();

                });
            }

            //subsequent times
            this.Invoke((MethodInvoker)delegate
            {
                pictureBox10.Location = new Point(startR4.X + progress * 50, startR4.Y);
                lblR4.Location = new Point(startR4.X + progress * 50, startR4.Y + 25); 
            });
            

            if (progress == 10)
            {
                if (emergencyFlag) { timerEmergency.Start(); emergencyPlane = pictureBox10; return; }
                //set interface for end of animation
                this.Invoke((MethodInvoker)delegate
                {
                    //green one hidden
                    pictureBox15.Hide();
                    //red one hidden
                    pictureBox14.Hide();
                    //grey is showed
                    pictureBox3.Show();
                    //plane is hidden
                    pictureBox10.Hide();
                    //hide label
                    lblR4.Hide(); 

                    R4 = false;
                });
            }
        }

        private void backgroundWorker4_DoWork(object sender, DoWorkEventArgs e)
        {
            Point tenstart = new Point(505, 313);
            this.Invoke((MethodInvoker)delegate
            {
                pictureBox10.Location = tenstart;
                pictureBox10.Show();
                pictureBox15.Hide();
            });

            //Thread.Sleep(3000);
            int prog = 0;
            for (int i = 455; i > 15; i = i - 50)
            {
                emergencyLocation = new Point(i, 313);
                prog = prog + 10;
                backgroundWorker4.ReportProgress(prog);
                Thread.Sleep(1000);
            }
            this.Invoke((MethodInvoker)delegate
            {
                //this was an emergency landing for sure so we are occupying the lane
                Thread.Sleep(5000);
                pictureBox10.Hide();
            });
        }

        private void backgroundWorker4_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                pictureBox10.Location = emergencyLocation;
            });

        }


        public void UpdateListView(string flight, AStatus status)
        {
            int i;
                for (i = 0; i < listView_flight.Items.Count; i++)
                {
                    if (listView_flight.Items[i].SubItems[0].Text == flight)
                    {
                        listView_flight.Items[i].SubItems[2].Text = statusDict[status];
                    }
                }
        }

        public void RemoveFromView(string flightNumber)
        {
            int index = -1;
            if (listView_flight.Items.Count > 0)
            {
                for (int i = 0; i < listView_flight.Items.Count; i++)
                    if (listView_flight.Items[i].SubItems[0].Text == flightNumber)
                    {
                        index = i;
                        break;
                    }
                
                if(index > -1)
                listView_flight.Items[index].Remove();
            }

        }

        private void attachLabel(string flightNr, string runwayId, bool emergencyFlag)
        {
            Label lbl = null;
            Point loc = new Point(0, 0);
            switch (runwayId)
            {
                case "R1":
                    
                        lbl = lblR1;
                        loc = new Point(startR1.X, startR1.Y + 25);
                 
                    break;
                case "R2":
                   
                        lbl = lblR2;
                        loc = new Point(startR2.X, startR2.Y + 25);
                    break;
                case "R3":
                   
                        lbl = lblR3;
                        loc = new Point(startR3.X, startR3.Y + 25);
                    break;

                case "R4":
               
                        lbl = lblR4;
                        loc = new Point(startR4.X, startR4.Y + 25);
                    break;
            }
            this.Invoke((MethodInvoker)delegate
            {
                lbl.Location = loc;
                lbl.Visible = true;
                if (emergencyFlag) lbl.Text = "E " + flightNr;
                else lbl.Text = flightNr;
            });
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            tb_command.Text += " R1";
            lb_message.Text = "Runway R1 is selected.";
        }

        private void prepareRunway(string runway)
        {
            switch (runway)
            {
                case "R1":
                    R1_red.Hide();
                    planeR1.Hide();
                    lblR1.Hide();
                    break;
                case "R2":
                    R2_red.Hide();
                    planeR2.Hide();
                    lblR2.Hide();
                    break;
                case "R3":
                    R3_red.Hide();
                    lblR3.Hide();
                    break;  
                case "R4":
                    //green one hidden
                    pictureBox15.Hide();
                    //red one hidden
                    pictureBox14.Hide();
                    //grey is showed
                    pictureBox3.Show();
                    //plane is hidden
                    pictureBox10.Hide();
                    //hide label
                    lblR4.Hide();

                    R4 = false;
                    break;

            }
        }



        private void tb_command_TextChanged(object sender, EventArgs e)
        {
            //check if its the clear service command and enable the service lane
            if (tb_command.Text == "clear service" || tb_command.Text == "CLEAR SERVICE")
            {
                pictureBox15.Show();
            }

            string f = "", r = "", s = "";

            if (tb_command.Text.Split(' ').Length == 3)
            {
                if (checkCommand(tb_command.Text.Split(' ')[2]))
                {
                    this.CommandLineHelper(out f, out r, out s);
                    this.CommandExecutor(f, r, s);
                }
            }

        }

        public bool checkCommand(string comm)
        {
            if (comm == "land" || comm == "abort" || comm == "taxi" || comm == "takeoff")
            {
                return true;
            }
                return false;
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {

            tb_command.Text += " land";
            string flight, RunwayId, request;

            this.CommandLineHelper(out flight, out RunwayId, out request);
            if (flight != null)
            {
                if (this.CommandExecutor(flight, "R4", request))
                {

                    lb_message.Text = "Emergency Landing approval sent to " + flight + " on Runway " + RunwayId;
                }

            }
            else
                lb_message.Text = "Flight does not exist.";

            //try
            //{
            //    string flight = tb_command.Text.Substring(0, 6);
            //    int count = 0;
            //    foreach (Airplane a in listOfPlanes)
            //    {
            //        if (a.FlightNumber == flight)
            //        {
            //            tb_command.Text += " EMERGENCY";
            //            proxy.ApproveLandRequest(flight, "R4");
            //            count++;
            //        }
            //    }
            //    if (count == 0)
            //    {
            //        lb_message.Text = "Incorrect flight number";
            //    }
            //}
            //catch (Exception) { lb_message.Text = "No flight selected"; }

        }


        private string locatePlane(string flightNumber)
        {
            if (lblR1.Text == flightNumber) return "R1";
            if (lblR2.Text == flightNumber) return "R2";
            if (lblR3.Text == flightNumber) return "R3";
            return null;
        }

        public void AbortedCommand(string flightNumber, string status)
        {
            lb_message.Text = flightNumber + " aborting";
            this.Invoke((MethodInvoker)delegate
            {

                foreach (ListViewItem l in listView_flight.Items)
                    if (l.SubItems[0].Text == flightNumber)
                    {
                        l.SubItems[2].Text = status;
                    }
            });
        }

        //status parameter not useful anymore apparently
        public void AbortComplete(string flight, AStatus status)
        {
            string label = locatePlane(flight);
            prepareRunway(label);
            lb_message.Text = flight + " has finished the abort action.";
        }
        public void UpdateForLandingProcess(string flight, AStatus status, string RunwayId)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                //by this point we know the lanidng approval, not the request was successfully send, so we start occupying the runway
               // to show that abort can be performed on this flight
                //and to prevent the user from sending unnecessary approvals to the server thinking runway is free
                bool emerg= false;
                if (status == AStatus.Emergency_landing) emerg = true;
                UpdateListView(flight, status);
                lb_message.Text = flight + ", " + "landing approval is sent.";
                taxi(RunwayId); attachLabel(flight, RunwayId, emerg);
            });
        }

        public void UpdateForTakingOffProcess(string flight, AStatus status, string runwId)
        {
            this.BeginInvoke((MethodInvoker)delegate {
                
                    UpdateListView(flight, status);
                    lb_message.Text = flight + ", " + "take off" + " request is sent.";
                    //animate(flight, runwId, false);
             
            });

        }

        public void UpdateForTaxi(string flight, AStatus status, string RunwayID, bool success)
        {
            this.BeginInvoke((MethodInvoker)delegate {
                if (success)
                {
                    UpdateListView(flight, status);
                    taxi(RunwayID);
                    attachLabel(flight, RunwayID, false);
                }
                else lb_message.Text = "Command not allowed";                
            });    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.FileName = "unknown.txt";
            // set filters - this can be done in properties as well
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            List<string> temp = proxy.getCommandList().ToList();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog1.OpenFile());
                foreach (string s in temp)
                {
                    sw.Write(s);
                }
                sw.Dispose();
                sw.Close();
            }
        }

        public void SendErrorMessage(string message)
        {
            lb_message.Text = message;
        }

       
        public void AbortFinished(string flight, AStatus status)
        {
            string runw = this.proxy.GetAirplane(flight).Runway.RunwayID;
            if (status == AStatus.In_air) RemoveFromView(flight);
            else UpdateListView(flight, status);
            prepareRunway(runw);
            lb_message.Text = flight + " finished abort";
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        public void RemoveFromListView(string flight)
        {
            foreach (ListViewItem c in listView_flight.Items)
            {
                if(c.SubItems[0].Text== flight)
                {
                    listView_flight.Items.Remove(c);
                    break;
                }
            }
        }

        private void timerEmergency_Tick(object sender, EventArgs e)
        {
            if (emergencyTicks == 5)
            {    //hide the stuff
                unoccupy();
                emergencyTicks = 0;
                timerEmergency.Stop(); }
            emergencyTicks++;

        }
        private void unoccupy()
        { 
         if(lblR1.Text.Substring(0,1).Equals("E"))
         {
             prepareRunway("R1");
         }
         if (lblR2.Text.Substring(0, 1).Equals("E"))
         {
             prepareRunway("R2");
         }
         if (lblR3.Text.Substring(0, 1).Equals("E"))
         {
             prepareRunway("R3");
         }
         if (lblR1.Text.Substring(0, 1).Equals("E"))
         {
             prepareRunway("R4");
         }
        }
    }
}



/////code for the emergency lane animation

            //this.Invoke((MethodInvoker)delegate
            //{
            //    pictureBox7.Location = startR2;
            //    pictureBox7.Show();
            //    lblR2.Location = new Point(130, 285);
            //    pictureBox12.Show();
            //});
            //// attachLabel(currentPlane, currentRunway);
            //int someProgress = 0;

            //for (int i = 0; i < 5; i++)
            //{
            //    Thread.Sleep(1000);
            //    if (backgroundWorker2.CancellationPending == true)
            //    {
            //        e.Cancel = true;
            //        return;
            //    }
            //}


            //for (int i = 265; i > 0; i = i - 30)
            //{
            //    if (backgroundWorker2.CancellationPending == true)
            //    {
            //        e.Cancel = true;
            //        return;
            //    }
            //    nextPointR2 = new Point(100, i);
            //    someProgress = someProgress + 10;
            //    labelPointR2 = new Point(135, i - 5);
            //    backgroundWorker2.ReportProgress(someProgress);
            //    Thread.Sleep(1000);
            //}

            //this.Invoke((MethodInvoker)delegate
            //{
            //    if ((bool)e.Argument)
            //    {
            //        //it was an emergency request so occupy the runway for 5more sec
            //        Thread.Sleep(5000);
            //    }
            //    pictureBox7.Hide();
            //    prepareRunway("R2");
            //    lblR2.Hide();
            //});
