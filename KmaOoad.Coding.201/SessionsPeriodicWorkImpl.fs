namespace KmaOoad.Coding._201

open System.Threading.Tasks
open Types
open IPeriodicWork
open ISessionTokenService


module PeriodicWork =
    type PeriodicWork(sessionTokenService: ISessionTokenService) =       
        interface IPeriodicWork with 
            member this.PeriodicClearAllExpiredSessions(interval: int): Async<unit> =
                let awaitTaskVoid : (Task -> Async<unit>) = Async.AwaitIAsyncResult >> Async.Ignore
                async {
                    while true do
                    List.iter (fun (x: Session) -> sessionTokenService.EndSession x.sessionId) (sessionTokenService.GetAllExpired())
                    do! Task.Delay(interval) |> awaitTaskVoid
                }