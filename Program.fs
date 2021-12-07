let print challenge result =
    printfn $"Challenge {challenge}: {result}"

[<EntryPoint>]
let main args =
    print "01.a" (Dec01.a())
    print "01.b" (Dec01.b())

    print "02.a" (Dec02.a())
    print "02.b" (Dec02.b())

    print "03.a" (Dec03.a())
    print "03.b" (Dec03.b())

    print "04.a" (Dec04.a())
    print "04.b" (Dec04.b())

    0
