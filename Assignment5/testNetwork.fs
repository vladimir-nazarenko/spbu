module TestNetwork

open LocalNetwork

let pcs = [new PC(new Linux(fakeGenerator), 1);
           new PC(new Windows(fakeGenerator), 2);
           new PC(new MacOS(fakeGenerator), 3)]

let connectPCs(plist: PC list) =
  match plist with
    | [] -> ()
    | x :: y :: ys -> x.Connect y
    | [z] -> z.Connect plist.Head

connectPCs pcs

pcs.Head.GetOpSys.ForceInfect 0.3

let printState(p: PC) = printf "%b\n" (p.GetOpSys.IsInfected)

let interact(p: PC) = p.Startup

List.iter (printState) pcs

List.iter (interact) pcs

List.iter (printState) pcs

List.iter (interact) pcs

List.iter (printState) pcs
