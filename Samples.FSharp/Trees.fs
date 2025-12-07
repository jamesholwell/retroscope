module Trees

open Xunit
open Retroscope

[<Fact>]
let ``Draw a tree`` () = async {
    let! scope = Scope.ConnectDefault
    
    let timestamp = System.DateTime.Now.ToString("HH:mm:ss")
    do! scope.Title($"Draw a tree ({timestamp})")
    do! scope.Clear()

    let rec drawBranch x y angle length thickness depth = async {
        if depth = 0 then
            return ()
        else
            let rad = angle * System.Math.PI / 180.0
            let x2 = x + length * System.Math.Cos(rad)
            let y2 = y + length * System.Math.Sin(rad)

            do! SVG.Line(x, y, x2, y2, "#fff", thickness) |> scope.Draw

            let newLength = length * 0.7
            let newThickness = thickness * 0.7

            do! drawBranch x2 y2 (angle - 20.0) newLength newThickness (depth - 1)
            do! drawBranch x2 y2 (angle + 20.0) newLength newThickness (depth - 1)
    }

    do! drawBranch 512 1000 -90.0 200.0 8.0 9

    return ()
}