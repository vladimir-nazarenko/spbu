module T

open System.Net
open System.IO
open Microsoft.FSharp.Control.WebExtensions
open System.Text
open System.Text.RegularExpressions

let getContent(url: string) =
  async {
    printf "----------1"
    let req = WebRequest.Create(url)
    printf "-------------1.2"
    // bug in this line, try changing use to let or let to use
    let! res = req.AsyncGetResponse()
    printf "------------1.3"
    use htres = res :?> System.Net.HttpWebResponse
    printf "-----------1.4"
    let encStr = htres.CharacterSet
    printf "-----------1.5"
    let enc = Encoding.GetEncoding(encStr)
    printf "----------2"
    use stream = res.GetResponseStream()
    use reader = new StreamReader(stream, enc)
    let code = reader.ReadToEnd()
    printf "-----------3"
    return code
  }

let countSymbols(url: string) =
  async {
    printf "----------4"
    let req = WebRequest.Create(url)
    use! res = req.AsyncGetResponse()
    let! code = getContent url
    let lenOfStr = code.Length
    printf "------------5"
    return int64(lenOfStr)
  }

let countRefs url =
  async {
    printf "---------------6"
    let! page = getContent url
    let regex = new Regex(@"a href=""http://?(\w|((?!\s|'|"")\W))*""")
    let matches = regex.Matches(page)
    let links = matches |> Seq.cast<Match> |> Seq.map (fun x -> x.Value.Substring(8, x.Value.Length - 9))
    printf "-----------------7"
//    Seq.iter(fun x -> printf "\n%s\n" x) links
    let workers = Seq.map (fun x -> countSymbols x) links
    printf "--------------8"
    let! results = Async.Parallel workers
    printf "-------------------9"
    let res = Seq.zip links (Array.toSeq results)
    do Seq.iter (fun (l, c) -> printf "link %s has size %i" l c) res
//    do Array.iter(fun x -> printf "\n %i" x) results
  }

// Async.Start throws out output
ignore (Async.RunSynchronously <| countRefs "http://google.com")
