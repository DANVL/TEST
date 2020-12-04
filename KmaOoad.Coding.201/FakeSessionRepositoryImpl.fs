namespace KmaOoad.Coding._201

open System
open System.Collections.Generic
open Types
open ISessionRepository
open Settings

module FakeSessionRepositoryImpl =
    type FakeSessionRepositoryImpl() =
        let mutable list: list<Session> = []

        interface ISessionRepository with
            member this.NewSession token userId time =
                let newSession = Session(Guid.NewGuid(), userId, token, time, time + sessionValidPeriod, time + tokenValidPeriod)
                list <- List.append list [ newSession ]
                newSession

            member this.EndSession sessionToEndId =
                list <- List.filter (fun ses -> ses.sessionId <> sessionToEndId) list
                ()

            member this.GetSession token: Option<Session> =
                try
                    let session =
                        List.find (fun (ses: Session) -> ses.token = token) list

                    Some(session)
                with :? KeyNotFoundException -> None

            member this.UpdateSession newToken sessionId =
                let session =
                    List.find (fun (ses: Session) -> ses.sessionId = sessionId) list

                (this :> ISessionRepository).EndSession session.sessionId
                session.token <- newToken
                session.tokenExpirationTime <- DateTime.Now + sessionValidPeriod
                list <- List.append list [ session ]

                ()

            member this.GetAllExpired() =
                List.filter (fun ses -> ses.expirationTime < DateTime.Now) list

            member this.GetAll() =
                list
