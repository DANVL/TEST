namespace KmaOoad.Coding._201

open System
open Types
open IUserService
open IUserRepository

module UserServiceImpl =
    type UserServiceImpl(userRepository: IUserRepository) =       
        interface IUserService with 
            member this.FindUserById(userId: Guid) =
                userRepository.FindUserById userId
            member this.FindUser(userData: UserData) = 
                userRepository.FindUser userData