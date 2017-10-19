using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.ComponentModel;
using System.Reflection;

namespace ATCServer
{

    public enum AStatus
    {
        [Description("On the ground")]
        On_the_ground,
        [Description("On land")]
        On_land,
        [Description("In the air")]
        In_the_air,
        [Description("In air")]
        In_air,
        [Description("Req landing")]
        Req_landing,
        [Description("Landing")]
        Landing,
        [Description("Req takeoff")]
        Req_takeoff,
        [Description("Taking off")]
        Taking_Off,
        [Description("Emergency")]
        Emergency,
        [Description("Emergency landing")]
        Emergency_landing,
        [Description("Awaiting take off approval")]
        Wait_takeoff,
        [Description("Awaiting emergency landing approval")]
        Wait_emergency,
        [Description("Awaiting landing approval")]
        Wait_landing,
        [Description("Docked")]
        Docked,
        [Description("Abort takeoff!")]
        Abort_takeoff,
        [Description("Abort landing!")]
        Abort_landing,
        [Description("Abort taxi!")]
        Abort_taxi,
        [Description("taxi to R1")]
        Taxi_R1,
        [Description("taxi to R2")]
        Taxi_R2,
        [Description("taxi to R3")]
        Taxi_R3,
        [Description("null")]
        Null

    }

