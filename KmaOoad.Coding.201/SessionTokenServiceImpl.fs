namespace KmaOoad.Coding._201

open System
open Types
open ISessionTokenService
open ISessionRepository

module SessionTokenServiceImpl =
    type SessionTokenServiceImpl(sessionRepository: ISessionRepository) =       
        interface ISessionTokenService with 
            member this.CreateSession(token: Token, userId: Guid): Session = 
                sessionRepository.NewSession token userId DateTime.Now
            
            member this.CreateToken(userData: UserData) = 
                use md5 = System.Security.Cryptography.MD5.Create()
                userData.username + System.DateTime.Now.ToString()
                |> System.Text.Encoding.ASCII.GetBytes
                |> md5.ComputeHash
                |> Seq.map (fun c -> c.ToString("X2"))
                |> Seq.reduce (+)
            
            member this.GetSession(token: Token): Option<Session> =
                sessionRepository.GetSession token
            
            member this.UpdateSessionWithNewToken(newToken: Token, sessionId: Guid) =
                sessionRepository.UpdateSession newToken sessionId
            
            member this.GetAllExpired() = 
                sessionRepository.GetAllExpired()

            member this.EndSession(sessionId:Guid) = 
                sessionRepository.EndSession sessionId