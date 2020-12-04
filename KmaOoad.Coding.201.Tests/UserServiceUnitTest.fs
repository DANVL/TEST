namespace KmaOoad.Coding._201

open System
open Xunit
open KmaOoad.Coding._201.UserServiceImpl
open KmaOoad.Coding._201.FakeUserRepositoryImpl
open KmaOoad.Coding._201.IUserService

open KmaOoad.Coding._201.Types

module UserServiceUTests =

    let userRepository = FakeUserRepositoryImpl()
    let userService = UserServiceImpl(userRepository)

    [<Fact>]
    let FindUserByIdTest () =
        let user: Option<User> =
            (userService :> IUserService).FindUserById(Guid.Parse("42fedb98-cbeb-489b-ac86-e39907962733"))

        let userFail: Option<User> =
            (userService :> IUserService).FindUserById(Guid.NewGuid())

        Assert.NotNull(user)
        Assert.Null(userFail)

    [<Fact>]
    let FindUserTest () =
        let userData = UserData("username3", "password")
        let userDataFake = UserData("fakeName", "password")

        let user: Option<User> =
            (userService :> IUserService).FindUser(userData)

        let userFail: Option<User> =
            (userService :> IUserService).FindUser(userDataFake)

        Assert.NotNull(user)
        Assert.Null(userFail)
