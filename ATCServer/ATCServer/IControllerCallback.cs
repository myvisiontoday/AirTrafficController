using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ATCServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.

    public interface IControllerCallback
    {
   
        [OperationContract(IsOneWay =true)]
        void UserInterface(string flight, int progress);

        //messaging
        [OperationContract(IsOneWay = true)]
        void AddToListMessage(string flightNumber);

        [OperationContract(IsOneWay = true)]
        void AbortMessage(string flightNumber);

        [OperationContract(IsOneWay = true)]
        void RemoveMessage(string flightNumber);

        [OperationContract(IsOneWay = true)]
        void DockedMessage(string flightNumber);

        [OperationContract(IsOneWay = true)]
        void RegularUpdate(string flightNumber);

        //
        [OperationContract(IsOneWay = true)]
        void NewPilotRequest(String flightNumber);
        //

        [OperationContract(IsOneWay = true)]
        void AbortedCommand(String flightNumber, String status);

        [OperationContract(IsOneWay= true)]
        void UpdateForLandingProcess(string flight, AStatus status, string RunwayId);

        [OperationContract]
        void UpdateForTakingOffProcess(string flight, AStatus status, string runwId);

        [OperationContract]
        void UpdateForTaxi(string flight, AStatus status, string Runway, bool success);

        [OperationContract]
        void SendErrorMessage(string message);

        [OperationContract]
        void AbortFinished(string flight, AStatus status);

        [OperationContract(IsOneWay = true)]
        void RemoveFromListView(string flight);

    }
}
