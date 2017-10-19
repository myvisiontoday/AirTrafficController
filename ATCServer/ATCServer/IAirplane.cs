using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ATCServer
{
    [ServiceContract(Namespace = "ATCServer", CallbackContract = typeof(IAirplaneCallback))]
    public interface IAirplane
    {
        [OperationContract(IsOneWay = true)]
        void Connect(string FlightNumber, int fuel_level, AStatus status);

        [OperationContract]
        void LandRequest(string FlightNumber);

        [OperationContract]
        void TakeOffRequest(string FlightNumber);

        [OperationContract]
        void CancelRequest(string FlightNumber);

        [OperationContract]
        void UpdatePlane(string FlightNumber, int fuel_level);

        [OperationContract]
        void UpdateStatusFromPilot(string flightNumber, AStatus status);

        [OperationContract]
        void EmergencyRequest(string flightNumber);

        //[OperationContract]
        //string GetEnumDescriptionPilot(Enum value);

        [OperationContract]
        Dictionary<AStatus, string> GetStatusDictionaryPilot();

        [OperationContract]
        void reportProgress(string flight, int progress);

        [OperationContract]
        void AbortComplete(string flight, AStatus status);

        [OperationContract(IsOneWay = true)]
        void Destroy(string flight);

    }
}
