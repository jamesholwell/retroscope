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

    do! SVG.Line(0, 100, 100, 300) |> scope.Draw
    do! SVG.Line(100, 300, -100, 300) |> scope.Draw
    do! SVG.Line(-100, 300, 0, 100) |> scope.Draw

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