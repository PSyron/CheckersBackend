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
                    message = "" + table.getGameId();
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

        public TableResponse refuseInvitation(String sessionToken, String SidGame)
        {
            int idGame = Int16.Parse(SidGame);
            Login LoginService = new Login();
            Boolean refused = false;
            String message = "Invitation not found";
            if (LoginService.session(sessionToken).Authorized == true)
            {
                mUser userInv = DBControler.refuseInvite(sessionToken, idGame);
                if (userInv.getId() > 0)
                {
                    refused = true;
                    message = "" + idGame;
                }
            }
            else sessionToken = "";

            return new TableResponse
            {
                Session = sessionToken,
                Successful = refused,
                Message = message
            };
        }

        public TableResponse acceptInvitation(String sessionToken, String SidGame)
        {
            int idGame = Int16.Parse(SidGame);
            Login LoginService = new Login();
            Boolean accepted = false;
            String message = "Game not found";
            if (LoginService.session(sessionToken).Authorized == true)
            {
                mUser userInv = DBControler.acceptInvite(sessionToken, idGame);
                if (userInv.getId() > 0)
                {
                    accepted = true;
                    message = "" + idGame;
                }
                if (userInv.getId() == -3)
                {
                    accepted = false;
                    message = ""+idGame;
                }
                if (userInv.getId()==-5)
                {
                    accepted = false;
                    message = "" + idGame;
                }
            }
            else sessionToken = "";

            return new TableResponse
            {
                Session = sessionToken,
                Successful = accepted,
                Message = message
            };
        }
        //Poprawic responsa
        public InvitationsResponse getInvitations(String sessionToken)
        {
            Login LoginService = new Login();
            Boolean responded = false;
            List<mInvite> invites = new List<mInvite>();
            if (LoginService.session(sessionToken).Authorized == true)
            {
                invites = DBControler.getInvitations(sessionToken);
                responded = true;
            }
            else sessionToken = "";

            return new InvitationsResponse
            {
                Session = sessionToken,
                Successful = responded,
                Invites = invitesListToJson(invites)
            };
        }

        private List<mInvite> invitesListToJson(List<mInvite> invites)
        {
            List<mInvite> invitesJson = new List<mInvite>();

            foreach (mInvite i in invites)
                invitesJson.Add(new mInvite
                {
                    dateString= i.getTime().ToString(),
                    playerName = i.playerName,
                    idGame=i.getIdGame()

                });
            return invitesJson;
        }

        //TODO Zaakceptowanie invite


    }
}
