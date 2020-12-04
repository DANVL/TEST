namespace KmaOoad.Coding._201

open System

module Types =

    type Token = string

    type UserData(username: string, password: string) =
        member x.username = username
        member x.password = password
        override x.GetHashCode() = hash (username, password)

        override x.Equals(b) =
            match b with
            | :? UserData as user -> (username, password) = (user.username, user.password)
            | _ -> false

    type Session(sessionId: Guid,
                 userId: Guid,
                 token: Token,
                 creationTime: DateTime,
                 expirationTime: DateTime,
                 tokenExpirationTime: DateTime) =
        member x.sessionId = sessionId
        member x.userId = userId
        member val token = token with get, set
        member x.creationTime = creationTime
        member public x.expirationTime = expirationTime
        member val tokenExpirationTime = tokenExpirationTime with get, set

        member x.IsValid(): bool = DateTime.Now < x.expirationTime
        member x.IsTokenValid(): bool = DateTime.Now < x.tokenExpirationTime

    type User(userId: Guid, userData: UserData) =
        member x.userId = userId
        member x.userData = userData
