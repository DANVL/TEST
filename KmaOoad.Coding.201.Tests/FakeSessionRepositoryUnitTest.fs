namespace KmaOoad.Coding._201

open System
open Xunit
open Types
open KmaOoad.Coding._201.FakeSessionRepositoryImpl
open ISessionRepository


module FakeSessionRepositoryUnitTest =

    let sessionRepository = FakeSessionRepositoryImpl()

    [<Fact>]
    let GetAllExpiredTest () =
        let list = (sessionRepository :> ISessionRepository).GetAllExpired()

        Assert.True(list.IsEmpty)

    [<Fact>]
    let GetAllTest () =
        let token: Token = "TOKEN"
        let userId = Guid.NewGuid()
        let time = DateTime.Now

        (sessionRepository :> ISessionRepository).NewSession token userId time |> ignore

        let list = (sessionRepository :> ISessionRepository).GetAll()

        Assert.False(list.IsEmpty)

    [<Fact>]
    let NewSessionTest () =
        let token: Token = "TOKEN"
        let userId = Guid.NewGuid()
        let time = DateTime.Now

        let session = (sessionRepository :> ISessionRepository).NewSession token userId time

        Assert.NotNull(session)    
