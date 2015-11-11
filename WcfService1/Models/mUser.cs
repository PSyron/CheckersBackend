using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Checkers.Models
{
    /// <summary>
    /// Model uzytkownika.
    /// </summary>
    public class mUser
    {

        int userID=-1;
        String login;
        String name;
        String surname;
        String password;
        String session = "";
        Boolean authorized=false;
        int ELO = 1000; //ranking punktowy
        
        public mUser(String session, int userID)
        {
            this.session = session;
            this.userID = userID;
        }
        public mUser(String login, String password, int userID)
        {
            this.login = login;
            this.password = password;
            this.userID = userID;
        }
        public mUser(String name, String surname, String login, String password)
        {
            this.name = name;
            this.surname = surname;
            this.login = login;
            this.password = password;
        }
        public void authorize() { authorized = true; }
        public void unauthorize() { authorized = false; }
        public Boolean isAuthorized() { return authorized; }
        public void setSession(String session) { this.session = session; }
        public String getSession() { return session; }
        public int getId() { return userID; }



    }
}