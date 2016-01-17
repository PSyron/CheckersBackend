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
    public interface IGame
    {
        //http://156.17.130.217/Pz/Services/Game.svc/newGame/8a502001-7291-4b8b-9ff1-d5617be4943e/1
        [OperationContract]
        [WebGet(UriTemplate = "newGame/{sessionToken}/{tableId}", ResponseFormat = WebMessageFormat.Json)]
        GameResponse newGame(String sessionToken, String tableId);

        //http://156.17.130.217/Pz/Services/Game.svc/getGames/8a502001-7291-4b8b-9ff1-d5617be4943e/1
        [OperationContract]
        [WebGet(UriTemplate = "getGames/{sessionToken}/{tableId}", ResponseFormat = WebMessageFormat.Json)]
        GamesResponse getGames(String sessionToken);

        [DataContract]
        public class GameResponse
        {
            [DataMember]
            public String Session { get; set; }
            [DataMember]
            public Boolean Successful { get; set; }
            [DataMember]
            public String Message { get; set; }
        }

        [DataContract]
        public class GamesResponse
        {
            [DataMember]
            public String Session { get; set; }
            [DataMember]
            public Boolean Successful { get; set; }
            [DataMember]
            public List<mGame> Games { get; set; }
        }


    }
}
