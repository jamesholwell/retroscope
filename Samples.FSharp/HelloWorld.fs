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