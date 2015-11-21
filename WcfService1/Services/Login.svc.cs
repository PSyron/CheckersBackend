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
        public LoginResponse session(String session)
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
        public LoginResponse connect(String session)
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
        public LoginResponse disconnect(String session)
        {
            Boolean authorized = false;
            mUser temp = DBControler.disconnect(session);
            if (temp != null && temp.isAuthorized() == true) authorized = true;
            return new LoginResponse
            {
                Session = session,
                Authorized = authorized
            };

        }
        public LoginResponse logIn(String login, String password)
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
        public LoginResponse logOff(String session)
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
        public RegisterResponse register(String name, String login, String password)
        {
            String session = "";
            Boolean authorized = false;
            int option=1;
            String message="Failed to add user";
            mUser user = DBControler.register(name,login, password);
            if (user != null)
            {
                session = user.getSession();
                authorized = user.isAuthorized();
                option = user.getId();
            }
            if (option > 0) message = "User added successfully";
            else if (option == -3) message = "Name already taken";
            else if (option == -5) message = "Login already taken";
            else if (option == -8) message = "Login and name already taken";

            return new RegisterResponse
            {
                Session = session,
                Authorized = authorized,
                Message = message
            };
        }
        //Sprawdzenie odpowiedzi serwisu
        public LoginResponse test2()
        {
            DBControler.newChecker();
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