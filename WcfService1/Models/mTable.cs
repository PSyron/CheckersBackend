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

        List<mUser> players;
        List<mUser> spectators;
    }
}