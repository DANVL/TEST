namespace KmaOoad.Coding._201

open System
open Xunit
open KmaOoad.Coding._201.SessionTokenServiceImpl
open KmaOoad.Coding._201.FakeSessionRepositoryImpl
open KmaOoad.Coding._201.ISessionRepository
open KmaOoad.Coding._201.UserServiceImpl
open KmaOoad.Coding._201.FakeUserRepositoryImpl
open KmaOoad.Coding._201.AuthenticationServiceImpl
open KmaOoad.Coding._201.IAuthenticationService
open KmaOoad.Coding._201.Types

module AuthenticationServiceUnitTests =
    
    let sessionRepository = FakeSessionRepositoryImpl()
    let sessionTokenService = SessionTokenServiceImpl(sessionRepository)
    let userRepository = FakeUserRepositoryImpl()
    let userService = UserServiceImpl(userRepository)
    let authenticationService = AuthenticationServiceImpl(userService, sessionTokenService)  
    
    [<Fact>]
    let ``LoginTest`` () =
        //existing user
        let testUserData: UserData = UserData("username1","password")    
        
        let token = (authenticationService :> IAuthenticationService).Login(testUserData)
        let session = (sessionRepository :> ISessionRepository).GetSession token

        Assert.False(session.IsNone)
        //non-existing user
        let testUserDataNonExisting: UserData = UserData("nonexisting","badpassword")    
        
        let tokenNonExisting = (authenticationService :> IAuthenticationService).Login(testUserDataNonExisting)
        
        Assert.True(isNull tokenNonExisting)