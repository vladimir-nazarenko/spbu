module PhoneBook

exception InvalidState of int

type Message =
  | DBModified
  | DBUnmodified
  | NeedExit
  | Error

// stores strings for messages
let strings = [
  ("initial", "Hello! Welcome to Phone Book Interface!\nPlease, enter the command(h to help)...");
  ("help", "Phone Book Interface Help.\nh - print this message\ne - exit\na - add record to database\nf - find record in database\ns - save database into file\nl - load database from file");
  ("invalidCommand", "Invalid input, type h to get help");
  ("goodbye", "Thank you for using this app. Goodbye!")] |> Map.ofList

let printHelp db =
  printfn "%s" strings.["help"]
  (DBUnmodified, db)

let doExit db =
  printfn "%s" strings.["goodbye"]
  (NeedExit, db)

let doAdd (db: Map<string, string>) =
  printf "Enter the name: "
  let name = System.Console.ReadLine()
  printf "Enter the phone number: "
  let number = System.Console.ReadLine()
  let modifiedDB = db.Add (name, number)
  printfn "Record succesfully added!"
  (DBModified, modifiedDB)

let doFind db = //db:Map<string, string> =
  printf "Enter the name: "
  let name = System.Console.ReadLine()
  match Map.tryFind name db with
    | None -> printfn "Nothing was found, sorry..."; (DBUnmodified, db)
    | Some value -> printfn "Number of %s is %s" name value; (DBUnmodified, db)

let doSave db =
  printfn "Enter filename, please"
  let path = System.Console.ReadLine()
  let fs = System.IO.File.CreateText path
  Map.iter (fun key value -> fs.WriteLine (key + "|" + value)) db
  fs.Close()
  printfn "Phone Book was succesfully written into %s" path
  (DBUnmodified, db)
  

let doLoad db =
  printfn "Enter filename, please"
  let name = System.Console.ReadLine()
  match System.IO.File.Exists name with
    | false -> printfn "Sorry, file doesn't exist" ; (Error, db)
    | true ->
      let lines = List.ofArray <| System.IO.File.ReadAllLines name
      let processLine (db: Map<string, string>, str: string) =
        let parts = str.Split [|'|'|]
        db.Add (parts.[0], parts.[1])
      let modifiedDB = List.fold (fun acc elem -> processLine(acc, elem)) db lines
      (DBModified, modifiedDB)

let doPrintError db =
  printfn "%s" strings.["invalidCommand"]
  (DBModified, db)

// interprets input symbol to function call
let handleCommand command db =
  match command with
    | "h" -> printHelp(db)
    | "e" -> doExit(db)
    | "a" -> doAdd(db)
    | "f" -> doFind(db)
    | "s" -> doSave(db)
    | "l" -> doLoad(db)
    | _ -> doPrintError(db)

// loop for command input
// Currently supported two statuses - "start" and "continue"
let rec init (status:string, db:Map<string, string>) =
  if status = "start" then printfn "%s" strings.["initial"]
  printf ">>"
  let input = System.Console.ReadLine()
  match handleCommand <| input <| db with
    | (DBUnmodified, _) | (Error, _) -> init("continue", db)
    | (DBModified, mdb) -> init("continue", mdb)
    | (NeedExit, _) -> ()
   // | (Error, _) -> raise (InvalidState x)
      
init("start", Map.empty)
