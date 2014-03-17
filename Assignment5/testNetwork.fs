module TestNetwork

open LocalNetwork

let pcs = [new PC(new Linux(fakeGenerator), 1);
           new PC(new Windows(fakeGenerator), 2);
           new PC(new MacOS(fakeGenerator), 3)]

let connectPCs(plist: PC list) =
  match plist with
    | [] -> ()
    | x :: y :: ys -> x.connect y
    | [z] -> z.connect plist.Head

connectPCs pcs

pcs.Head.getOpSys.forceInfect 0.3

let printState(p: PC) = printf "%b\n" (p.getOpSys.isInfected)

let interact(p: PC) = p.startup

List.iter (printState) pcs

List.iter (interact) pcs

List.iter (printState) pcs

List.iter (interact) pcs

List.iter (printState) pcs
