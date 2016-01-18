using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Checkers.Models
{
    /// <summary>
    /// Model odzwierciedlajacy ruch gracza. Wskazuje na 
    /// </summary>
    public class mMove
    {
        public int idGame;
        public int idPlayer;
        public mPawn PawnPre; //stan pionka przed
        public mPawn PawnPost; //stan pionka po
        public mPawn PawnDown; //zbity pionek w ruchu
        public Boolean hasNextMove; //czy po tym ruchu, jest nastepny?
        public Boolean moveAllowed; //tryb rozgrywki, czy mozna wykonac taki ruch, czy nie
        
        public mMove(int idGame, int idPlayer, mPawn Pawn, int postColumn, int postRow)
        {
            this.idGame = idGame;
            this.idPlayer = idPlayer;
            PawnPre=Pawn;
            PawnPost=new mPawn(PawnPre.getId(),postColumn,postRow,PawnPre.getColor());
        }

        public String checkMove()
        {
            return "Move successful";
        }
    }
}