namespace KmaOoad.Coding._201

open Xunit
open System

open KmaOoad.Coding._201.FakeUserRepositoryImpl
open KmaOoad.Coding._201.IUserRepository

open KmaOoad.Coding._201.Types

module FakeUserRepositoryUnitTest =

    let userRepository = FakeUserRepositoryImpl()

    [<Fact>]
    let FindUserByIdTest () =

        let username = "username5";
        let user: Option<User> =
            (userRepository :> IUserRepository).FindUserById(Guid.Parse("42fedb98-cbeb-489b-ac86-e39907962733"))

        Assert.Equal(user.Value.userData.username,username)

    [<Fact>]
    let FindUserByIdNullTest () =
        let userFail: Option<User> =
            (userRepository :> IUserRepository).FindUserById(Guid.NewGuid())

        Assert.Null(userFail)    

    [<Fact>]
    let FindUserTest () =
        let userData = UserData("username3", "password")

        let user: Option<User> =
            (userRepository :> IUserRepository).FindUser(userData)

        Assert.NotNull(user)

    [<Fact>]
    let FindUserNullTest () =
        let userDataFake = UserData("fakeName", "password")

        let userFail: Option<User> =
            (userRepository :> IUserRepository).FindUser(userDataFake)

        Assert.Null(userFail)    
