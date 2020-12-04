namespace KmaOoad.Coding._201

open System.Threading.Tasks
open Types
open IPeriodicWork
open ISessionRepository


module SessionsPeriodicWorkImpl =
    type SessionsPeriodicWorkImpl(sessionRepository: ISessionRepository) =       
        interface IPeriodicWork with 
            member this.PeriodicClearAllExpiredSessions(token: Token, interval: int): Async<unit> =
                let awaitTaskVoid : (Task -> Async<unit>) = Async.AwaitIAsyncResult >> Async.Ignore
                async {
                    while true do
                    List.iter (fun (x: Session) -> sessionRepository.EndSession x.sessionId) sessionRepository.GetAllExpired
                    do! Task.Delay(interval) |> awaitTaskVoid
                }