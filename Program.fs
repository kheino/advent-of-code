let print challenge result =
    printfn $"Challenge {challenge}: {result}"

[<EntryPoint>]
let main args =
    print "01.a" (Dec01.a())
    print "01.b" (Dec01.b())

    print "02.a" (Dec02.a())
    print "02.b" (Dec02.b())

    0
