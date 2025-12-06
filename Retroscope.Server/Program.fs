open System
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)
    let app = builder.Build()

    // serve static assets
    app.UseDefaultFiles() |> ignore
    app.UseStaticFiles() |> ignore

    app.Run()

    0 // exit
