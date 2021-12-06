module Dec02

open System

type private Command =
    | Forward of int
    | Down of int
    | Up of int

module private Command =
    let parse (s: string) =
        match s.Split ' ' with
        | [| "forward"; value |] -> Forward (value |> Int32.Parse)
        | [| "down"; value |] -> Down (value |> Int32.Parse)
        | [| "up"; value |] -> Up (value |> Int32.Parse)
        | _  -> raise (NotImplementedException s)

let a() =
    Challenge.ReadAllLines "02"
    |> Seq.map Command.parse
    |> Seq.fold
        (fun (hpos, depth) cmd ->
            match cmd with
            | Forward v -> (hpos + v, depth)
            | Down v -> (hpos, depth + v)
            | Up v -> (hpos, depth - v)
        )
        (0, 0)
    |> fun (hpos, depth) -> hpos * depth

let b() =
    Challenge.ReadAllLines "02"
    |> Seq.map Command.parse
    |> Seq.fold
        (fun (hpos, depth, aim) cmd ->
            match cmd with
            | Forward v -> (hpos + v, depth + (aim * v), aim)
            | Down v -> (hpos, depth, aim + v)
            | Up v -> (hpos, depth, aim - v)
        )
        (0, 0, 0)
    |> fun (hpos, depth, _) -> hpos * depth
