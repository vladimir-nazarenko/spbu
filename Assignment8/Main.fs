module Refs

open System.Net
open System.IO
open Microsoft.FSharp.Control.WebExtensions
open System.Text
open System.Text.RegularExpressions

let getContent(url: string) =
  async {
    let req = WebRequest.Create(url)
    // bug in this line, try changing use to let or let to use
    let! res = req.AsyncGetResponse()
    use htres = res :?> System.Net.HttpWebResponse
    let encStr = htres.CharacterSet
    let enc = Encoding.GetEncoding(encStr)
    use stream = res.GetResponseStream()
    use reader = new StreamReader(stream, enc)
    let code = reader.ReadToEnd()
    return code
  }

let countSymbols(url: string) =
  async {
    let req = WebRequest.Create(url)
    use! res = req.AsyncGetResponse()
    let! code = getContent url
    let lenOfStr = code.Length
    return int64(lenOfStr)
  }

let countRefs url =
  async {
    let! page = getContent url
    let regex = new Regex(@"a href=""http://?(\w|((?!\s|'|"")\W))*""")
    let matches = regex.Matches(page)
    let links = matches |> Seq.cast<Match> |> Seq.map (fun x -> x.Value.Substring(8, x.Value.Length - 9))
    let workers = Seq.map (fun x -> countSymbols x) links
    let! results = Async.Parallel workers
    let res = Seq.zip links (Array.toSeq results)
    do Seq.iter (fun (l, c) -> printf "link %s has size %i" l c) res
  }

// Async.Start throws out output
ignore (Async.RunSynchronously <| countRefs "http://google.com")
