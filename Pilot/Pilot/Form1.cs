using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pilot.AirplaneServiceRef;
using System.ServiceModel;
using System.Net.Sockets;
using System.Reflection;

namespace Pilot
{

    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class Form1 : Form, IAirplaneCallback
    {
        //properties
        private string FlightNumber;
        private int FuelLevel;
        private AStatus status;
        private AirplaneClient proxy;
        private InstanceContext context;
        private DialogForm dialog;
        private int progress;
        private bool canAbort;        
        private int progressTicks;
        private bool connected;
        private string requestType;        
        bool semafor = true;


        Dictionary<AStatus, string> statusDict;

        // To cancel threads   -Jose
        private CancellationTokenSource CancelTokenSource;
        private CancellationToken Token;
        private object _lock;
        private bool Switch; // To see if the server is BACK online to reconnect the plane

        //constructor
        public Form1()
        {
            InitializeComponent();

            context = new InstanceContext(this);
            proxy = new AirplaneClient(context);

            CancelTokenSource = new CancellationTokenSource();
            Token = CancelTokenSource.Token;
            Switch = false;
            connected = false;

            canAbort = false;
            Cancel_Request.Enabled = false;
            timer1.Interval = 1000;
            timer2.Interval = 30000;
            dialog = new DialogForm();
            dialog.Show();
            dialog.FormClosed += Dialog_FormClosed;
            this.FlightNumberAndFuelGenerator();
            lb_flightNumber.Text = FlightNumber;

            _lock = new object();

            progress = -1;
            progressTicks = 0;
            this.Undock.Enabled = false;
            
            statusDict = proxy.GetStatusDictionaryPilot();
        }
        //This is to pass the data from form2 Reason:It ust became more complicated than i thought   -Jose
        private void Dialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            ConnectToServer();
        }

        public void ConnectToServer()
        {

            FuelLevel = dialog.fuel;
            //magic stuff
            status = statusDict.FirstOrDefault(x => x.Value == dialog.status).Key;

            //if (status == AStatus.On_the_ground) {
            //    this.connected = true;
            //    this.proxy.Connect(FlightNumber, FuelLevel, status);
            //}

           
            lb_status.Text = statusDict[status];
            lb_fuel.Text = FuelLevel + " %";
            pb_fuelLevel.Value = FuelLevel;

            if (status == AStatus.In_the_air)
            {
                Take_Off_Request.Visible = false;

            }
            else
            {
                btn_LandingRequest.Visible = false;
            }
            timer1.Start();
            timer2.Start();
        }

        //methods
        public void FlightNumberAndFuelGenerator()
        {
            Random random = new Random();
            //generates Flight Number
            char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            char[] nums = "0123456789".ToCharArray();
            char[] numbers = new char[4];
            char[] stringChars = new char[2];

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = nums[random.Next(nums.Length)];
            }
            this.FlightNumber = new String(stringChars) + new string(numbers);

            //generates the Fuel Level
            //this.FuelLevel = random.Next(40, 100);

