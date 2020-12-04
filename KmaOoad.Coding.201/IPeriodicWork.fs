namespace KmaOoad.Coding._201

open System
open Types

module IPeriodicWork =
    [<Interface>]
    type IPeriodicWork =
        abstract member PeriodicClearAllExpiredSessions : int -> Async<unit>