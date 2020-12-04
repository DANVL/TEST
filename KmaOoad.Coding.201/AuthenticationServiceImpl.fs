namespace KmaOoad.Coding._201

open System
open Types
open IAuthenticationService
open IUserService
open ISessionTokenService

module AuthenticationServiceImpl =
    type AuthenticationServiceImpl(userService: IUserService, sessionTokenService: ISessionTokenService) =       
        interface IAuthenticationService with 
            
            member this.Login(userdata: UserData): Token =
                match userService.FindUser userdata with 
                | Some(user) ->
                    let session = sessionTokenService.CreateSession (sessionTokenService.CreateToken userdata, user.userId)
                    session.token
                | None -> null//"Could not find user with such credentials"

            member this.Logout(token: Token):unit = 
                match sessionTokenService.GetSession token with
                | None -> failwith "Could not find session with that access token" 
                | Some(session) -> sessionTokenService.EndSession session.sessionId
            
            member this.EndSession(currentToken: Token, sessionToEnd: Guid) = 
                match sessionTokenService.GetSession currentToken with
                | None -> failwith "Could not find session with that token, please log in"
                | Some(session) -> 
                    if session.IsValid()
                    then 
                        if session.IsTokenValid() 
                        then 
                            if session.sessionId <> sessionToEnd
                            then sessionTokenService.EndSession(sessionToEnd)
                            else failwith "You can't end the current session" 
                        else failwith "Your token is invalid, please renew session or relogin" //WS hub could be used here
                    else failwith "The current session has ended, please relogin" //WS hub could be used here
            
            member this.RenewSession(token: Token): Token =
                match sessionTokenService.GetSession token with
                | None -> null //"Could not find session with that access token" could be logged here
                | Some(session) -> 
                    if session.IsValid() 
                    then 
                        match userService.FindUserById(session.userId) with
                        | Some(usr) -> 
                            let freshToken = sessionTokenService.CreateToken(usr.userData)
                            sessionTokenService.UpdateSessionWithNewToken(freshToken, usr.userId)
                            freshToken
                        | None -> null //"Exception: Could not find user with such id" could be logged here
                    else failwith "The session has expired, please, relogin" //WS hub could be used here with custom error