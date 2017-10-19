using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace ATCServer
{
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "ATCServer.ContractType".
    [DataContract]
    public class Airplane
    {
        //data about an airplane available in the contract
        [DataMember]
        public DateTime TimeLanded { get; set; }

        [DataMember]
        public int FuelLevel { get; set; }

        [DataMember]
        public string FlightNumber { get; set; }

        [DataMember]
        public AStatus Status { get; set; }

        [DataMember]
        public Runway Runway { get; set; }

        [DataMember]
        public int Id { get; set; }
        
        public static int IdAll { get; set; }
        
        //[DataMember]
        //public Point MyProperty { get; set; }


        public Airplane(string flightNumber, int fuelLevel, AStatus status)

        {
            this.FlightNumber = flightNumber;
            this.FuelLevel = fuelLevel;
            this.Status = status;

             IdAll += 1;
            this.Id = IdAll;

        }

        // taxi to the runway
        public bool Taxi(string RunwayID, ATC_Contract contract)
        {
            bool returnme = false;
            //check if command allowed: first check if the runway isnt taken
            if(isCommandAllowed("taxi"))
            {
                returnme = true;
                switch (RunwayID)
                {
                    case "R1":
                        this.Status = AStatus.Taxi_R1;
                        
                        break;

                    case "R2":
                        this.Status = AStatus.Taxi_R2;
                        break;

                    case "R3":
                        this.Status = AStatus.Taxi_R3;
                        break;
                }

                this.Runway = contract.getRunway(RunwayID);
                this.Runway.Status = false;
            }
            return returnme;
               
        }

        public bool TakeOff(string RunwayID, ATC_Contract contract)
        {
            bool returnme = false;
            if(isCommandAllowed("takeoff"))
            {
                Status = AStatus.Taking_Off;
                returnme = true;
            }
            return returnme;
        }

        public bool Land(string RunwayID, ATC_Contract contract)
        {
            if (isCommandAllowed("land"))
            {
                //if its waiting for emergency, we have a emergency landing process
                if (this.Status == AStatus.Emergency) this.Status = AStatus.Emergency_landing;
                else this.Status = AStatus.Landing;

                this.Runway = contract.getRunway(RunwayID);
                this.Runway.Status = false;
                return true; 
            }
            return false;
        }
        
        public void Abort()
        {
            if (this.Status == AStatus.Landing)
            {
                this.Status = AStatus.Abort_landing;
            }
            if (this.Status == AStatus.Taking_Off)
            {
                this.Status = AStatus.Abort_takeoff;
            }
            if (this.Status == AStatus.Taxi_R1
                || this.Status == AStatus.Taxi_R2
                || this.Status == AStatus.Taxi_R3)
            {
                this.Status = AStatus.Abort_taxi;
            }

            this.Runway.Status = true;
        }

        public void reqLand()
        {
            this.Status = AStatus.Req_landing;
        }

        public void TakeOff()
        {
            this.Status = AStatus.Req_takeoff;
        }

        public bool isCommandAllowed(string command)
        {
         bool returnme = false;
         switch(command)
         {
             case ("taxi"): if (this.Status == AStatus.Req_takeoff) returnme = true; break;
             case ("takeoff"): if (Status.Equals(AStatus.Taxi_R1) || Status.Equals(AStatus.Taxi_R2) || Status.Equals(AStatus.Taxi_R3)) returnme = true; break;
             case ("land"): if (Status.Equals(AStatus.Req_landing) || Status.Equals(AStatus.Emergency)) returnme = true; break;      
         }
         return returnme;
        }
        
       
    }
}
