namespace KmaOoad.Coding._201

open System
open Types
open IAuthenticationService



module AuthenticationController =
    type AuthenticationController(authenticationService: IAuthenticationService) =
        member this.Login(username: string, password: string) =
            authenticationService.Login(UserData(username, password))

        member this.Logout(token: Token) = authenticationService.Logout(token)

        member this.Renew(token: Token) =
            authenticationService.RenewSession(token)

        member this.EndSession(currentToken: Token, sessionIdToEnd: Guid) =
            authenticationService.EndSession(currentToken, sessionIdToEnd)
