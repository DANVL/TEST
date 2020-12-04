namespace KmaOoad.Coding._201

open System
open Xunit
open KmaOoad.Coding._201.PeriodicWork
open KmaOoad.Coding._201.SessionTokenServiceImpl
open FakeSessionRepositoryImpl
open IPeriodicWork

module SessionPeriodicWorkUnitTest =

    let periodicWork = PeriodicWork(SessionTokenServiceImpl(FakeSessionRepositoryImpl()))

    [<Fact>]
    let PeriodicWorkTest () =
        let interval = 1
        (periodicWork :> IPeriodicWork).PeriodicClearAllExpiredSessions(interval)
        |> ignore

        Assert.Equal(interval, 1)
       