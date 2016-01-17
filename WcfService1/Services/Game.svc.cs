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
                games = DBControler.getInvitations(sessionToken);
                responded = true;
            }
            else sessionToken = "";

            return new GamesResponse
            {
                Session = sessionToken,
                Successful = responded,
                games = gamesListToJson(games)
            };
        }

        private List<mGame> gamesListToJson(List<mGame> games)
        {
            List<mGame> gamesJson = new List<mGame>();

            foreach (mGame i in games)
                gamesJson.Add(new mGame
                {
                    dateString = i.getTime().ToString(),
                    playerName = i.playerName,
                    idGame = i.getIdGame()

                });
            return gamesJson;
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
                Session = sessionToken,
                Successful = created,
                Message = message
            };
        }



    }
}
