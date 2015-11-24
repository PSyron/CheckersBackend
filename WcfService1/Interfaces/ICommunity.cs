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
        //http://156.17.130.217/Pz/Services/Community.svc/checkActivePlayers/8a502001-7291-4b8b-9ff1-d5617be4943e
        [OperationContract]
        [WebGet(UriTemplate = "checkActivePlayers/{sessionToken}", ResponseFormat = WebMessageFormat.Json)]
        PlayersResponse checkActivePlayers(String sessionToken);

        //http://156.17.130.217/Pz/Services/Community.svc/checkActiveFriends/8a502001-7291-4b8b-9ff1-d5617be4943e
        [OperationContract]
        [WebGet(UriTemplate = "checkActiveFriends/{sessionToken}", ResponseFormat = WebMessageFormat.Json)]
        PlayersResponse checkActiveFriends(String sessionToken);

        //http://156.17.130.217/Pz/Services/Community.svc/getFriends/8a502001-7291-4b8b-9ff1-d5617be4943e
        [OperationContract]
        [WebGet(UriTemplate = "getFriends/{sessionToken}", ResponseFormat = WebMessageFormat.Json)]
        PlayersResponse getFriends(String sessionToken);


        [OperationContract]
        [WebGet(UriTemplate = "addFriend/{sessionToken}/{friendName}", ResponseFormat = WebMessageFormat.Json)]
        FriendResponse addFriend(String sessionToken, String friendName);

        [OperationContract]
        [WebGet(UriTemplate = "removeFriend/{sessionToken}/{friendName}", ResponseFormat = WebMessageFormat.Json)]
        FriendResponse removeFriend(String sessionToken, String friendName);

        [OperationContract]
        [WebGet(UriTemplate = "createTable/{sessionToken}", ResponseFormat = WebMessageFormat.Json)]
        TableResponse createTable(String sessionToken);
    }

    [DataContract]
    public class PlayersResponse
    {
        [DataMember]
        public String Session { get; set; }
        [DataMember]
        public List<mUser> Users { get; set; }
    }
    [DataContract]
    public class FriendResponse
    {
        [DataMember]
        public String Session { get; set; }
        [DataMember]
        public Boolean Successful { get; set; }
    }
    [DataContract]
    public class TableResponse
    {
        [DataMember]
        public String Session { get; set; }
        [DataMember]
        public Boolean Successful { get; set; }
        [DataMember]
        public String Message { get; set; }
    }
}
