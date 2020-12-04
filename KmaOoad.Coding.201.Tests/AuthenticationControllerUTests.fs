namespace KmaOoad.Coding._201

open System
open Xunit
open KmaOoad.Coding._201.AuthenticationController
open AuthenticationServiceImpl
open UserServiceImpl
open FakeUserRepositoryImpl
open SessionTokenServiceImpl
open FakeSessionRepositoryImpl
open KmaOoad.Coding._201.Types


module AuthenticationControllerUTests =

    let authenticationController =
        AuthenticationController
            (AuthenticationServiceImpl
                (UserServiceImpl(FakeUserRepositoryImpl()), SessionTokenServiceImpl(FakeSessionRepositoryImpl())))


    [<Fact>]
    let LoginTest () =
        let username: string = "username1"
        let password: string = "password"

        let token: Token =
            authenticationController.Login(username, password)

        Assert.False(isNull token)
        Assert.Equal(32, token.Length)
        
        
    [<Fact>]
    let RenewTest () =
        let username: string = "username1"
        let password: string = "password"

        let token: Token =
            authenticationController.Login(username, password)

        Assert.Throws<Collections.Generic.KeyNotFoundException>((fun () -> authenticationController.Renew token |> ignore))

    [<Fact>]
    let LogoutTest () =
        let username: string = "username1"
        let password: string = "password"

        let token: Token = authenticationController.Login(username, password)

        let ex = Record.Exception(fun () -> authenticationController.Logout(token))

        Assert.Null(ex)

    [<Fact>]
        let EndSessionTest () =
            let username: string = "username1"
            let password: string = "password"

            let token: Token = authenticationController.Login(username, password)

            let ex = Record.Exception(fun () -> authenticationController.EndSession(token,Guid.NewGuid()))

            Assert.Null(ex)



