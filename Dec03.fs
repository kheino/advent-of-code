module Dec03

open System

let flip (bits: int seq) =
    bits
    |> Seq.map ((*) -1 >> (+) 1)

let bitsToInt (bits: int seq) =
    bits
    |> String.Concat
    |> fun s -> Convert.ToInt32 (s, 2)

let bitClamp value = value |> max 0 |> min 1

let a() =
    Challenge.ReadAllLines "03"
    |> Seq.fold
        (fun weights bits ->
            bits.ToCharArray()
            |> Seq.map (Char.ToString >> Int32.Parse >> (*) 2 >> (+) -1)
            |> Seq.zip weights
            |> Seq.map ((<||) (+))
        )
        (Array.create 12 0)
    |> Seq.map bitClamp
    |> fun gamma -> [ gamma; flip gamma ]
    |> Seq.map bitsToInt
    |> Seq.reduce (*)

let b() =
    let rec find weightFun evenBit bitPos (numbers: int[] list) =
        match numbers with
        | [ single ] -> single
        | _ ->
            let targetBit =
                numbers
                |> Seq.fold
                    (fun weight bits -> bits[bitPos] |> weightFun |> (+) weight)
                    0
                |> fun w -> if (w = 0) then evenBit else bitClamp w

            numbers
            |> List.filter (fun bits -> bits[bitPos] = targetBit)
            |> find weightFun evenBit (bitPos + 1)

    let numbers =
        Challenge.ReadAllLines "03"
        |> Seq.map (fun bits -> bits.ToCharArray() |> Array.map (Char.ToString >> Int32.Parse))
        |> Seq.toList

    let generator = numbers |> find ((*) 2 >> (+) -1) 1 0
    let scrubber = numbers |> find ((*) 2 >> (-) 1) 0 0

    [ generator; scrubber ]
    |> Seq.map bitsToInt
    |> Seq.reduce (*)