    public enum Command
    {
        [Description("land")]
        Land,
        [Description("takeoff")]
        TakeOff,
        [Description("abort")]
        Abort,
        [Description("taxi")]
        Taxi
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ATC_Contract : IController, IAirplane
    {
        //properties and variables
        private List<string> commands;
        public string AirportName;
        public IControllerCallback controllerCallback = null;
        public IAirplaneCallback airplaneCallback;

        public List<Airplane> airplanes; //List of airplane objects
        public List<IAirplaneCallback> airplaneCallbacks;
        public List<Runway> listOfRunways;

        public int UpdatePosition;

        //for docking
        List<int> listOfTimes;


        Queue<int> indexes;

        private delegate void MessageEvent(String flightNumber);
        private MessageEvent messageSendEvent; // delegate message when pilot sends the request 

        private MessageEvent messageAddToList;
        private MessageEvent messageAbort;
        private MessageEvent messageRemove;
        private MessageEvent messageDocked;
        private MessageEvent messageStandardUpdate;

        public Dictionary<AStatus, string> statusDict; //correspondence between status enum and string value
        public Dictionary<Command, string> commandDict; //correspondence between command enum and string value

        //for landing updates
        public List<String> eventList;
        //public delegate void landingUpdateEvent(string flight, int progress);
        //public landingUpdateEvent atLandingUpdateEvent;
        public ATC_Contract()
        {
            this.listOfTimes = new List<int>();

            this.indexes = new Queue<int>();
            this.AirportName = "Fontys Airport Eindhoven";
            this.airplanes = new List<Airplane>();
            this.airplaneCallbacks = new List<IAirplaneCallback>();

            this.listOfRunways = new List<Runway>();
            this.listOfRunways.Add(new Runway("R1"));
            this.listOfRunways.Add(new Runway("R2"));
            this.listOfRunways.Add(new Runway("R3"));
            this.listOfRunways.Add(new Runway("R4"));

            this.UpdatePosition = -1;

            //populating dictionary
            statusDict = new Dictionary<AStatus, string>();
            commandDict = new Dictionary<Command, string>();

            foreach (AStatus s in Enum.GetValues(typeof(AStatus)))
                statusDict.Add(s, GetEnumDescriptionServer(s));
            foreach (Command s in Enum.GetValues(typeof(Command)))
                commandDict.Add(s, GetEnumDescriptionServer(s));

            eventList = new List<string>();

            commands = new List<string>();
        }

        // ----- Methods form IAirplane Interface
        public void Connect(string FlightNumber, int fuel_level, AStatus status)
        {
            this.airplaneCallback = OperationContext.Current.GetCallbackChannel<IAirplaneCallback>();
            this.airplaneCallbacks.Add(this.airplaneCallback);
            airplanes.Add(new Airplane(FlightNumber, fuel_level, status));
            //if it is on land at startup of the application, the docking process is started
            //if (status == AStatus.On_the_ground) LetPilotKnow(FlightNumber, status);
        }


        public void TakeOffRequest(string FlightNumber)
        {

            Airplane a = this.GetAirplane(FlightNumber);
            if (a.Status == AStatus.On_land || a.Status == AStatus.On_the_ground)
            {
                a.TakeOff();

                messageAddToList(FlightNumber);
            }
        }


        public void LandRequest(string FlightNumber)
        {
            Airplane a = this.GetAirplane(FlightNumber);
            System.Diagnostics.Debug.WriteLine(a.Status.ToString());
            if (a.Status == AStatus.In_air || a.Status == AStatus.In_the_air)
            {
                a.reqLand();
                //messageSendEvent(FlightNumber);// fires event

                messageAddToList(FlightNumber);
            }

        }



        public void CancelRequest(string FlightNumber)
        {
            Airplane a = this.GetAirplane(FlightNumber);
            if (a.Status == AStatus.Req_landing
                || a.Status == AStatus.Req_takeoff
                || a.Status == AStatus.Emergency)
            {
                a.Status = AStatus.Null;
                //messageSendEvent(FlightNumber);// fires event

                messageRemove(FlightNumber);
            }
        }

        public void UpdatePlane(string FlightNumber, int fuel_level)
        {
            int counter = -1; /*this is to give the position in the list and return this position to the controller.
                                So that when update the listbox it dont need to loop it will just change the item in the listbox directly.
                                 Reason: It makes it faster.   -Jose*/
            if (airplanes.Count > 0)
            {
                foreach (Airplane c in airplanes)
                {
                    counter++;
                    if (c.FlightNumber == FlightNumber)
                    {
                        c.FuelLevel = fuel_level;
                        break;
                    }
                }
            }
            UpdatePosition = counter;

        }


        // ----- Methods form IController Interface
        public string GetAirportName()
        {
            return this.AirportName;
        }


        public List<Airplane> GetAirplaneList()
        {
            return this.airplanes;
        }

        //Sends the position to the controller -Jose
        public int GetUpdatePosition()
        {
            return this.UpdatePosition;
        }
        //Reset the position -Jose
        public void ResetUpdatePosition()
        {
            this.UpdatePosition = -1;
        }

        public Airplane GetAirplane(string FlightNumber)
        {
            foreach (Airplane a in airplanes)
            {
                if (a.FlightNumber == FlightNumber)
                    return a;
            }
            return null;
        }


        public void ApproveLandRequest(string FlightNumber, string RunwayID)
        {
            Airplane aux = GetAirplane(FlightNumber);
            //check if the runway isnt taken
            if (isRunwayFree(RunwayID))
            { 
                    if (GetAirplane(FlightNumber).Land(RunwayID, this))
                    {
                        controllerCallback.UpdateForLandingProcess(FlightNumber, GetAirplane(FlightNumber).Status, RunwayID);
                        foreach (IAirplaneCallback a in airplaneCallbacks)
                        {
                            if (a.getFlightNr().Equals(FlightNumber))
                                a.RequestConfirm(RunwayID);
                        }
                    commands.Add("The flight Number " + FlightNumber + " on " + RunwayID + " land");
                }
                    else controllerCallback.SendErrorMessage("Command not allowed");
                
            }
            else controllerCallback.SendErrorMessage("Runway " + RunwayID + " is taken.");

        }

        public void ApproveTakeOffRequest(string FlightNumber, string RunwayID)
        {
            try
            {
                Airplane a = GetAirplane(FlightNumber);
                if (a.Runway.RunwayID == RunwayID)
                {
                    if (a.TakeOff(RunwayID, this))
                    {
                        controllerCallback.UpdateForTakingOffProcess(FlightNumber, AStatus.Taking_Off, RunwayID);
                        foreach (IAirplaneCallback ac in airplaneCallbacks)
                        {
                            if (ac.getFlightNr().Equals(FlightNumber))
                            {
                                ac.RequestConfirm(RunwayID);
                            }
                        }
                        commands.Add("The flight Number " + FlightNumber + " on " + RunwayID + " takeoff");
                    }
                    else controllerCallback.SendErrorMessage("Command not allowed");
                }else controllerCallback.SendErrorMessage("Cannot takeoff from this Runway.");
            }
            catch (Exception)
            {

            }
        }

        public bool isRunwayFree(string RunwayId)
        {
            foreach (Runway r in listOfRunways)
            {
                if (r.RunwayID == RunwayId)
                    if (r.Status == true) return true;
            }
            return false;
        }


        public void sendTaxiCommand(string FlightNumber, string RunwayID)
        {

            Airplane a = GetAirplane(FlightNumber);
            //check if the runway is free or occupied by current flight
            if (isRunwayFree(RunwayID))
            {
                    if (a.Taxi(RunwayID, this))
                    {
                        controllerCallback.UpdateForTaxi(a.FlightNumber, a.Status, RunwayID, true);
                        foreach (IAirplaneCallback iac in airplaneCallbacks)
                        {
                            if (iac.getFlightNr() == FlightNumber)
                            {
                                iac.StatusChangedPilot(FlightNumber, a.Status);
                            }

                        }
                    commands.Add("The flight Number " + FlightNumber + " on " + RunwayID + " taxi");
                }
                
            }
            else { controllerCallback.SendErrorMessage("Runway " + RunwayID + " is taken."); }

        }

        public void AbortInTheServer(string FlightNumber, string RunwayID)
        {
            foreach (IAirplaneCallback ac in airplaneCallbacks)
            {
                if (ac.getFlightNr() == FlightNumber)
                {
                    if (ac.canAbort())
                    {
                        Airplane a = GetAirplane(FlightNumber);
                        a.Abort();
                        ac.StatusChangedPilot(FlightNumber, a.Status);
                        controllerCallback.AbortedCommand(FlightNumber, statusDict[a.Status]);

                    }
                    else controllerCallback.SendErrorMessage("Cannot abort this flight!");
                }
               
            }
            commands.Add("The flight Number " + FlightNumber + " on " + RunwayID + " abort");
        }

        public void SubscribeEvent()
        {
            this.controllerCallback = OperationContext.Current.GetCallbackChannel<IControllerCallback>();
            //
            messageSendEvent += this.controllerCallback.NewPilotRequest;
            //

            messageAddToList += this.controllerCallback.AddToListMessage;
            messageAbort += this.controllerCallback.AbortMessage;
            messageRemove += this.controllerCallback.RemoveMessage;
            messageDocked += this.controllerCallback.DockedMessage;
            messageStandardUpdate += this.controllerCallback.RegularUpdate;
        }

        public void UnSubscribeEvent()
        {
            if (this.controllerCallback != null)
            {
                //
                messageSendEvent -= this.controllerCallback.NewPilotRequest;
                //

                messageAddToList -= this.controllerCallback.AddToListMessage;
                messageAbort -= this.controllerCallback.AbortMessage;
                messageRemove -= this.controllerCallback.RemoveMessage;
                messageDocked -= this.controllerCallback.DockedMessage;
                messageStandardUpdate -= this.controllerCallback.RegularUpdate;

                this.controllerCallback = null;
            }
        }

        //the queue may be removable
        public void UpdateStatusFromPilot(string flightNumber, AStatus status)
        {
            AStatus previous = this.GetAirplane(flightNumber).Status;

            for (int i = 0; i < airplanes.Count; i++)
                if (airplanes[i].FlightNumber == flightNumber)
                {
                    airplanes[i].Status = status;

                    //a little case distinction
                    if (status == AStatus.On_land)
                    {
                        //it adds the plane to the list if it was Docked before
                        if (previous == AStatus.Docked) messageAddToList(flightNumber);
                        else {
                            messageStandardUpdate(flightNumber);
                        }
                        
                    }
                    if (status == AStatus.In_air)
                    {
                        messageRemove(flightNumber); // removes from the List View
                        airplanes.Remove(GetAirplane(flightNumber)); // removes from the List from the server.
                    }
                    if (status == AStatus.Docked)
                    {
                        messageDocked(flightNumber);
                        airplanes.Remove(GetAirplane(flightNumber)); // removes from the List from the server.
                    }

                    i = airplanes.Count;

                }

        }

        public void UpdateStatusFromController(string flightNumber, AStatus status)
        {
            for (int i = 0; i < airplanes.Count; i++)
                if (airplanes[i].FlightNumber == flightNumber)
                {
                    airplanes[i].Status = status;
                    indexes.Enqueue(i);
                    i = airplanes.Count;
                }
        }

        //when animation is done, this function is called from the controller
        //it checks if the status is on land 
        //and calls a method which waits for a few seconds before calling this method again with status docked
        //the controller is also notified of the docked status
        public void LetPilotKnow(string flight, AStatus status)
        {
            Thread temp;
            foreach (IAirplaneCallback a in airplaneCallbacks)
            {
                if (a.getFlightNr().Equals(flight))
                {
                    a.StatusChangedPilot(flight, status);
                }
            }
            //time to pass till the plane gets docked
            if (status == AStatus.On_land)
            {
                temp = new Thread(() => timePasses(flight));
                temp.Start();
            }
            if (status == AStatus.Docked)
            {
                this.UpdateStatusFromPilot(flight, status);
                this.airplanes.Remove(GetAirplane(flight)); // remove from the list when docked
            }
            
        }

        public void timePasses(string flight)
        {
            Thread.Sleep(6000);
            //if the status changed from on land to request waiting, docking process does not complete
            Airplane a = this.GetAirplane(flight);
            if(a.Status == AStatus.On_land
                || a.Status ==  AStatus.On_the_ground) LetPilotKnow(flight, AStatus.Docked);
        }


        public Queue<int> GetQueue()
        {
            Queue<int> aux = indexes;
            indexes.Clear();
            return aux;
        }

        public void EmergencyRequest(string flightNumber)
        {
            Airplane a = GetAirplane(flightNumber);
            if (a.Status == AStatus.In_air 
                || a.Status == AStatus.In_the_air)
            {
                a.Status = AStatus.Emergency;
                //messageSendEvent(flightNumber);

                messageAddToList(flightNumber);
            }
        }


        //used only to populate the dictionary
        public string GetEnumDescriptionServer(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public Dictionary<AStatus,string> GetStatusDictionaryPilot()
        {
            return this.statusDict;
        }

        public Dictionary<AStatus, string> GetStatusDictionaryController()
        {
            return this.statusDict;
        }

        public Dictionary<Command, string> GetCommandDictionary()
        {
            return this.commandDict;
        }


        public void reportProgress(string flight, int progress)
        {
            //free the Runway if the animation ends at this call
            if (progress == 10)
            {
                Airplane a = this.GetAirplane(flight);
                a.Runway.Status = true;
            }

            //updates controller interface  
            try
            {
                controllerCallback.UserInterface(flight, progress);
            }
            catch (Exception)
            {

            }
        }
        public Runway getRunway(string runwayId)
        {
            foreach(Runway r in listOfRunways)
            { if (r.RunwayID == runwayId) return r;}
            return null;
        }



        public void AbortComplete(string flight, AStatus status)
        {
            Airplane a = GetAirplane(flight);
            a.Status = status;
            
            controllerCallback.AbortFinished(flight, status);
        }

        public void Destroy(string flight)
        {
            Airplane airplane = null;
            
            airplane = GetAirplane(flight);
            if (airplane != null)
            {
                this.airplanes.Remove(airplane);
                this.controllerCallback.RemoveFromListView(flight);
            }
            
        }

        public List<string> getCommandList()
        {
            return commands;
        }
    }
}
