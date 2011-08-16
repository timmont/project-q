module Hasher

open System
open System.Security.Cryptography

let testBytes : byte[] = Array.init 4 (fun _ -> byte(0))

let internal hashBytes (bytes: byte array) =
    let SHACrypt = SHA512Managed.Create()
    SHACrypt.ComputeHash(bytes) |> BitConverter.ToString

