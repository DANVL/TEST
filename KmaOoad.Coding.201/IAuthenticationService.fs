namespace KmaOoad.Coding._201

open System
open Types

module IAuthenticationService =
    [<Interface>]
    type IAuthenticationService =
        //for valid input, start new login session, issue access token
        abstract member Login : UserData -> Token

        //end current login session, invalidate access token
        abstract member Logout : Token -> unit

        //requires valid access token
        abstract member EndSession : Token * Guid -> unit

        //works both with valid token or invalid token if session is still valid; old session ends, new session starts
        abstract member RenewSession : Token -> Token

        //After defined period, end session and notify client through WebSocket hub (see picture)
        //AUTOEND SESSION - TBD...