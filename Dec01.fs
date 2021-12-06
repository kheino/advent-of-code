module Dec01

open System

let a() =
    Challenge.ReadAllLines "01"
    |> Seq.map Int32.Parse
    |> Seq.fold
        (fun (count, prev) current ->
            let count' = if (current > prev) then count + 1 else count
            (count', current)
        )
        (0, Int32.MaxValue)
    |> fst

let b() =
    Challenge.ReadAllLines "01"
    |> Seq.map Int32.Parse
    |> Seq.fold
        (fun (count, (prev, prev', prevSum)) current ->
            if (prev' = -1) then
                (0, (current, prev, Int32.MaxValue))
            else
                let sum = prev + prev' + current
                let count' = if (sum > prevSum) then count + 1 else count
                (count', (current, prev, sum))
        )
        (0, (-1, -1, Int32.MaxValue))
    |> fst
