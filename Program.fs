let print challenge result =
    printfn $"Challenge {challenge}: {result}"

[<EntryPoint>]
let main args =
    print "01.a" (Dec01.a())
    print "01.b" (Dec01.b())

    0
