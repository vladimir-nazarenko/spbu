module HashT

type HashTable<'a when 'a: equality>(hashFunc: 'a -> int) =
  let tableSize = 10000
  let table = Array.init tableSize (fun i -> List.empty<'a>)
  let index(elem: 'a) = (hashFunc elem) % tableSize
  member ht.Insert(elem: 'a) =
    table.[index(elem)] <- elem :: table.[index(elem)]
  member ht.Exists(elem: 'a) =
    List.exists ((=) elem) table.[index(elem)]
  member ht.Remove(elem: 'a) =
    table.[index(elem)] <- List.filter ((<>) elem) table.[index(elem)]
    
let naiveHash(s: string) =
  s.Length

let strHash = new HashTable<string>(naiveHash)

strHash.Insert("abc")

printf "Insert test: %b\n" (strHash.Exists("abc") = true)

printf "Exists test: %b\n" (strHash.Exists("abd") = false)

strHash.Remove("abc")

printf "Remove test: %b\n" (strHash.Exists("abc") = false)


