using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Checkers.Models
{
    /// <summary>
    /// Klasa odpowiedzialna za model wpisu dziennika ruchów.
    /// </summary>
    public class mLog
    {
        int idTable;
        public int idGame;
        public int idMove;
        int idPawnMoved; //pionek ruszony
        int idPawnOut; //pionek zbity(o ile jest)
        public int preColumn;
        public int postColumn;
        public int preRow;
        public int postRow;
        
        public mLog(int idTable, int idGame, int idMove, int preColumn, int postColumn, int preRow, int postRow, int idPawnOut, int idPawnMoved)
        {
            this.idTable = idTable;
            this.idGame = idGame;
            this.idMove = idMove;
            this.preColumn = preColumn;
            this.postColumn = postColumn;
            this.preRow = preRow;
            this.postRow = postRow;
            this.idPawnOut = idPawnOut;
            this.idPawnMoved = idPawnMoved;
        }

        public mLog()
        {
        }

        public void saveLog()
        {

        }
    }
}