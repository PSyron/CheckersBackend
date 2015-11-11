using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Checkers.Models
{
    /// <summary>
    /// Model pionka
    /// </summary>
    public class mPawn
    {
        int idPawn;
        int column;
        int row;
        int idColor;


        public mPawn(int idPawn, int column, int row, int idColor)
        {
            this.idPawn = idPawn;
            this.column = column;
            this.row = row;
            this.idColor = idColor;
        }
        public int getId()
        {
            return idPawn;
        }

        public int getColumn()
        {
            return column;
        }
        public int getRow()
        {
            return row;
        }

        public int getColor()
        {
            return idColor;
        }
        public void movePawn(int columnPost, int rowPost)
        {
            column = columnPost;
            row = rowPost;
        }
    }
}