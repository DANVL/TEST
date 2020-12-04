namespace KmaOoad.Coding._201

open System
open Types

module IUserRepository =

    [<Interface>]
    type IUserRepository =
        abstract FindUser: UserData -> Option<User>
        abstract FindUserById: Guid -> Option<User>
