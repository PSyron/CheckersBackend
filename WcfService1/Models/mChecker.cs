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
        int CheckerId;
        List<mPawn> checkers;
        int maxWidth=10;
        int maxHeight=10;
    }
}