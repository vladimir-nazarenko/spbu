module PhoneBook

exception InvalidState of int

// stores strings for messages
let strings = [
  ("initial", "Hello! Welcome to Phone Book Interface!\nPlease, enter the command(h to help)...");
  ("help", "Phone Book Interface Help.\nh - print this message\ne - exit\na - add record to database\nf - find record in database\ns - save database into file\nl - load database from file");
  ("invalidComand", "Invalid input, type h to get help");
  ("goodbye", "Thank you for using this app. Goodbye!")] |> Map.ofList

let printHelp db =
  printfn "%s" strings.["help"]
  (0, db)

let doExit db =
  printfn "%s" strings.["goodbye"]
  (-1, db)

let doAdd (db: Map<string, string>) =
  printf "Enter the name: "
  let name = System.Console.ReadLine()
  printf "Enter the phone number: "
  let number = System.Console.ReadLine()
  let modifiedDB = db.Add (name, number)
  printfn "Record succesfully added!"
  (1, modifiedDB)

let doFind db =
  printf "Enter the name: "
  let name = System.Console.ReadLine()
  match Map.tryFindKey (fun key value -> key = name) db with
    | None -> printfn "Nothing was found, sorry..."; (0, db)
    | Some key -> printfn "Number of %s is %s" key db.[key]; (0, db)

let doSave db =
  printfn "Enter filename, please"
  let path = System.Console.ReadLine()
  let fs = System.IO.File.CreateText path
  Map.iter (fun key value -> fs.WriteLine (key + "|" + value)) db
  fs.Close()
  printfn "Phone Book was succesfully written into %s" path
  (0, db)
  

let doLoad db =
  printfn "Enter filename, please"
  let name = System.Console.ReadLine()
//  if not <| System.IO.File.Exists name then printfn "Sorry, file doesn't exist" ; (2, db)
  let lines = List.ofArray <| System.IO.File.ReadAllLines name
  let processLine db: Map<string, string> -> str: string -> Map<string, string> =
    let parts = str.Split [|'|'|]
    db.Add (parts.[0], parts.[1])
  let modifiedDB = List.fold (processLine) db lines
  (1, modifiedDB)

let doPrintError db =
  printfn "%s" strings.["invalidCommand"]
  (0, db)

// interprets input symbol to function call
// each function returns number from -1 to 2
// -1 - exit
// 0 - all is OK, db unmodified
// 1 - all is OK, db modified
// 2 - command finished with errors
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
    | (0, _) -> init("continue", db)
    | (1, modifiedDB) | (2, modifiedDB) -> init("continue", modifiedDB)
    | (-1, _) -> ()
    | (x, _) -> raise (InvalidState x)
      
init("start", Map.empty)
