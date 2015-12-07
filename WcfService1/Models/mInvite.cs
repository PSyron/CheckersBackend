using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Checkers.Models
{
    public class mInvite
    {

        DateTime timestamp;
        int idGame;
        String playerName;
        public mInvite();

        public mInvite(DateTime timestamp, int idGame)
        {
            this.idGame = idGame;
            this.timestamp = timestamp;
        }
        public mInvite(DateTime timestamp, int idGame, String playerName)
        {
            this.idGame = idGame;
            this.timestamp = timestamp;
            this.playerName = playerName;
        }
        public DateTime getTime(){return timestamp;}

        public int getIdGame(){return idGame;}

    }
}