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
        int idGame;
        int idPawnMoved; //pionek ruszony
        int idPawnOut; //pionek zbity(o ile jest)
        int preColumn;
        int postColumn;
        int preRow;
        int postRow;
        
        public mLog(int idTable, int idGame, int preColumn, int postColumn, int preRow, int postRow, int idPawnOut, int idPawnMoved)
        {
            this.idTable = idTable;
            this.idGame = idGame;
            this.preColumn = preColumn;
            this.postColumn = postColumn;
            this.preRow = preRow;
            this.postRow = postRow;
        }

        public void saveLog()
        {

        }
    }
}