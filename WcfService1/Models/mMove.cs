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
        int idGame;
        int idPlayer;
        mPawn PawnPre; //stan pionka przed
        mPawn PawnPost; //stan pionka po
        mPawn PawnDown; //zbity pionek w ruchu
        Boolean hasNextMove; //czy po tym ruchu, jest nastepny?
        Boolean moveAllowed; //tryb rozgrywki, czy mozna wykonac taki ruch, czy nie
        
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