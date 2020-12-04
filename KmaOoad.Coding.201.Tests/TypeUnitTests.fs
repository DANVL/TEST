namespace KmaOoad.Coding._201

open Xunit
open KmaOoad.Coding._201.Types

module SessionTokenServiceUTests =

    [<Fact>]
    let FindUserByIdTest () =
        let username = "username"
        let password = "password"

        let userData: UserData = UserData(username, password)

        Assert.Equal(userData.username, username)
        Assert.Equal(userData.password, password)
