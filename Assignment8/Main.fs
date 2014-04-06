module Refs

open System.Net
open System.IO
open Microsoft.FSharp.Control.WebExtensions
open System.Text
open System.Text.RegularExpressions

let google = "http://habrahabr.ru"

let getContent(url: string) =
  async {
    let req = WebRequest.Create(url)
    let! res = req.AsyncGetResponse()
    let encStr = (res :?> System.Net.HttpWebResponse).CharacterSet
    let enc = Encoding.GetEncoding(encStr)
    let stream = req.GetResponse().GetResponseStream()
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
  
let reportSizesOfLinks(url: string) =
    async {
      let page = Async.RunSynchronously(getContent url)
      let regex = new Regex("a href=\"http://[^< ]*\"")
      let matches = regex.Matches(page)
      let links = matches |> Seq.cast<Match> |> Seq.map (fun x -> x.Value.Substring(8, x.Value.Length - 9))
      let asyncs = Seq.map (fun x -> 
//      do printf "I'm alive %i" <| matches.Count
//      let! results = Async.Parallel asyncs
//      for i in 0..results.Length do printfn "lol"
//      Seq.iter (fun x -> printf "\n%s" x) links
    }
    
ignore <| Async.RunSynchronously(reportSizesOfLinks google)
printf "\nWell"
