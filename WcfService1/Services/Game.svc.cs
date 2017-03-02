using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Checkers.Interfaces;
using Checkers.App_Data;
using Checkers.Models;
namespace Checkers.Services
{
    /// <summary>
    /// Serwis odpowiedzialny za panowanie nad rozgrywka.
    /// </summary>
    public class Game : IGame
    {
        public GamesResponse getGames(String sessionToken)
        {
            Login LoginService = new Login();
            Boolean responded = false;
            List<mGame> games = new List<mGame>();
            if (LoginService.session(sessionToken).Authorized == true)
            {
                games = DBControler.getGames(sessionToken);
                responded = true;
            }
            else sessionToken = "";

            return new GamesResponse
            {
                Session = sessionToken,
                Successful = responded,
                Games = gamesListToJson(games)
            };
        }
        
        private List<mGame> gamesListToJson(List<mGame> games)
        {
            List<mGame> gamesJson = new List<mGame>();

            foreach (mGame i in games)
                gamesJson.Add(new mGame
                {
                    idGame=i.getId(),
                    Player1name=i.getPlayer1name(),
                    Player2name=i.getPlayer2name()

                });
            return gamesJson;
        }
        //needs fix
        public GameResponse newGame(String sessionToken, String SidGame)
        {
            Login LoginService = new Login();
            Boolean created = false;
            int idGame = Int16.Parse(SidGame);
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
            return new GameResponse
            {
                Session = sessionToken,
                Successful = created,
                Message = message
            };
        }
        public GameResponse movePawn(String sessionToken, String sidGame, String spreX, String spreY, String spostX, String spostY)
        {
            Login LoginService = new Login();
            Boolean moved = false;
            int idGame = Int16.Parse(sidGame);
            int preX = Int16.Parse(spreX);
            int preY = Int16.Parse(spreY);
            int postX = Int16.Parse(spostX);
            int postY = Int16.Parse(spostY);
            mMove move = null;
            String message = "Login fail";
            if (LoginService.session(sessionToken).Authorized == true)
            {
                message = "Pawn doesn't exist";
                move = DBControler.movePawn(sessionToken, idGame, preX, preY, postX, postY);
                if (move != null)
                {
                    message = move.idMove+"";
                    moved = true;
                }
            }
            return new GameResponse
            {
                Session = sessionToken,
                Successful = moved,
                Message = message
            };
        }

        public MovesResponse getLastMoves(String sessionToken, String sidGame, String sidLastMove)
        {
            Login LoginService = new Login();
            Boolean send = false;
            int idGame = Int16.Parse(sidGame);
            int idLastMove = Int16.Parse(sidLastMove);
            List<mLog> move = null;
            if (LoginService.session(sessionToken).Authorized == true)
            {
                move=DBControler.getLastMoves(sessionToken, idGame, idLastMove);
                send = true;
            }
            return new MovesResponse
            {
                Session = sessionToken,
                Successful = send,
                Moves = movesListToJson(move)
            };
        }
        private List<mLog> movesListToJson(List<mLog> moves)
        {
            List<mLog> move = new List<mLog>();

            if(moves!=null) foreach (mLog m in moves)
                move.Add(new mLog
                {
                    idGame=m.idGame,
                   // idPawnMoved=m.idPawnMoved,
                   // idPawnOut=m.idPawnOut,
                    idMove=m.idMove,
                    preColumn=m.preColumn,
                    postColumn=m.postColumn,
                    preRow=m.preRow,
                    postRow = m.postRow

                });
            return move;
        }

        public GamesResponse getFullGames(String sessionToken)
        {
            Login LoginService = new Login();
            Boolean responded = false;
            List<mGame> games = new List<mGame>();
            if (LoginService.session(sessionToken).Authorized == true)
            {
                games = DBControler.getFullGames(sessionToken);
                responded = true;
            }
            else sessionToken = "";

            return new GamesResponse
            {
                Session = sessionToken,
                Successful = responded,
                Games = gamesListToJson(games)
            };
        }

        public GameResponse finishMove(String sessionToken, String sidGame)
        {
            Login LoginService = new Login();
            Boolean finished = false;
            int idGame = Int16.Parse(sidGame);
            String message = "Failed to finish move";
            if (LoginService.session(sessionToken).Authorized == true)
            {
                message= ""+DBControler.finishMove(sessionToken, idGame);
                finished = true;
            }
            return new GameResponse
            {
                Session = sessionToken,
                Successful = finished,
                Message = message
            };
        }

        public GameResponse finishGame(String sessionToken, String sidGame)
        {
            Login LoginService = new Login();
            Boolean finished = false;
            int idGame = Int16.Parse(sidGame);
            String message = "Failed to login";
            if (LoginService.session(sessionToken).Authorized == true)
            {
                message = "" + DBControler.finishGame(sessionToken, idGame);
                finished = true;
            }
            return new GameResponse
            {
                Session = sessionToken,
                Successful = finished,
                Message = message
            };
        }
    }
}
