module LocalNetwork

let mutable numbers = [0.1; 0.5; 0.9]

let fakeGenerator() =
  numbers <- numbers.Tail @ [numbers.Head]
  numbers.Head

let authGenerator() =
  let rand = System.Random().Next(1, 100)
  (/) <|| (float rand, float 100)

[<AbstractClass>]
type OS() =
  let mutable mPowerOfVirus = 0.0
  let mutable virusIsActive = false
  abstract member Name: string
  // varies from 0.0 to 1.0
  abstract member Reliability: float with get
  member o.Power = mPowerOfVirus
  member o.IsInfected = mPowerOfVirus > 0.0 && virusIsActive
  member o.ForceInfect power = mPowerOfVirus <- power
  member o.Reboot = if mPowerOfVirus > 0.0 then virusIsActive <- true
  member o.TryInfect power = if power > o.Reliability then mPowerOfVirus <- power

type Linux(rand: unit -> float) =
  inherit OS()
  override l.Reliability = 0.7 * rand() 
  override l.Name = "Linux"

type MacOS(rand: unit -> float) =
  inherit OS()
  override m.Reliability = 0.6 * rand()
  override l.Name = "MacOS"

type Windows(rand: unit -> float) =
  inherit OS()
  override w.Reliability = 0.5 * rand()
  override w.Name = "Windows"
  
type PC(opsys: OS, num: int) =
  let mutable mConnected = []
  member p.Connected = mConnected
  member p1.Connect(p2: PC) = mConnected <- p2 :: mConnected
  member p.Startup: unit =
    let handlePC (power: float, p: PC) =
      let o: OS = p.GetOpSys
      o.TryInfect power
    if opsys.IsInfected then
      List.iter (fun pc -> handlePC(opsys.Power, pc)) mConnected
    opsys.Reboot
  member p.GetOpSys = opsys
  override p.ToString() = System.String.Format("PC number {0} with OS {1}\n", num, opsys.Name)
  
