
module FileCrawler

open System
open System.IO

let internal dirInfoFilter (dirInfo: DirectoryInfo) =
    (not (dirInfo.Name.StartsWith("."))) &&
        (not (dirInfo.Attributes.HasFlag(FileAttributes.Hidden) ||
             dirInfo.Attributes.HasFlag(FileAttributes.System) ||
             dirInfo.Attributes.HasFlag(FileAttributes.ReadOnly)))    

let internal crawlDirectories rootDirectory =
    let rec crawlSubDirectories results unprocessed =
        let directory = List.head unprocessed
        let restOfUnprocessed = List.tail unprocessed
        let currentDirInfo = DirectoryInfo(directory)
        let subDirectories =
            currentDirInfo.EnumerateDirectories()
            |> Seq.filter (fun item -> (dirInfoFilter item) && (not (List.exists (fun name -> String.Equals(name, item.FullName)) results)))
            |> Seq.map (fun item -> item.FullName)
            |> Seq.toList

        let newResults = directory :: results
        if (List.isEmpty subDirectories) && (List.isEmpty restOfUnprocessed) then
            newResults
        else
            crawlSubDirectories newResults (restOfUnprocessed @ subDirectories)

    crawlSubDirectories List.Empty (rootDirectory :: [])
        |> List.rev

let internal crawlFiles directoryPath =
    let files =
        Directory.EnumerateFiles(directoryPath, "*", SearchOption.TopDirectoryOnly)
        |> Seq.sortBy (fun item -> item.ToLowerInvariant())
    files
