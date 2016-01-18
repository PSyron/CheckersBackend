using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Checkers.Responses
{
    public class GameMoveReponse
    {

        public int idGame;
        public int idPlayer;
        public int PawnPreX;
        public int PawnPreY;
        public int PawnPostX;
        public int PawnPostY;
        public Boolean hasNextMove; //czy po tym ruchu, jest nastepny?

    }
}