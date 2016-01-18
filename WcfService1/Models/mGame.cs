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
        public int idGame;
        mChecker checker;
        int idPlayer1;
        int idPlayer2;
        public String Player1name;
        public String Player2name;
        int player1Points = 0;
        int player2Points = 0;
        int Scorelimit = 2;

        public mGame(int idGame, int idPlayer1, mChecker checker)
        {
            this.idGame = idGame;
            this.idPlayer1 = idPlayer1;
            this.checker = checker;
        }
        public mGame(int idGame, int idPlayer1, int idPlayer2)
        {
            this.idGame = idGame;
            this.idPlayer1 = idPlayer1;
            this.idPlayer2 = idPlayer2;
        }
        public mGame(int idGame, String Player1name, String Player2name)
        {
            this.idGame = idGame;
            this.Player1name = Player1name;
            this.Player2name = Player2name;
        }
        public mGame()
        {
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

        public int getId()
        {
            return idGame;
        }
        public String getPlayer1name()
        {
            return Player1name;
        }
        public String getPlayer2name()
        {
            return Player2name;
        }
        public void setPlayer2(int idPlayer2)
        {
            this.idPlayer2 = idPlayer2;
        }
    }
}