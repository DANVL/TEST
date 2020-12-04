namespace KmaOoad.Coding._201

open System
open Types

module ISessionTokenService =

    [<Interface>]
    type ISessionTokenService =
      abstract CreateSession: Token * Guid -> Session
      abstract CreateToken: UserData -> Token
      abstract GetSession: Token -> Option<Session>
      abstract UpdateSessionWithNewToken: Token * Guid -> unit
      abstract GetAllExpired: unit -> list<Session>
      abstract EndSession: Guid -> unit