module Threads

open System.ComponentModel
open System
open System.Threading

let arrayLength = 1000000
let numberOfThreads = 10
let arrayOfOnes = Array.create arrayLength 1
let sum = ref 0

// sum pice of the array and return result
let rec sumFromToTR(array: int [], s: int, e: int) =
  match e - s with
    | x when x < 0 -> ()
    | _ -> sum := !sum + array.[s]; sumFromToTR(array, s + 1, e)

// sum array of size 1m in 100 threads
let sumArrayInThreads() =
  let workers: BackgroundWorker [] = Array.map (fun _ -> new BackgroundWorker()) [|0..numberOfThreads - 1|]
  // events which would be triggered, when worker is done
  let finishEvents = Array.map (fun _ -> new System.Threading.AutoResetEvent(false)) [|0..numberOfThreads - 1|]
  let assignWorker n =
    let start = n * (arrayLength / numberOfThreads)
    let fin = (n + 1) * (arrayLength / numberOfThreads) - 1
    (workers.[n]).DoWork.Add(
      fun args ->
        printf "Worker %i started\n" n;
        sumFromToTR(arrayOfOnes, start, fin);
        printf "Thread %i processed data\n" n)
    (workers.[n]).RunWorkerCompleted.Add(
      fun args ->
        printf "Worker %i finished\n %O" n args.Error;
        finishEvents.[n].Set() |> ignore)
  Array.iter assignWorker [|0..numberOfThreads - 1|]
  let startWorker(w: BackgroundWorker) = w.RunWorkerAsync()
  // wait for workers
  let activateEventLoop(e: System.Threading.AutoResetEvent) = e.WaitOne() |> ignore
  Array.iter startWorker workers
  Array.iter activateEventLoop finishEvents

sumArrayInThreads()

printf "result: %i\n" !sum
printf "Finished\n"





