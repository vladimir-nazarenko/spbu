module T

open System.Net
open System.IO
open Microsoft.FSharp.Control.WebExtensions
open System.Text
open System.Text.RegularExpressions

let getContent(url: string) =
  async {
    let req = WebRequest.Create(url)
    use! res = req.AsyncGetResponse()
    let encStr = (res :?> System.Net.HttpWebResponse).CharacterSet
    let enc = Encoding.GetEncoding(encStr)
    use stream = res.GetResponseStream()
    let reader = new StreamReader(stream, enc)
    return reader.ReadToEnd()
  }

let countSymbols(url: string) =
  async {
    let req = WebRequest.Create(url)
    use! res = req.AsyncGetResponse()
    let! code = getContent url
    let lenOfStr = code.Length
    return int64(lenOfStr)
  }

//printfn "%s" <| Async.RunSynchronously(getContent "http://google.com").Substring(0, 100)

//printfn "\n\n %i" <| Async.RunSynchronously(countSymbols "http://google.com")

printf "\n"

//let urls = List.toSeq[|"http://google.com"; "http://habrahabr.ru"|]

let countRefs url =
  async {
    do printf "1"
    let! page = getContent url
    let regex = new Regex("a href=(\"http://([^\"]*\")|\'[^\']*\')")
    let matches = regex.Matches(page)
    let links = matches |> Seq.cast<Match> |> Seq.map (fun x -> x.Value.Substring(8, x.Value.Length - 9))
    Seq.iter(fun x -> printf "\n%s\n" x) links
    let workers = Seq.map (fun x -> countSymbols x) links
    printf "I was here"
    let! results = Async.Parallel workers
    let res = Seq.zip links (Array.toSeq results)
    do Seq.iter (fun (l, c) -> printf "link %s has size %i" l c) res
  }

ignore (Async.RunSynchronously <| countRefs "http://google.com")
