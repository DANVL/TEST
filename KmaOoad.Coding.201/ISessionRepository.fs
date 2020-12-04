namespace KmaOoad.Coding._201

open System
open Types

module ISessionRepository =

    [<Interface>]
    type ISessionRepository =
        abstract NewSession: Token -> Guid -> DateTime -> Session
        abstract EndSession: Guid -> unit
        abstract UpdateSession: Token -> Guid -> unit
        abstract GetSession: Token -> Option<Session>
        abstract GetAllExpired: unit ->  list<Session>
        abstract GetAll: unit ->  list<Session>
