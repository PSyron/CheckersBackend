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
        static SQL_DATASETTableAdapters.TableAdapterManager tAM = new SQL_DATASETTableAdapters.TableAdapterManager();
        static SQL_DATASETTableAdapters.tUsersTableAdapter Uta = new SQL_DATASETTableAdapters.tUsersTableAdapter();
        static SQL_DATASETTableAdapters.tUserFriendsTableAdapter UFta = new SQL_DATASETTableAdapters.tUserFriendsTableAdapter();
        
        //Sprawdzenie prawidlowosci sesji
        public static mUser logIn(String session)
        {
            if ((int)Uta.CheckSession(session) == 0) return null;
            Uta.UserActive(session);
            mUser loggedIn = new mUser(session, (int)Uta.SessionUserId(session));
            loggedIn.authorize();
            return loggedIn;
        }
        //Wylogowanie uzytkownika. Usuniecie tokena sesji i przelaczenie stanu aktywnosci na false
        public static mUser logOff(String session)
        {
            if ((int)Uta.CheckSession(session) == 0) return null;
            mUser loggedOff = new mUser(DBNull.Value.ToString(), (int)Uta.SessionUserId(session));
            Uta.NewSession(DBNull.Value.ToString(), false, loggedOff.getId());
            loggedOff.unauthorize();
            return loggedOff;
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
        public static mUser register(String name, String login, String password)
        {
            //Mozna dodac jeszcze jakies warunki dotyczace hasla
            int userid = -1;
            int message = 0;
            if ((int)Uta.LoginExists(login) > 0)
            {
                message -= 5;
                if ((int)Uta.NameExists(name) == 1) message -= 3;
                mUser taken = new mUser("", message);
                return taken;
            }
            if ((int)Uta.NameExists(name) > 0)
            {
                message -= 3;
                mUser taken = new mUser("", message);
                return taken;
            }
            
            String guid;
            do
            {
                guid = Guid.NewGuid() + "";
            }
            while ((int)Uta.CheckSession(guid) != 0);           
            userid = (int)Uta.NewUser(name, login, password, guid);

            mUser newUser=new mUser(userid,name, login, password,guid);
            newUser.authorize();
            return newUser;
        }

        public static mUser disconnect(String session)
        {
            if ((int)Uta.CheckSession(session) == 0) return null;
            Uta.UserInactive(session);
            mUser loggedIn = new mUser(session, (int)Uta.SessionUserId(session));
            loggedIn.authorize();
            return loggedIn;
        }
        public static List<mUser> getActiveUsers()
        {
            DataTable list = Uta.ActiveUsers();
            return dataToUsersList(list);
        }
        public static List<mUser> getFriends(String sessionToken)
        {
            int userId = (int)Uta.SessionUserId(sessionToken);
            DataTable list = UFta.UserFriends(userId);
            return dataToUsersList(list);
        }
        public static List<mUser> getActiveFriends(String sessionToken)
        {
            DataTable list = UFta.UserActiveFriends((int)Uta.SessionUserId(sessionToken));
            return dataToUsersList(list);
        }

        private static List<mUser> dataToUsersList(DataTable list)
        {
            List<mUser> users = new List<mUser>();
            for (int i = 0; i < list.Rows.Count; i++)
            {
                var row = list.Rows[i];
                users.Add(new mUser(row["name"].ToString()));
            }
            return users;
        }

        public static Boolean addFriend(String sessionToken, String nameFriend)
        {
            Boolean added=false;
            int idUser = -1;
            int idUserFriend = -1;
            idUser = (int)Uta.SessionUserId(sessionToken);
            idUserFriend = (int)Uta.NameUserId(nameFriend);           
            if (UFta.CheckFriend(idUser, idUserFriend) == 0)
            {
                var db = UFta.NewFriend(idUser, idUserFriend);
                if (db > 0) added = true;
            }
            return added;
        }

        public static Boolean removeFriend(String sessionToken, String nameFriend)
        {
            Boolean removed = false;
            int idUser = -1;
            int idUserFriend = -1;
            idUser = (int)Uta.SessionUserId(sessionToken);
            idUserFriend = (int)Uta.NameUserId(nameFriend);            
            if (UFta.CheckFriend(idUser, idUserFriend)>0)
            {
                var db = UFta.DeleteFriend(idUser, idUserFriend);
                if (db > 0) removed = true;
            }
            return removed;
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