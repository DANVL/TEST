namespace KmaOoad.Coding._201

open System
open Xunit
open KmaOoad.Coding._201.SessionTokenServiceImpl
open KmaOoad.Coding._201.ISessionTokenService
open KmaOoad.Coding._201.FakeSessionRepositoryImpl
open KmaOoad.Coding._201.ISessionRepository
open KmaOoad.Coding._201.Types

module SessionTokenServiceUnitTest =

    let sessionRepository = FakeSessionRepositoryImpl()

    let sessionTokenService =
        SessionTokenServiceImpl(sessionRepository)

    [<Fact>]
    let CreateTokenTest () =
        let testUserData: UserData = UserData("Dania", "ilovefsharp1337")

        let token: Token =
            (sessionTokenService :> ISessionTokenService).CreateToken(testUserData)

        Assert.False(isNull token)
        Assert.Equal(32, token.Length)

    [<Fact>]
    let CreateSessionTest () =
        let testUserData: UserData = UserData("Anatoliy", "ilovefsharp")

        let token: Token =
            (sessionTokenService :> ISessionTokenService).CreateToken(testUserData)

        let userId = Guid.NewGuid()

        let session: Session =
            (sessionTokenService :> ISessionTokenService).CreateSession(token, userId)

        Assert.Equal(userId, session.userId)
        Assert.Equal(token, session.token)
        Assert.Equal(session, ((sessionRepository :> ISessionRepository).GetSession token).Value)

    [<Fact>]
    let UpdateSessionWithNewTokenTest () =
        let testUserData: UserData = UserData("Ihor", "ihorihor")

        let token: Token =
            (sessionTokenService :> ISessionTokenService).CreateToken(testUserData)

        let userId = Guid.NewGuid()

        let session: Session =
            (sessionTokenService :> ISessionTokenService).CreateSession(token, userId)

        let previousTokenExpirationTime = session.tokenExpirationTime

        let tokenNew: Token =
            (sessionTokenService :> ISessionTokenService).CreateToken(testUserData)
        //updating the session with a new token
        (sessionTokenService :> ISessionTokenService).UpdateSessionWithNewToken(tokenNew, session.sessionId)

        let sessionAfterUpdate =
            (sessionRepository :> ISessionRepository).GetSession tokenNew

        Assert.True(sessionAfterUpdate.IsSome)
        Assert.Equal(tokenNew, sessionAfterUpdate.Value.token)
        Assert.NotEqual(previousTokenExpirationTime, sessionAfterUpdate.Value.tokenExpirationTime)

    [<Fact>]
    let EndSessionUnitTest () =
        let testUserData: UserData = UserData("Slavik", "slavonloverevo228")

        let token: Token =
            (sessionTokenService :> ISessionTokenService).CreateToken(testUserData)

        let userId = Guid.NewGuid()

        let session: Session =
            (sessionTokenService :> ISessionTokenService).CreateSession(token, userId)

        //updating the session with a new token
        (sessionTokenService :> ISessionTokenService).EndSession(session.sessionId)

        let sessionOption: Option<Session> =
            (sessionRepository :> ISessionRepository).GetSession token

        Assert.True(sessionOption.IsNone)

    [<Fact>]
    let GetSessionUnitTest () =
        let testUserData: UserData =
            UserData("Slaventiy", "slavonloverevo1337")

        let token: Token =
            (sessionTokenService :> ISessionTokenService).CreateToken(testUserData)

        let userId = Guid.NewGuid()

        let session: Session =
            (sessionTokenService :> ISessionTokenService).CreateSession(token, userId)
        //getting existing session by linked token
        let sessionOptionExisting: Option<Session> =
            (sessionRepository :> ISessionRepository).GetSession token
        //getting non-existing session by linked token
        let testUserDataNew: UserData = UserData("test", "test")

        let tokenWithoutSession: Token =
            (sessionTokenService :> ISessionTokenService).CreateToken(testUserDataNew)

        let sessionOptionNonExisting: Option<Session> =
            (sessionRepository :> ISessionRepository).GetSession tokenWithoutSession

        Assert.True(sessionOptionExisting.IsSome)
        Assert.True(sessionOptionNonExisting.IsNone)
