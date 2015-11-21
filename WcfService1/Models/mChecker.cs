using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Checkers.Models
{
    /// <summary>
    /// Model odpowiedzialny za stan planszy(pozycje pionkow).
    /// </summary>
    public class mChecker
    {
        int checkerId;
        Dictionary<int, mPawn> checkers = new Dictionary<int, mPawn>();
        int maxWidth=8;
        int maxHeight=8;


        public mChecker(int checkerId)
        {
            this.checkerId = checkerId;
            generateChecker();
        }

        private void generateChecker()
        {
            checkers.Add(1, new mPawn(1, 2, 1, 1));
            checkers.Add(2, new mPawn(2, 4, 1, 1));
            checkers.Add(3, new mPawn(3, 6, 1, 1));
            checkers.Add(4, new mPawn(4, 8, 1, 1));

            checkers.Add(5, new mPawn(5, 1, 2, 1));
            checkers.Add(6, new mPawn(6, 3, 2, 1));
            checkers.Add(7, new mPawn(7, 5, 2, 1));
            checkers.Add(8, new mPawn(8, 7, 2, 1));

            checkers.Add(9, new mPawn(9, 2, 3, 1));
            checkers.Add(10, new mPawn(10, 4, 3, 1));
            checkers.Add(11, new mPawn(11, 6, 3, 1));
            checkers.Add(12, new mPawn(12, 8, 3, 1));

            checkers.Add(13, new mPawn(13, 1, 6, 2));
            checkers.Add(14, new mPawn(14, 3, 6, 2));
            checkers.Add(15, new mPawn(15, 5, 6, 2));
            checkers.Add(16, new mPawn(16, 7, 6, 2));

            checkers.Add(17, new mPawn(17, 2, 7, 2));
            checkers.Add(18, new mPawn(18, 4, 7, 2));
            checkers.Add(19, new mPawn(19, 6, 7, 2));
            checkers.Add(20, new mPawn(20, 8, 7, 2));

            checkers.Add(21, new mPawn(21, 1, 8, 2));
            checkers.Add(22, new mPawn(22, 3, 8, 2));
            checkers.Add(23, new mPawn(23, 5, 8, 2));
            checkers.Add(24, new mPawn(24, 7, 8, 2));

        }
        private void addChecker(mPawn pawn){
            checkers.Add(pawn.getId(), pawn);
        }

        public int getId()
        {
            return checkerId;
        }
        public mPawn getPawn(int idPawn)
        {
            return checkers[idPawn];
        }
    }
}