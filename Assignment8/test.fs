module T

open System.Net
open System.IO
open Microsoft.FSharp.Control.WebExtensions
open System.Text
open System.Text.RegularExpressions

let getContent(url: string) =
  async {
    let req = WebRequest.Create(url)
    let! res = req.AsyncGetResponse()
    let encStr = (res :?> System.Net.HttpWebResponse).CharacterSet
    let enc = Encoding.GetEncoding(encStr)
    let stream = res.GetResponseStream()
    let reader = new StreamReader(stream, enc)
    return reader.ReadToEnd()
  }

let countSymbols(url: string) =
  async {
//    printf "started %s" url
    let req = (WebRequest.Create(url))
    let! res = req.AsyncGetResponse()
    // note that len could be -1, if server doesn't send the length
    // e.g. google doen't
    // Possible solution is to use HttpClient class
    let len = res.ContentLength
    if len <> -1L
    then return len
    else
      let lenOfStr = (Async.RunSynchronously(getContent url)).Length
      return int64(lenOfStr)
  }

printfn "%s" <| Async.RunSynchronously(getContent "http://google.com").Substring(0, 100)

printfn "\n\n %i" <| Async.RunSynchronously(countSymbols "http://google.com")

printf "\n"

let url = "http://google.com"

Async.RunSynchronously <|
  async {
    let! page = getContent url
    let regex = new Regex("a href=\"http://[^< ]*\"")
    let matches = regex.Matches(page)
    let links = matches |> Seq.cast<Match> |> Seq.map (fun x -> x.Value.Substring(8, x.Value.Length - 9))
//    do Seq.iter (fun x -> printf "\n%s" x) links
//    do Seq.iter (fun x -> printf "Link %s has length %i" x (Async.RunSynchronously(countSymbols x))) links
    let workers = Seq.map (fun x -> countSymbols x) links
    let! results = Async.Parallel workers
    Array.iter (fun x -> printf "%i" x) results
    //let asyncs = Seq.map (fun x -> 
//      do printf "I'm alive %i" <| matches.Count
//      let! results = Async.Parallel asyncs
//      for i in 0..results.Length do printfn "lol"
    
    }

