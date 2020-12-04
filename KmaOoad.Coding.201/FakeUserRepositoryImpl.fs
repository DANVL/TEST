namespace KmaOoad.Coding._201

open System
open System.Collections.Generic
open Types
open IUserRepository
open Settings

module FakeUserRepositoryImpl =

    type FakeUserRepositoryImpl() =
        let mutable list: list<User> =
            [ User(Guid.Parse("957d9b7c-eb88-4439-aa02-629ed03f818d"), UserData("username1", "password"))
              User(Guid.Parse("deec58e5-d0d1-47a7-859b-31001b686d08"), UserData("username2", "password"))
              User(Guid.Parse("b283cf09-1964-4530-a19e-fc7aff09ce48"), UserData("username3", "password"))
              User(Guid.Parse("e24eac79-fa61-430d-8708-b312afe4ed40"), UserData("username4", "password"))
              User(Guid.Parse("42fedb98-cbeb-489b-ac86-e39907962733"), UserData("username5", "password")) ]

        interface IUserRepository with
            member this.FindUser userData =
                try
                    let usr =
                        List.find (fun (usr: User) -> usr.userData = userData) list

                    Some(usr)
                with :? KeyNotFoundException -> None

            member this.FindUserById id =
                try
                    let user =
                        List.find (fun (usr: User) -> usr.userId = id) list

                    Some(user)
                with :? KeyNotFoundException -> None
