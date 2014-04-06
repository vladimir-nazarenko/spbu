module Threads

open System.ComponentModel
open System
open System.Threading

let arrayOfOnes = Array.create 1000000 1
let sum = ref 0

// sum pice of the array and return result
let rec sumFromToTR(array: int [], s: int, e: int) =
  match e - s with
    | x when x < 0 -> ()
    | _ -> sum := !sum + array.[s]; sumFromToTR(array, s + 1, e)

// sum array of size 1m in 100 threads
let sumArrayInThreads() =
  let workers: BackgroundWorker [] = Array.map (fun _ -> new BackgroundWorker()) [|0..99|]
  // events which would be triggered, when worker is done
  let finishEvents = Array.map (fun _ -> new System.Threading.AutoResetEvent(false)) [|0..99|]
  let assignWorker n =
    let start = n * 10000
    let fin = (n + 1) * 10000 - 1
    (workers.[n]).DoWork.Add(fun args -> sumFromToTR(arrayOfOnes, start, fin))
    (workers.[n]).RunWorkerCompleted.Add(fun args -> finishEvents.[n].Set() |> ignore)
  Array.iter assignWorker [|0..99|]
  let startWorker(w: BackgroundWorker) = w.RunWorkerAsync()
  // wait for workers
  let activateEventLoop(e: System.Threading.AutoResetEvent) = e.WaitOne() |> ignore
  Array.iter startWorker workers
  Array.iter activateEventLoop finishEvents

sumArrayInThreads()

printf "result: %i\n" !sum
printf "Finished\n"





