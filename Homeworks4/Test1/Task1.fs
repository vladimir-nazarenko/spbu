module Task1

let meanSin xs =
  let sinuses = List.map (fun e -> sin(e)) xs
  (List.fold (+) 0.0 sinuses) / float(xs.Length)

let meanSin1 xs =
  let rec sumSinTR xs sum =
    match xs with
      | [] -> sum
      | x :: xs' -> sumSinTR xs' (sum + sin(x))
  (sumSinTR xs 0.0) / float(xs.Length)
