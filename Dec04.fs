module Dec04

open System

type private Board =
    | Board of Set<int> seq

module private Board =
    let parse lines =
        let rows =
            lines |> Seq.map
                (fun (line: String) ->
                    line.Split (' ', StringSplitOptions.RemoveEmptyEntries)
                    |> Seq.map Int32.Parse
                )

        let cols = rows |> Seq.transpose

        [ rows; cols ]
        |> Seq.concat
        |> Seq.map Set.ofSeq
        |> Board

    let drop number board =
        match board with
        | Board sets ->
            sets
            |> Seq.map (Set.remove number)
            |> Board

    let hasEmptySet board =
        match board with
        | Board sets ->
            sets |> Seq.exists Set.isEmpty

    let sum board =
        match board with
        | Board sets ->
            sets
            |> Seq.fold Set.union Set.empty
            |> Set.fold (+) 0


module private Bingo =
    let rec draw numbers boards =
        match numbers with
        | [] -> None
        | head :: tail ->
            let boards' = boards |> List.map (Board.drop head)
            let winner = boards' |> List.tryFind Board.hasEmptySet

            match winner with
            | Some board -> Some (head, board)
            | None -> draw tail boards'

    let rec last numbers boards =
        match numbers with
        | [] -> None
        | head :: tail ->
            let boards' =
                boards
                |> List.map (Board.drop head)
                |> List.filter (not << Board.hasEmptySet)

            match boards' with
            | [ _ ] -> draw tail boards'
            | _ -> last tail boards'


let private loadInput() =
    let lines = Challenge.ReadAllLines "04"

    let numbers =
        lines[0].Split ','
        |> Seq.map Int32.Parse
        |> Seq.toList

    let boards =
        lines
        |> Seq.skip 2
        |> Seq.fold
            (fun (boards, buffer) line ->
                if (line = String.Empty) then
                    let board = Board.parse buffer
                    (board :: boards, [])
                else
                    (boards, line :: buffer)
            )
            ([], [])
        |> fst

    (numbers, boards)


let a() =
    let (numbers, boards) = loadInput()

    match Bingo.draw numbers boards with
    | Some (number, winner) -> number * Board.sum winner
    | None -> raise (Exception "no winner found")


let b() =
    let (numbers, boards) = loadInput()

    match Bingo.last numbers boards with
    | Some (number, winner) -> number * Board.sum winner
    | None -> raise (Exception "no winner found")
