
module FileVersions.Hasher

open System
open System.IO
open System.Security.Cryptography

let internal hashBytes (bytes: byte array) =
    let SHACrypt = SHA512Managed.Create()
    SHACrypt.ComputeHash(bytes) |> BitConverter.ToString

let calculateHash fileName =
    File.ReadAllBytes fileName
    |> hashBytes

