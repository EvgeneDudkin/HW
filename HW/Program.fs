open System
open System.IO
open System.Runtime.Serialization.Formatters.Binary
open System.ComponentModel
open System.Threading

//4HW 3)телефонный справочник
//let telBook =
//    let rec recHelpFunc (result : (string * string) list) =
//        let x = System.Console.ReadLine()
//        match x with
//        | "0" -> ()
//        | "1" -> printfn "Enter name and number"
//                 recHelpFunc ((System.Console.ReadLine(), System.Console.ReadLine())::result)
//        | "2" -> printfn "Enter name"
//                 let name = System.Console.ReadLine()
//                 (List.find(fun (a, b) -> a = name) >> snd >> printfn("%A")) result
//                 recHelpFunc result
//        | "3" -> printfn "Enter number"
//                 let number = System.Console.ReadLine()
//                 (List.find(fun (a, b) -> b = number) >> fst >> printfn("%A")) result
//                 recHelpFunc result
//        | "4" -> let fsOut = new FileStream("Data.dat", FileMode.Create)
//                 let formatter = new BinaryFormatter()
//                 formatter.Serialize(fsOut, box result)
//                 fsOut.Close()
//                 recHelpFunc result
//        | "5" -> let fsIn = new FileStream("Data.dat", FileMode.Open)
//                 let formatter = new BinaryFormatter()
//                 let res = formatter.Deserialize(fsIn) |> unbox<(string * string) list>
//                 fsIn.Close()
//                 printfn "%A" res
//                 recHelpFunc(res @ result)  
//        | _ -> printfn "Incorrect command"
//               recHelpFunc result
//    recHelpFunc []

//7 домашка 
let array = Array.create 1000000 1

let mutable res = Array.create 100 0
let mutable test = Array.create 100 0

let threadList = [for i in 0..99 -> new BackgroundWorker()]

for i in 0..99 do

    threadList.[i].DoWork.Add(fun _ -> res.[i] <- Array.sum array.[i * 10000..(i + 1)* 10000 - 1]
                                       test.[i] <- 1)
    threadList.[i].RunWorkerAsync()

while Array.sum test <> 100 do ()

printfn "%A" <| Array.sum res



