using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Checkers.App_Data;
using Checkers.Models;
using Checkers.Interfaces;
namespace Checkers.Services
{
    /// <summary>
    /// Serwis odpowiedzialny za usluge logowania i nadzorowania sesji.
    /// </summary>
    public class Login : ILogin
    {
        public LoginResponse session(string session)
        {
            Boolean authorized = false;
            mUser temp = DBControler.logIn(session);
            if (temp != null && temp.isAuthorized() == true) authorized = true;
            return new LoginResponse
            {
                Session = session,
                Authorized = authorized
            };
        
        }     
        
        public LoginResponse logIn(string login, string password)
        {
            String session = "";
            Boolean authorized = false;
            mUser user = DBControler.logIn(login, password);
            if (user != null)
            {
                session = user.getSession();
                authorized = user.isAuthorized();
            }
            return new LoginResponse
            {
                Session = session,
                Authorized = authorized
            };
        }
        public LoginResponse logOff(string session)
        {
            Boolean authorized = false;
            mUser temp = DBControler.logOff(session);
            if (temp != null && temp.isAuthorized() == false)
            {
                authorized = true;
                session = "";
            }
            return new LoginResponse
            {
                Session = session,
                Authorized = authorized
            };

        } 
        
        //Sprawdzenie odpowiedzi serwisu
        public LoginResponse test2()
        {
            return new LoginResponse
            {
                Session = DBControler.czas(),
                Authorized = true
            };
        }
        //Sprawdzenie odpowiedzi z uzyciem bazy danych
        public LoginResponse test()
        {
            String session = "";
            Boolean authorized = false;
            String login = "t.kluza";
            String password = "tk321";
            mUser user = DBControler.logIn(login, password);
            if (user != null)
            {
                session = user.getSession();
                authorized = user.isAuthorized();
            }
            return new LoginResponse
            {
                Session = session,
                Authorized = authorized
            };
        }
        public LoginResponse test3()
        {
            String session = "";
            Boolean authorized = false;
            String login = "t.kluza";
            String password = "tk321";
            mUser user = DBControler.logIn(login, password);
            if (user != null) session = user.getSession();
            authorized = user.isAuthorized();
            return new LoginResponse
            {
                Session = session,
                Authorized = authorized
            };
        }
    }
}