using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ATCServer
{

    public interface IAirplaneCallback
    {
        [OperationContract(IsOneWay = true)]
        void RequestConfirm(string RunwayId);

        [OperationContract(IsOneWay = true)]
        void StatusChangedPilot(string flightNumber, AStatus status);

        [OperationContract]
        string getFlightNr();

        [OperationContract]
        bool canAbort();

        [OperationContract(IsOneWay = true)]
        void Aborted(AStatus status);

        [OperationContract(IsOneWay =true)]
        void AfterAnimation();


    }
}