            //generates status
            //if (random.Next(1, 3) == 1)
            //{
            //    this.status = "Departing";
            //    this.btn_request.Text = "Send takeoff Request";
            //}
            //else
            //{
            //    this.status = "Landing";
            //    this.btn_request.Text = "Send landing Request";
            //}
        }


        public void RequestConfirm( string RunwayId)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
               
                if (btn_LandingRequest.Visible == true || this.status == AStatus.Wait_emergency)
                {
                    System.Diagnostics.Debug.WriteLine("request reached");

                    this.btn_LandingRequest.BackColor = Color.Green;
                    this.btn_LandingRequest.Text = "Confirmed";
                    this.btn_LandingRequest.Enabled = false;
                    if (status == AStatus.Wait_emergency)
                        status = AStatus.Emergency_landing;
                    else status = AStatus.Landing;
                }
                else
                {
                    this.Take_Off_Request.BackColor = Color.Green;
                    this.Take_Off_Request.Text = "Confirmed";
                    this.Take_Off_Request.Enabled = false;
                    status = AStatus.Taking_Off;
                    this.FuelLevel = 100;
                    timer2.Start();                      
                    }

                //setting the process of landing/taking off
                progress = -1;
                progressTicks = 0;               
                semafor = true;
                timerProgress.Start();
                

                lb_status.Text = statusDict[status];
                if (status == AStatus.Taking_Off || status == AStatus.Landing) this.Cancel_Request.Enabled = false;
                
            });

        }

        // this is for landing request  -Jose
        private void btn_landingrequest_Click(object sender, EventArgs e)
        {
            connected = false;
            if (connected == false)
            {
                this.proxy.Connect(this.FlightNumber, this.FuelLevel, this.status);
                this.connected = true;
            }
            try
            {
                this.proxy.LandRequest(this.FlightNumber);
                requestType = "Land";
            }
            catch (Exception)
            {

            }
            this.BeginInvoke((MethodInvoker)delegate
            {
                btn_LandingRequest.Enabled = false;
                Cancel_Request.Enabled = true;
                //Change in the status
                status = AStatus.Wait_landing;
                lb_status.Text = statusDict[status];
            });

        }

        private void Take_Off_Request_Click(object sender, EventArgs e)
        {
            connected = false;
            System.Diagnostics.Debug.WriteLine("ENters takeoff handler");
            //connect
            if (connected == false)
            {
                this.proxy.Connect(this.FlightNumber, this.FuelLevel, this.status);
                this.connected = true;
            }

                System.Diagnostics.Debug.WriteLine("ENters try block");
                this.proxy.TakeOffRequest(this.FlightNumber);
                requestType = "TakeOff";

            this.BeginInvoke((MethodInvoker)delegate
            {
                Take_Off_Request.Enabled = false;
                Cancel_Request.Enabled = true;
            });


            // change in the status
            status = AStatus.Wait_takeoff;
            this.BeginInvoke((MethodInvoker)delegate
            {
                lb_status.Text = statusDict[status];
            });



        }

        private void Cancel_Request_Click(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                this.proxy.CancelRequest(FlightNumber);

                if (btn_LandingRequest.Visible == true)
                {
                    btn_LandingRequest.Enabled = true;
                    status = AStatus.In_air;
                    lb_status.Text = statusDict[status];
                }
                else
                {
                    Take_Off_Request.Enabled = true;
                    status = AStatus.On_land;
                    lb_status.Text = statusDict[status];
                }
                Cancel_Request.Enabled = false;
            });


        }

        //Checks server status. If server is on or not    -Jose
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                TcpClient tcp = new TcpClient(proxy.Endpoint.Address.Uri.Host, Convert.ToInt32(proxy.Endpoint.Address.Uri.OriginalString.TrimStart('h', 't', 't', 'p', '/', '/', 'l', 'o', 'c', 'a', 'l', 'h', 'o', 's', 't', ':').Split('/')[0]));
                lbl_serverStatus.Text = " I ";
                lbl_serverStatus.BackColor = Color.Green;
                if (Switch == true)
                {
                    this.context = new InstanceContext(this);
                    this.proxy = new AirplaneClient(context);
                    CancelTokenSource = new CancellationTokenSource();
                    Token = CancelTokenSource.Token;
                    if (connected == true)
                    {
                        lock (this._lock)
                        {
                            proxy.Connect(this.FlightNumber, this.FuelLevel, this.status);
                            try
                            {
                                if (requestType == "TakeOff")
                                {
                                    this.proxy.TakeOffRequestAsync(this.FlightNumber);
                                }
                                else if (requestType == "Land")
                                {
                                    this.proxy.LandRequestAsync(this.FlightNumber);
                                }
                                else if (requestType == "Emergency")
                                {
                                    this.proxy.EmergencyRequestAsync(this.FlightNumber);
                                }
                            }
                            catch (Exception)
                            {

                            }
                        }
                    }
                    Switch = false;
                    
                }
            }
            catch (Exception)
            {
                CancelTokenSource.Cancel();
                lbl_serverStatus.Text = "O";
                lbl_serverStatus.BackColor = Color.Red;
                Switch = true;
            }
            // timer1.Stop();
            // IPAddress ip =  Dns.GetHostAddresses(proxy.Endpoint.Address.Uri.Host)[0];
            //PingReply reply = ping.Send(ip);
            // pining = reply.Status == IPStatus.Success;
            // MessageBox.Show(pining.ToString());
            // timer1.Start();
        }

        //Reduce and updates the server its fuel     -Jose
        private void timer2_Tick(object sender, EventArgs e)
        {

            FuelLevel -= 1;
            pb_fuelLevel.Value = FuelLevel;
            lb_fuel.Text = FuelLevel + " %";
            Task t = Task.Run(() =>
            {
                if (!Token.IsCancellationRequested)
                {
                    try
                    {
                        proxy.UpdatePlane(this.FlightNumber, this.FuelLevel);
                        
                    }
                    catch (Exception)
                    {

                    }
                   
                    
                }
            },Token);
            t.ContinueWith((i) => 
            {
                t.Dispose();
            });
            


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        Thread t;
        public delegate void Work(string nextstatus);

        public void StatusChangedPilot(string flightNumber, AStatus status)
        {
            string previousStatus = lb_status.Text;

            if (this.FlightNumber == flightNumber)
            {
                AStatus nextStatus = AStatus.Null;
                lb_status.Text = statusDict[status];
                this.status = status;

                //i assume the fuel is 100% at taking off
                if (status == AStatus.Taking_Off)
                {
                    this.FuelLevel = 100;
                    timer2.Start();
                }

                //you cant cancel request while performing an action
                //if (status == "Taking Off" || status == "Landing") this.Cancel_Request.Enabled = false; - moved to requestconfirm


                //UI update
               

                if (status == AStatus.Abort_landing)
                {
                    //timer2.Start();
                    this.btn_LandingRequest.BackColor = Color.Red;
                    this.btn_LandingRequest.Text = statusDict[status];
                    this.btn_LandingRequest.Visible = true;
                    this.btn_LandingRequest.Enabled = false;
                    nextStatus = AStatus.In_air;

                }
                if (status == AStatus.Abort_takeoff || status == AStatus.Abort_taxi)
                {
                    this.Take_Off_Request.Visible = true;
                    this.Take_Off_Request.Enabled = false;
                    this.Take_Off_Request.BackColor = Color.Red;
                    this.Take_Off_Request.Text = statusDict[status];
                    timer2.Stop();
                    nextStatus = AStatus.On_land;
                }

                
                if (status == AStatus.On_land)
                {
                    timer2.Stop();
                    this.btn_LandingRequest.BackColor = default(Color);
                    this.btn_LandingRequest.Text = "Land request";
                    this.btn_LandingRequest.Visible = false;

                    this.Take_Off_Request.Visible = true;
                    this.Take_Off_Request.Enabled = true;
                }
                if (status == AStatus.In_air)
                {
                    this.Take_Off_Request.BackColor = default(Color);
                    this.Take_Off_Request.Text = "Take off request";
                    this.Take_Off_Request.Visible = false;

                    this.btn_LandingRequest.Visible = true;
                    this.btn_LandingRequest.Enabled = true;
                }

                Cancel_Request.Enabled = false;
                if (nextStatus != AStatus.Null)
                {
                    t = new Thread(() => DoAbort(nextStatus));
                    t.Start();
                }

                if (status == AStatus.Docked)
                {
                    this.Take_Off_Request.Enabled = false;
                    this.Take_Off_Request.BackColor = default(Color);
                    this.Take_Off_Request.Text = "Take off request";
                    this.btn_LandingRequest.Enabled = false;
                    this.button1.Enabled = false;
                    this.Cancel_Request.Enabled = false;
                    this.Undock.Enabled = true;
                }
            }


        }
        private void DoAbort(AStatus nextstatus)
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1000);
            }

            this.status = nextstatus;
            this.Aborted(nextstatus);
            timerProgress.Stop();
            proxy.AbortComplete(FlightNumber, this.status);
        }


        private void button1_Click(object sender, EventArgs e)
        {

            if (connected == false)
            {
                this.proxy.Connect(this.FlightNumber, this.FuelLevel, this.status);
                this.connected = true;
            }
            try
            {
                if (status == AStatus.In_air || status == AStatus.In_the_air || status == AStatus.Wait_landing)
                {
                    proxy.EmergencyRequest(this.FlightNumber);
                    status = AStatus.Wait_emergency;
                    lb_status.Text = statusDict[status];
                    requestType = "Emergency";
                }
                else MessageBox.Show("Emergency calls only allowed in air");
                
            }
            catch (Exception)
            {

            }

        }

        //self-explanatory
        private void Undock_Click(object sender, EventArgs e)
        {
            Take_Off_Request.Enabled = true;
            Take_Off_Request.Visible = true;

            this.Undock.Enabled = false;

            btn_LandingRequest.Visible = false;
            btn_LandingRequest.Enabled = false;
            this.status = AStatus.On_land;
            //if (status == AStatus.On_land)
            //{
            //    this.connected = true;
            //    this.proxy.Connect(FlightNumber, FuelLevel, status);
            //}
            lb_status.Text = statusDict[status];
            //this.proxy.UpdateStatusFromPilot(this.FlightNumber, this.status);
        }

        public string getFlightNr()
        {
            return FlightNumber;
        }

        
        private void timerProgress_Tick(object sender, EventArgs e)
        {

            if (progressTicks < 2)
            {
                canAbort = true;
            }
            progressTicks = progressTicks + 1;
            if (progressTicks > 2)
            {

                canAbort = false;
                progress = progress + 1;

                if(semafor && progress < 10)
                    proxy.reportProgress(FlightNumber, progress);
                if (progress == 10)
                {
                    //condition to occupy extra for emergency
                    if (this.status == AStatus.Emergency_landing && semafor)
                    {
                        semafor = false;
                        progress -= 2;
                    }

                    else
                    {
                        timerProgress.Stop();
                        proxy.reportProgress(FlightNumber, 10);
                        if (status == AStatus.Landing || status == AStatus.Emergency_landing) { this.status = AStatus.On_land; timer2.Stop(); }
                        if (status == AStatus.Taking_Off) this.status = AStatus.In_air;
                        lb_status.Text = statusDict[status];
                        this.AfterAnimation();
                        this.proxy.UpdateStatusFromPilot(this.FlightNumber, this.status);
                    }

                }

            }

        }
           
        public void AfterAnimation()
        {
            this.Invoke((MethodInvoker)delegate {

                lb_status.Text = statusDict[status];

                if (this.status == AStatus.On_land)
                {
                    btn_LandingRequest.Visible = false;
                    btn_LandingRequest.Text = "Land request";
                    btn_LandingRequest.BackColor = default(Color);

                    Take_Off_Request.Visible = true;
                    Take_Off_Request.Enabled = true;

                    button1.Enabled = false;
                }

                if (this.status == AStatus.In_air)
                {
                    btn_LandingRequest.Visible = true;
                    btn_LandingRequest.Enabled = true;
                    btn_LandingRequest.Text = "Land request";
                    btn_LandingRequest.BackColor = default(Color);

                    Take_Off_Request.Visible = false;

                    button1.Enabled = true;
                }
            });
            
        }

        bool IAirplaneCallback.canAbort()
        {
            if (this.canAbort) {
                timerProgress.Stop();
                return true; }
            else return false;
        }

        public void Aborted(AStatus status)
        {
            //setting the button request landing to be as usual + label
            this.Invoke((MethodInvoker)delegate {

              
                lb_status.Text = statusDict[status];

                if (status == AStatus.On_land)
                {
                    Take_Off_Request.BackColor = Color.Green;
                    Take_Off_Request.Text = "Takeoff request";
                    btn_LandingRequest.Visible = false;                   
                    Take_Off_Request.Visible = true;
                    Take_Off_Request.Enabled = true;
                }

                if (status == AStatus.In_air)
                {
                    btn_LandingRequest.BackColor = Color.Green;
                    btn_LandingRequest.Text = "Land request";
                    btn_LandingRequest.Visible = true;
                    btn_LandingRequest.Enabled = true;
                    Take_Off_Request.Visible = false;
                }
            });
            



            //I will leave it here for future reference

            //    AStatus nextStatus = AStatus.Null;
            //   if(status == AStatus.Landing)
            //   {
            //       status = AStatus.Abort_landing;
            //       this.btn_request.BackColor = Color.Red;
            //       this.btn_request.Text = statusDict[status];
            //       this.btn_request.Visible = true;
            //       this.btn_request.Enabled = false;
            //       nextStatus = AStatus.In_air;
            //   }
            //   if (status == AStatus.Taking_Off)
            //   {
            //       status = AStatus.Abort_takeoff;
            //       proxy.UpdateStatusFromPilot(this.FlightNumber, status);
            //       this.Take_Off_Request.Visible = true;
            //       this.Take_Off_Request.Enabled = false;
            //       this.Take_Off_Request.BackColor = Color.Red;
            //       this.Take_Off_Request.Text = statusDict[status];
            //       timer2.Stop();
            //       nextStatus = AStatus.On_land;
            //   }
            //    if(status.Equals(AStatus.Taxi_R1)||status.Equals(AStatus.Taxi_R2)||status.Equals(AStatus.Taxi_R3))
            //    {
            //        status = AStatus.Abort_taxi;
            //        this.Take_Off_Request.Visible = true;
            //        this.Take_Off_Request.Enabled = false;
            //        this.Take_Off_Request.BackColor = Color.Red;
            //        this.Take_Off_Request.Text = statusDict[status];
            //        timer2.Stop();
            //        nextStatus = AStatus.On_land;
            //    }

            //    proxy.UpdateStatusFromPilot(this.FlightNumber, status);
            //    DoAbort(nextStatus);

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // to remove the airplane object and callBack from the service.
            this.proxy.Destroy(this.FlightNumber);
        }
    }
}
