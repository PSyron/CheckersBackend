using Checkers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Checkers.Interfaces
{
    [ServiceContract]
    public interface ICommunity
    {
        //Dodac active przy connect
        //http://156.17.130.217/Pz/Services/Login.svc/checkActivePlayers/8a502001-7291-4b8b-9ff1-d5617be4943e
        [OperationContract]
        [WebGet(UriTemplate = "checkActivePlayers/{sessionToken}", ResponseFormat = WebMessageFormat.Json)]
        PlayersResponse checkActivePlayers(String sessionToken);

        [OperationContract]
        [WebGet(UriTemplate = "checkActiveFriends/{sessionToken}", ResponseFormat = WebMessageFormat.Json)]
        PlayersResponse checkActiveFriends(String sessionToken);

        [OperationContract]
        [WebGet(UriTemplate = "getFriends/{sessionToken}", ResponseFormat = WebMessageFormat.Json)]
        PlayersResponse getFriends(String sessionToken);
    }

    [DataContract]
    public class PlayersResponse
    {
        [DataMember]
        public String Session { get; set; }
        [DataMember]
        public List<mUser> Users { get; set; }
    }
}
