module Challenge

open System.IO

let ReadAllLines challenge =
    File.ReadAllLines $"input/{challenge}.txt"
