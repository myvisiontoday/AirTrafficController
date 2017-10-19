using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ATCServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(Namespace = "ATCServer", CallbackContract = typeof(IControllerCallback))]
    public interface IController
    {
        [OperationContract]
        string GetAirportName();

        [OperationContract]
        List<Airplane> GetAirplaneList();

        [OperationContract]
        Airplane GetAirplane(string FlightNumber);

        [OperationContract]
        void sendTaxiCommand(string FlightNumber, string RunwayID);

        [OperationContract]
        void ApproveTakeOffRequest(string FlightNumber, string RunwayID);

        [OperationContract]
        void ApproveLandRequest(string FlightNumber, string RunwayID);

        [OperationContract]
        void AbortInTheServer(string FlightNumber, string RunwayID);

        [OperationContract]
        int GetUpdatePosition();

        [OperationContract]
        void ResetUpdatePosition();

        [OperationContract]
        void SubscribeEvent();

        [OperationContract]
        void UnSubscribeEvent();

        [OperationContract]
        void UpdateStatusFromController(string flightNumber, AStatus status);

        [OperationContract]
        Queue<int> GetQueue();

        [OperationContract]
        void LetPilotKnow(string flight, AStatus status);
        

        [OperationContract]
        Dictionary<AStatus, string> GetStatusDictionaryController();

        [OperationContract]
        Dictionary<Command, string> GetCommandDictionary();

        [OperationContract]
        Runway getRunway(string runwayId);

        [OperationContract]
        List<string> getCommandList();
    }
}
