using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Checkers.Interfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITable" in both code and config file together.
    [ServiceContract]
    public interface ITable
    {
        //http://156.17.130.217/Pz/Services/Table.svc/createTable/8a502001-7291-4b8b-9ff1-d5617be4943e
        [OperationContract]
        [WebGet(UriTemplate = "createTable/{sessionToken}", ResponseFormat = WebMessageFormat.Json)]
        TableResponse createTable(String sessionToken);
        
        //http://156.17.130.217/Pz/Services/Table.svc/invitePlayer/8a502001-7291-4b8b-9ff1-d5617be4943e/Nietoperek/1
        [OperationContract]
        [WebGet(UriTemplate = "invitePlayer/{sessionToken}/{friendName}/{idGame}", ResponseFormat = WebMessageFormat.Json)]
        TableResponse invitePlayer(String sessionToken, String friendName, String idGame);

        [OperationContract]
        [WebGet(UriTemplate = "revokeInvitation/{sessionToken}/{friendName}/{idGame}", ResponseFormat = WebMessageFormat.Json)]
        TableResponse revokeInvitation(String sessionToken, String friendName, String idGame);


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

    [DataContract]
    public class PlayersResponse
    {
        [DataMember]
        public String Session { get; set; }
        [DataMember]
        public List<mInvite> Invites { get; set; }
    }
}
