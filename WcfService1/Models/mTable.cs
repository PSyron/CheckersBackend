using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Checkers.Models
{
    /// <summary>
    /// Model pokoju/stolu. Lista graczy i widzow. 
    /// Do przemyslenia.
    /// </summary>
    public class mTable
    {
        int idTable;
        int idGame_;
        int idAdmin;
        List<mUser> players;
        List<mUser> spectators;

        public mTable(int idTable, int idGame_, int idAdmin)
        {
            this.idTable = idTable;
            this.idGame_ = idGame_;
            this.idAdmin = idAdmin;
        }
        public int getId()
        {
            return idTable;
        }
        public int getGameId()
        {
            return idGame_;
        }
        public int getAdminId()
        {
            return idAdmin;
        }

        public void newGame(int idGame)
        {
            idGame_ = idGame;
        }

    }

}