module HelloWorld

open Xunit
open Retroscope

[<Fact>]
let ``Hello World`` () = async {
    let! scope = Scope.ConnectDefault
    
    let timestamp = System.DateTime.Now.ToString("HH:mm:ss")
    do! scope.Title($"Hello, World! ({timestamp})")

    return ()
}

[<Fact>]
let ``Draw Triangle`` () = async {
    let! scope = Scope.ConnectDefault
    
    let timestamp = System.DateTime.Now.ToString("HH:mm:ss")
    do! scope.Title($"Triangles ({timestamp})")

    let random = System.Random()
    let offsetX = random.Next(50, 501)
    let offsetY = random.Next(50, 501)

    let rainbowColors = [| "#ff0000"; "#ff7f00"; "#ffff00"; "#00ff00"; "#0000ff"; "#4b0082"; "#9400d3" |]
    let fillColor = rainbowColors.[random.Next(rainbowColors.Length)]
    let strokeColor = rainbowColors.[random.Next(rainbowColors.Length)]
    let lineColor = rainbowColors.[random.Next(rainbowColors.Length)]

    do! SVG.circle (50 + offsetX) (110 + offsetY) 8 |> SVG.withFill fillColor |> SVG.withStroke strokeColor |> SVG.withStrokeWidth 1 |> scope.Draw
    do! SVG.circle (10 + offsetX) (180 + offsetY) 8 |> SVG.withFill fillColor |> SVG.withStroke strokeColor |> SVG.withStrokeWidth 1 |> scope.Draw
    do! SVG.circle (90 + offsetX) (180 + offsetY) 8 |> SVG.withFill fillColor |> SVG.withStroke strokeColor |> SVG.withStrokeWidth 1 |> scope.Draw

    do! SVG.line (50 + offsetX) (110 + offsetY) (10 + offsetX) (180 + offsetY) |> SVG.withStroke lineColor |> SVG.withStrokeWidth 3 |> scope.Draw
    do! SVG.line (10 + offsetX) (180 + offsetY) (90 + offsetX) (180 + offsetY) |> SVG.withStroke lineColor |> SVG.withStrokeWidth 3 |> scope.Draw
    do! SVG.line (90 + offsetX) (180 + offsetY) (50 + offsetX) (110 + offsetY) |> SVG.withStroke lineColor |> SVG.withStrokeWidth 3 |> scope.Draw

    return ()
}

[<Fact>]
let ``Clear Screen`` () = async {
    let! scope = Scope.ConnectDefault
    
    let timestamp = System.DateTime.Now.ToString("HH:mm:ss")
    do! scope.Title("");
    do! scope.Clear()

    return ()
}