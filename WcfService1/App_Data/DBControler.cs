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
        static SQL_DATASETTableAdapters.tCheckersTableAdapter Cta = new SQL_DATASETTableAdapters.tCheckersTableAdapter();
        static SQL_DATASETTableAdapters.tGamesTableAdapter Gta= new SQL_DATASETTableAdapters.tGamesTableAdapter();
        static SQL_DATASETTableAdapters.tTablesTableAdapter Tta = new SQL_DATASETTableAdapters.tTablesTableAdapter();
        static SQL_DATASETTableAdapters.tInvitesTableAdapter Ita = new SQL_DATASETTableAdapters.tInvitesTableAdapter();
        static SQL_DATASETTableAdapters.tLogsTableAdapter Lta = new SQL_DATASETTableAdapters.tLogsTableAdapter();
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

        public static mChecker newChecker()
        {
            mChecker checker;
            var idChecker=Cta.GetLatestCheckerId();
            if (idChecker != null)
            {
                checker = new mChecker(((int)idChecker)+1);
            }
            else
            {
                checker = new mChecker(1);
            }
            int idPawn=1;
            while (idPawn <= 24)
            {
                mPawn pawn=checker.getPawn(idPawn);
                Cta.PawnInsert(checker.getId(),pawn.getId(), pawn.getColor(), pawn.Queen(), pawn.isInGame(), pawn.getColumn(), pawn.getRow());
                idPawn++;
            }
            return checker;
        }

        public static mGame newGame(int player)
        {
            mGame game;
            mChecker checker=newChecker();
            var idGame=Gta.NewGame(checker.getId(), player);
            if (idGame != null)
            {
                game = new mGame(Decimal.ToInt16((decimal)idGame), player, checker);
            }
            else return null;
            return game;
        }

        public static Boolean addPlayer2(int idPlayer2,int idGame)
        {
            Boolean added=false;
            int changed=Gta.SetPlayer2(idPlayer2, idGame);
            if (changed > 0) added = true;
            return added;
        }

        public static mTable newTable(String sessionToken)
        {
            mTable table;
            int idUser = -1;
            idUser = (int)Uta.SessionUserId(sessionToken); 
            mGame game = newGame(idUser);
            int idGame = game.getId();
            var idTable = Tta.CreateTable(idGame, idUser);
            if (idTable!=null)
            {
                table = new mTable(Decimal.ToInt16((decimal)idTable), idGame, idUser);
            }
            else return null;
            return table;
        }

        public static mUser newInvite(String sessionToken, String name, int idGame)
        {
            
            int idUser = -1;
            int idUserInvited = -1;
            idUser = (int)Uta.SessionUserId(sessionToken);
            if (Gta.CheckPermission(idUser, idGame) == 0) return new mUser(sessionToken,0);
            
            idUserInvited = (int)Uta.NameUserId(name);
            if (idUserInvited < 1) return new mUser(sessionToken, -1);
            if (Ita.UpdateInvite(idGame, idUserInvited) > 0) return new mUser(sessionToken, -4);
            else Ita.NewInvite(idGame, idUserInvited);
            //if ((Decimal)idInvitation < 1) return new mUser(sessionToken, -3);

            return new mUser(sessionToken,1);
        }

        public static mMove movePawn(String sessionToken, int idGame,int preX, int preY, int postX, int postY) //still working on it
        {
            //dodac logowanie ruchu
            int idUser = -1;
            int idPawnOut=-2;
            idUser = (int)Uta.SessionUserId(sessionToken);
            DataTable pawns= Cta.FindPawnIdByPossition(preY, preX, idGame);
            mPawn Pawn = null;
            if (pawns.Rows.Count > 0)
            {
                var row = pawns.Rows[pawns.Rows.Count - 1];
                Pawn = new mPawn(Int16.Parse(row["IdPawn"].ToString()), Int16.Parse(row["PawnColumn"].ToString()), Int16.Parse(row["PawnRow"].ToString()), Int16.Parse(row["IdColor_"].ToString()), Int16.Parse(row["IdChecker"].ToString()));
            }
                if (postX == 8 && Pawn.getColor() == 1 || postX == 1 && Pawn.getColor() == 2) Pawn.advanceToQueen();
           
            if(Math.Abs(postX-preX)==2){
                DataTable pawns2= Cta.FindPawnIdByPossition((preX+postX), (preY+postY), idGame);
                if (pawns.Rows.Count > 0)
                {
                    var row2 = pawns2.Rows[pawns.Rows.Count - 1];
                    idPawnOut = Int16.Parse(row2["IdPawn"].ToString());
                    Cta.PawnOut(Pawn.getidChecker(), idPawnOut);
                }
            }
            if (Pawn == null) return null;
            Cta.PawnMove(postX,postY,Pawn.getidChecker(),Pawn.getId()); //move Pawn
            Lta.Insert(Pawn.getidChecker(), Pawn.getId(), idPawnOut, preY, postY, preX, postX); //log move
            return new mMove(idGame, idUser, new mPawn(Pawn.getId(), Pawn.getRow(), Pawn.getColumn(), Pawn.getColor()), postX, postY);
        }

        public static List<mLog> getLastMoves(String sessionToken, int idGame, int idLastMove) //still working on it
        {
            int idUser = -1;
            idUser = (int)Uta.SessionUserId(sessionToken);
            DataTable list = Lta.GetLastMoves(idLastMove, idGame);
            return dataToMovesList(list);
        }
        private static List<mLog> dataToMovesList(DataTable list)
        {
            List<mLog> moves = new List<mLog>();
            for (int i = 0; i < list.Rows.Count; i++)
            {
                var row = list.Rows[i];                
                moves.Add(new mLog(0, Int16.Parse(row["IdGame"].ToString()), Int16.Parse(row["ColumnPre"].ToString()), Int16.Parse(row["ColumnPost"].ToString()), 
                    Int16.Parse(row["RowPre"].ToString()), Int16.Parse(row["RowPost"].ToString()), Int16.Parse(row["IdPawnOut_"].ToString()), Int16.Parse(row["IdPawnMoved_"].ToString())));
            }
            return moves;
        }
        public static mUser removeInvite(String sessionToken, String name, int idGame)
        {

            int idUser = -1;
            int idUserInvited = -1;
            idUser = (int)Uta.SessionUserId(sessionToken);
            if (Gta.CheckPermission(idUser, idGame) == 0) return new mUser(sessionToken, 0);

            idUserInvited = (int)Uta.NameUserId(name);
            if (idUserInvited < 1) return new mUser(sessionToken, -1);
            var idInvitation = Ita.DeleteInvite(idGame, idUserInvited);
            if ((Decimal)idInvitation < 1) return new mUser(sessionToken, -3);

            return new mUser(sessionToken, 1);
        }
        public static mUser refuseInvite(String sessionToken, int idGame)
        {
            int idUserInvited = -1;
            idUserInvited = (int)Uta.SessionUserId(sessionToken);    
            if (idUserInvited < 1) return new mUser(sessionToken, -1);
            var idInvitation = Ita.DeleteInvite(idGame, idUserInvited);
            if ((Decimal)idInvitation < 1) return new mUser(sessionToken, -3);
            return new mUser(sessionToken, 1);
        }
        public static mUser acceptInvite(String sessionToken, int idGame)
        {
            int idUserInvited = -1;
            idUserInvited = (int)Uta.SessionUserId(sessionToken);
            if (idUserInvited < 1) return new mUser(sessionToken, -1);
            if (Ita.CheckInvite(idGame,idUserInvited) < 1) return new mUser(sessionToken, -3);
            else if((int)Gta.HasPlayer2(idGame) > 0) return new mUser(sessionToken, -5); //room full
            else Gta.SetPlayer2(idUserInvited, idGame);
            return new mUser(sessionToken, 1);
        }

        public static List<mInvite> getInvitations(String sessionToken)
        {
            int idUser = (int)Uta.SessionUserId(sessionToken);
            DataTable list = Ita.GetUserInvites(idUser);
            return dataToInvitationList(list);
        }

        private static List<mInvite> dataToInvitationList(DataTable list)
        {
            List<mInvite> invites = new List<mInvite>();
            for (int i = 0; i < list.Rows.Count; i++)
            {
                var row = list.Rows[i];
                //(Convert.ToDateTime(row["SendTime"].ToString())
                invites.Add(new mInvite((DateTime)row["SendTime"], Int16.Parse(row["IdGame_"].ToString()), row["Name"].ToString()));

            }
            return invites;
        }

        public static List<mGame> getGames(String sessionToken)
        {
            int idUser = (int)Uta.SessionUserId(sessionToken);
            DataTable list = Gta.GetGames(idUser);
            return dataToGamesList(list);
        }

        private static List<mGame> dataToGamesList(DataTable list)
        {
            List<mGame> games = new List<mGame>();
            String Player1name = "";
            String Player2name = "";
            int Player1id=-1;
            int Player2id=-1;
            for (int i = 0; i < list.Rows.Count; i++)
            {
                var row = list.Rows[i];
                Player1id=Int16.Parse(row["IdUser1_"].ToString());
                var value = row["IdUser2_"];
                if (value!=DBNull.Value) Player2id = Int16.Parse(row["IdUser2_"].ToString());
                if(Player1id>0)Player1name = Uta.Username(Player1id);
                if(Player2id>0)Player2name = Uta.Username(Player2id);
                    games.Add(new mGame(Int16.Parse(row["IdGame"].ToString()), Player1name, Player2name));
            }
            return games;
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