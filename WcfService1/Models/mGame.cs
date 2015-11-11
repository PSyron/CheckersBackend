using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Checkers.Models
{
    /// <summary>
    /// Model odpowiedzialny za odzwierciedlanie gier pomiedzy graczami i ich wynikow. 
    /// Trzeba zastanowic sie czy beda jakies tryby gier.
    /// </summary>
    public class mGame
    {
        int idGame;
        int idTable;
        int idPlayer1;
        int idPlayer2;
        int player1Points=0;
        int player2Points=0;
        int Scorelimit = 2;
        

        public mGame(int idGame, int idTable, int idPlayer1, int idPlayer2)
        {
            this.idGame = idGame;
            this.idTable = idTable;
            this.idPlayer1 = idPlayer1;
            this.idPlayer2 = idPlayer2;
        }

        public String playerWin(int idPlayer)
        {
            String message = "";
            if (idPlayer == idPlayer1) {
                player1Points++;
                message+="Player 1 wins the round!";
            }
            else if (idPlayer == idPlayer2) { 
                player2Points++;
                message += "Player 2 wins the round!";
            }
            else return "This player doesn't belong to this game!";
            if (player1Points == Scorelimit) message += " Game ends. Player 1 wins the match!";
            else if (player2Points == Scorelimit) message += " Game ends. Player 2 wins the match!";
            else message += " The game continues!";
            return message;
        }

        public void changeScoreLimit(int Scorelimit)
        {
            this.Scorelimit = Scorelimit;
        }


    }
}