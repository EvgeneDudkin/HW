open System.Net
open Microsoft.FSharp.Control.WebExtensions
open System.Text.RegularExpressions

let url = "http://www.1tv.ru/"
let pattern = "a href=(\"http://([^\"]*\")|\'[^\']*\')"
let regx = new Regex(pattern)

let fetchSync(name, url:string) = 
    let uri = new System.Uri(url)
    let webClient = new WebClient()
    let html = webClient.DownloadString(uri)
    printfn "Read %d characters for %s" html.Length name
    html

let fetchAsync(name, url:string) =
    async { 
        try 
            let uri = new System.Uri(url)
            let webClient = new WebClient()
            let! html = webClient.AsyncDownloadString(uri)
            printfn "%s --- %d" name html.Length
        with
            | ex -> printfn "%s" (ex.Message);
    }

let fromHtmlInMatchToURL (htmlInAdr:Match) =
    let temp = htmlInAdr.ToString()
    temp.[8..temp.Length - 2]

let downloadAllPages() = 
    let startPage = fetchSync(url, url)
    seq {for page in regx.Matches(startPage) do yield (fromHtmlInMatchToURL page, fromHtmlInMatchToURL page)}
    |> Seq.map fetchAsync
    |> Async.Parallel
    |> Async.RunSynchronously 
    |> ignore

downloadAllPages()
