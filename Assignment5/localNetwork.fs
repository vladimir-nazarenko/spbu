module LocalNetwork

type OSName =
  | Linux
  | Windows
  | MacOS

let fakeGenerator() =
  let mutable numbers = [0.1; 0.5; 0.9]
  numbers <- numbers.Tail @ [numbers.Head]
  numbers.Head

let authGenerator() =
  let rand = System.Random().Next(1, 100)
  (/) <|| (float rand, float 100)

[<AbstractClass>]
type OS() =
  let mutable mPowerOfVirus = 0.0
  let mutable virusIsActive = false
  abstract member name: string
  // varies from 0.0 to 1.0
  abstract member reliability: float with get
  abstract member reboot: unit
  abstract member isInfected: bool
  abstract member tryInfect: float -> unit
  abstract member forceInfect: float -> unit
  member o.power = mPowerOfVirus
  default o.reboot = if mPowerOfVirus > 0.0 then virusIsActive <- true
  default o.isInfected = mPowerOfVirus > 0.0 && virusIsActive
  default o.tryInfect power =
    if power > o.reliability then mPowerOfVirus <- power
  default o.forceInfect power =
    mPowerOfVirus <- power

type Linux(rand: unit -> float) =
  inherit OS()
  override l.reliability = 0.7 * rand() 
  override l.name = "Linux"

type MacOS(rand: unit -> float) =
  inherit OS()
  override m.reliability = 0.6 * rand()
  override l.name = "MacOS"

type Windows(rand: unit -> float) =
  inherit OS()
  override w.reliability = 0.5 * rand()
  override w.name = "Windows"
  
type PC(opsys: OS, num: int) =
  let mutable mConnected = []
  member p.connected = mConnected
  member p1.connect(p2: PC) = mConnected <- p2 :: mConnected
  member p.startup: unit =
    let handlePC (power: float, p: PC) =
      let o: OS = p.getOpSys
      o.tryInfect power
    if opsys.isInfected then
      List.iter (fun pc -> handlePC(opsys.power, pc)) mConnected
    opsys.reboot
  member p.getOpSys = opsys
  override p.ToString() = System.String.Format("PC number {0} with OS {1}\n", num, opsys.name)
  
