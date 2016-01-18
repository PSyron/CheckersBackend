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

        //http://156.17.130.217/Pz/Services/Game.svc/getGames/67c90b8a-8b68-4e1c-bd8e-c5a74d455b7c
        [OperationContract]
        [WebGet(UriTemplate = "getGames/{sessionToken}", ResponseFormat = WebMessageFormat.Json)]
        GamesResponse getGames(String sessionToken);


        //http://156.17.130.217/Pz/Services/Game.svc/getLastMoves/67c90b8a-8b68-4e1c-bd8e-c5a74d455b7c/2/1
        [OperationContract]
        [WebGet(UriTemplate = "getLastMoves/{sessionToken}/{idGame}/{idLastMove}", ResponseFormat = WebMessageFormat.Json)]
        MovesResponse getLastMoves(String sessionToken, String idGame, String idLastMove);

        //http://156.17.130.217/Pz/Services/Game.svc/movePawn/67c90b8a-8b68-4e1c-bd8e-c5a74d455b7c/2/1/3/2/4
        [OperationContract]
        [WebGet(UriTemplate = "movePawn/{sessionToken}/{idGame}/{preX}/{preY}/{postX}/{postY}", ResponseFormat = WebMessageFormat.Json)]
        GameResponse movePawn(String sessionToken, String idGame, String preX, String preY, String postX, String postY);

        //http://156.17.130.217/Pz/Services/Game.svc/finishMove/67c90b8a-8b68-4e1c-bd8e-c5a74d455b7c/2
        [OperationContract]
        [WebGet(UriTemplate = "finishMove/{sessionToken}/{idGame}", ResponseFormat = WebMessageFormat.Json)]
        GameResponse finishMove(String sessionToken, String idGame);
    }

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

    [DataContract]
    public class MovesResponse
    {
        [DataMember]
        public String Session { get; set; }
        [DataMember]
        public Boolean Successful { get; set; }
        [DataMember]
        public List<mLog> Moves { get; set; }
    }


}
