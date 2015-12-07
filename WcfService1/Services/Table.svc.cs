using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Checkers.Interfaces;
using Checkers.Models;
using Checkers.App_Data;
namespace Checkers.Services
{
    /// <summary>
    /// Serwis odpowiedzialny za panowanie nad dolaczajacymi graczami/widzami.
    /// </summary>
    public class Table : ITable
    {
        public TableResponse createTable(String sessionToken)
        {
            Login LoginService = new Login();
            Boolean created = false;
            String message = "Failed to create";
            if (LoginService.session(sessionToken).Authorized == true)
            {
                mTable table = DBControler.newTable(sessionToken);
                if (table != null)
                {
                    message = "Created table with ID:" + table.getId();
                    created = true;
                }
            }
            return new TableResponse
            {
                Session = sessionToken,
                Successful = created,
                Message = message
            };
        }
        //TODO Obsluga komunikatow
        public TableResponse invitePlayer(String sessionToken, String friendName, String SidGame)
        {
            int idGame = Int16.Parse(SidGame);
            Login LoginService = new Login();
            Boolean invited = false;
            String message = "Failed to invite";
            if (LoginService.session(sessionToken).Authorized == true)
            {
                mUser userInv = DBControler.newInvite(sessionToken, friendName, idGame);
                if (userInv.getId() > 0)
                {
                    invited = true;
                    message = "Invited user with name:" + friendName;
                }
                else if (userInv.getId() == -4) message = "Invite already sent to:" + friendName;
                
            }
            else sessionToken = "";

            return new TableResponse
            {
                Session = sessionToken,
                Successful = invited,
                Message = message
            };
        }
        //TODO Obsluga komunikatow
        public TableResponse revokeInvitation(String sessionToken, String friendName, String SidGame)
        {
            int idGame = Int16.Parse(SidGame);
            Login LoginService = new Login();
            Boolean revoked = false;
            String message = "Failed to revoke";
            if (LoginService.session(sessionToken).Authorized == true)
            {
                mUser userInv = DBControler.removeInvite(sessionToken, friendName, idGame);
                if (userInv.getId() > 0) revoked = true;
                message = "Revoked invitation for user with name:" + friendName;
            }
            else sessionToken = "";

            return new TableResponse
            {
                Session = sessionToken,
                Successful = revoked,
                Message = message
            };
        }

        //TODO Zaakceptowanie invite


    }
}
