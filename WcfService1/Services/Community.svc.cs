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
    /// Sprawdzenie aktywnych graczy, zaproszenia.
    /// </summary>

    public class Community : ICommunity
    {
        public PlayersResponse checkActivePlayers(String sessionToken)
        {
            Login LoginService= new Login();
            List<mUser> users=new List<mUser>();
            if(LoginService.session(sessionToken).Authorized==true)
            {
                users=DBControler.getActiveUsers();
            }
            else sessionToken="";

            return new PlayersResponse
            {
                Session=sessionToken,
                Users = userListToJson(users)
            };
        }

        public PlayersResponse checkActiveFriends(String sessionToken)
        {
            Login LoginService= new Login();
            List<mUser> users=new List<mUser>();
            if(LoginService.session(sessionToken).Authorized==true)
            {
                users=DBControler.getActiveFriends(sessionToken);
            }
            else sessionToken="";

            return new PlayersResponse
            {
                Session=sessionToken,
                Users = userListToJson(users)
            };
        }
        public PlayersResponse getFriends(String sessionToken)
        {
            Login LoginService= new Login();
            List<mUser> users=new List<mUser>();
            if(LoginService.session(sessionToken).Authorized==true)
            {
                 users=DBControler.getFriends(sessionToken);
            }
            else sessionToken="";
            return new PlayersResponse
            {
                Session=sessionToken,
                Users = userListToJson(users)
            };
        }
            private List<mUser> userListToJson(List<mUser> users)
        {
            List<mUser> usersJson = new List<mUser>();

            foreach (mUser u in users)
                usersJson.Add(new mUser
                {
                    name=u.name
                });
            return usersJson;
        }

            public FriendResponse addFriend(String sessionToken, String friendName)
            {
                Login LoginService = new Login();
                Boolean added = false;
                if (LoginService.session(sessionToken).Authorized == true)
                {
                    added = DBControler.addFriend(sessionToken, friendName);
                }
                else sessionToken = "";
                return new FriendResponse
                {
                    Session = sessionToken,
                    Successful = added
                };
            }

            public FriendResponse removeFriend(String sessionToken, String friendName)
            {
                Login LoginService = new Login();
                Boolean removed = false;
                if (LoginService.session(sessionToken).Authorized == true)
                {
                    removed = DBControler.removeFriend(sessionToken, friendName);
                }
                else sessionToken = "";
                return new FriendResponse
                {
                    Session = sessionToken,
                    Successful = removed
                };
            }

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
                    Session=sessionToken,
                    Successful=created,
                    Message=message
                };
            }

        }

}

