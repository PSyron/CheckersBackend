using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Checkers.Models;
using System.Data;
namespace Checkers.App_Data
{
    /// <summary>
    /// Tranzakcje z baza danych.
    /// Umieszczamy tutaj wszystkie table adaptery i metody zwiazane z wymiana/aktualizacja/usuwaniem/dodawaniem danych do db.
    /// </summary>
    static class DBControler
    {
        static App_Data.SQL_DATASETTableAdapters.TableAdapterManager tAM = new App_Data.SQL_DATASETTableAdapters.TableAdapterManager();
        static App_Data.SQL_DATASETTableAdapters.tUsersTableAdapter Uta = new App_Data.SQL_DATASETTableAdapters.tUsersTableAdapter();

        //Sprawdzenie prawidlowosci sesji
        public static mUser logIn(String session)
        {
            if (Uta.CheckSession(session) == null || (int)Uta.CheckSession(session) == 0) return null;
            mUser logedIn = new mUser(session, (int)Uta.SessionUserId(session));
            logedIn.authorize();
            Uta.NewSession(DBNull.Value.ToString(), false, logedIn.getId());
            return logedIn;
        }

        public static mUser logOff(String session)
        {
            //if (Uta.CheckSession(session) == null || (int)Uta.CheckSession(session) == 0) return null;
            if ((int)Uta.CheckSession(session) == 0) return null;
            mUser logedIn = new mUser("", (int)Uta.SessionUserId(session));
            logedIn.unauthorize();
            return logedIn;
        }
        //Logowanie, uzyskanie nowej sesji
        public static mUser logIn(String login, String password)
        {
            var userid = Uta.UserLogIn(login, password);
            if (userid == null) return null;
            mUser user = new mUser(login, password, (int)userid);
            user.authorize();
            String guid;
            do
            {
                guid = Guid.NewGuid() + "";
            }
            while ((int)Uta.CheckSession(guid) != 0);
            user.setSession(guid);
            //, getAccessDate(DateTime.Now)
            Uta.NewSession(guid, true,(int)userid);            
            return user;
        }
        //Rejestracja uzytkownika
        public static mUser register(String name, String surname, String login, String password)
        {
            //Mozna dodac jeszcze jakies warunki dotyczace hasla
            int userid = -1;
            if((int)Uta.LoginExists(login)==1) return null;
            mUser newUser=new mUser(name, surname, login, password);
            newUser.authorize();
            String guid;
            do
            {
                guid = Guid.NewGuid() + "";
            }
            while ((int)Uta.CheckSession(guid) != 0);
           
            newUser.setSession(guid);
            //userid = (int)Uta.NewUser(name, login, password, guid);
            //,getAccessDate(DateTime.Now)
            Uta.NewSession(guid,true, userid);
            return newUser;
        }
        public static String czas()
        {

            return getAccessDate(DateTime.Now).ToString();
        }
        private static DateTime getAccessDate(DateTime d)
        {
            return new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second);
        }



     
        }
}