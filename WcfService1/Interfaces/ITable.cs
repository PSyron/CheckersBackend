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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITable" in both code and config file together.
    [ServiceContract]
    public interface ITable
    {
        //http://156.17.130.217/Pz/Services/Table.svc/createTable/8a502001-7291-4b8b-9ff1-d5617be4943e
        [OperationContract]
        [WebGet(UriTemplate = "createTable/{sessionToken}", ResponseFormat = WebMessageFormat.Json)]
        TableResponse createTable(String sessionToken);

        //http://156.17.130.217/Pz/Services/Table.svc/invitePlayer/67c90b8a-8b68-4e1c-bd8e-c5a74d455b7c/Nietoperek/4
        [OperationContract]
        [WebGet(UriTemplate = "invitePlayer/{sessionToken}/{friendName}/{idGame}", ResponseFormat = WebMessageFormat.Json)]
        TableResponse invitePlayer(String sessionToken, String friendName, String idGame);

        [OperationContract]
        [WebGet(UriTemplate = "revokeInvitation/{sessionToken}/{friendName}/{idGame}", ResponseFormat = WebMessageFormat.Json)]
        TableResponse revokeInvitation(String sessionToken, String friendName, String idGame);

        [OperationContract]
        [WebGet(UriTemplate = "refuseInvitation/{sessionToken}/{idGame}", ResponseFormat = WebMessageFormat.Json)]
        TableResponse refuseInvitation(String sessionToken, String idGame);

        //http://localhost:13622/Services/Table.svc/acceptInvite/67c90b8a-8b68-4e1c-bd8e-c5a74d455b7c/7
        [OperationContract]
        [WebGet(UriTemplate = "acceptInvite/{sessionToken}/{idGame}", ResponseFormat = WebMessageFormat.Json)]
        TableResponse acceptInvitation(String sessionToken, String idGame);

        //http://156.17.130.217/Pz/Services/Table.svc/getInvitations/3781fb33-8883-40c1-82db-929d18f7d84b
        [OperationContract]
        [WebGet(UriTemplate = "getInvitations/{sessionToken}", ResponseFormat = WebMessageFormat.Json)]
        InvitationsResponse getInvitations(String sessionToken);

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
    public class InvitationsResponse
    {
        [DataMember]
        public String Session { get; set; }
        [DataMember]
        public Boolean Successful { get; set; }
        [DataMember]
        public List<mInvite> Invites { get; set; }
    }
}
