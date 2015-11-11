using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Checkers.Interfaces
{
    /// <summary>
    /// Odwolujemy sie do metod zwartych w serwisach odwolujemy sie zgodnie z UriTemplate czyli np tak: http://localhost:13622/Services/Login.svc/test
    /// Aby dodac parametry do wywolywanej metody robimy tak:http://localhost:13622/Services/Login.svc/session/9fa1c33c-4180-4b02-9e2f-aa83f5e3af3f
    /// </summary>
    [ServiceContract]
    public interface ILogin
    {
        //Dodac active przy connect
        //http://156.17.130.217/Pz/Services/Login.svc/connect/8a502001-7291-4b8b-9ff1-d5617be4943e
        [OperationContract]
        [WebGet(UriTemplate = "connect/{session}", ResponseFormat = WebMessageFormat.Json)]
        LoginResponse connect(String session);
        //http://156.17.130.217/Pz/Services/Login.svc/disconnect/8a502001-7291-4b8b-9ff1-d5617be4943e
        [OperationContract]
        [WebGet(UriTemplate = "disconnect/{session}", ResponseFormat = WebMessageFormat.Json)]
        LoginResponse disconnect(String session);
        
        //http://156.17.130.217/Pz/Services/Login.svc/login/t.kluza/tk321
        [OperationContract]
        [WebGet(UriTemplate = "login/{login}/{password}", ResponseFormat = WebMessageFormat.Json)]
        LoginResponse logIn(String login, String password);

        //http://156.17.130.217/Pz/Services/Login.svc/login/<jakisGuidSesji>
        //Nie uzywaj tego z gory bo zniknie i bedzie trzeba sie znowu logowac i kopiowac.
        [OperationContract]
        [WebGet(UriTemplate = "logoff/{session}", ResponseFormat = WebMessageFormat.Json)]
        LoginResponse logOff(String session);

        //http://156.17.130.217/Pz/Services/Login.svc/register/<jakasNazwa>/<jakisLogin>/<jakiesHaslo>
        //http://156.17.130.217/Pz/Services/Login.svc/register/test5/test5/test5
        [OperationContract]
        [WebGet(UriTemplate = "register/{name}/{login}/{password}", ResponseFormat = WebMessageFormat.Json)]
        RegisterResponse register(String name, String login, String password);


        //testy
        [OperationContract]
        [WebGet(UriTemplate = "test", ResponseFormat = WebMessageFormat.Json)]
        LoginResponse test();
        [OperationContract]
        [WebGet(UriTemplate = "test2", ResponseFormat = WebMessageFormat.Json)]
        LoginResponse test2();
       
        [OperationContract]
        [WebGet(UriTemplate = "test3", ResponseFormat = WebMessageFormat.Json)]
        LoginResponse test3();
        }

    //Mozna dodac jeszcze obiekt z wiadomoscia, ktora wyjasnia czemu nie wpuscilo
        [DataContract]
        public class LoginResponse
        {
            [DataMember]
            public String Session { get; set; }
            [DataMember]
            public Boolean Authorized { get; set; }
        }
        [DataContract]
        public class RegisterResponse
        {
            [DataMember]
            public String Session { get; set; }
            [DataMember]
            public Boolean Authorized { get; set; }
            [DataMember]
            public String Message { get; set; }
        }
    }

