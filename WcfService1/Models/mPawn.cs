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
        Boolean isQueen = false;
        Boolean inGame = true;
        int idChecker;
        public mPawn(int idPawn, int column, int row, int idColor)
        {
            this.idPawn = idPawn;
            this.column = column;
            this.row = row;
            this.idColor = idColor;
        }
        public mPawn(int idPawn, int column, int row, int idColor,int idChecker)
        {
            this.idPawn = idPawn;
            this.column = column;
            this.row = row;
            this.idColor = idColor;
            this.idChecker = idChecker;
        }
        public int getId()
        {
            return idPawn;
        }

        public int getColumn()
        {
            return column;
        }
        public int getidChecker()
        {
            return idChecker;
        }
        public int getRow()
        {
            return row;
        }

        public Boolean Queen()
        {
            return isQueen;
        }
        public void advanceToQueen()
        {
            isQueen = true;
        }
        public Boolean isInGame()
        {
            return inGame;
        }
        public void outOfGame()
        {
            inGame = false;
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