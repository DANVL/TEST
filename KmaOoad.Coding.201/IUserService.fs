namespace KmaOoad.Coding._201

open System
open Types

module IUserService =
    [<Interface>]
    type IUserService =
      abstract FindUser: UserData -> Option<User>
      abstract FindUserById: Guid -> Option<User>