open System.Threading.Tasks
open Microsoft.AspNetCore.SignalR
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection

type IScopeClient =
    abstract member Title : title:string -> Task

    abstract member Draw : drawable:string -> Task

    abstract member Clear : unit -> Task

type ScopeHub() =
    inherit Hub<IScopeClient>()

    member this.Title(title : string) =
        this.Clients.All.Title(title)

    member this.Draw(drawable : string) =
        this.Clients.All.Draw(drawable)

    member this.Clear() =
        this.Clients.All.Clear()

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)
    builder.Services.AddSignalR() |> ignore

    let app = builder.Build()

    // serve static assets
    app.UseDefaultFiles() |> ignore
    app.UseStaticFiles() |> ignore

    // serve SignalR
    app.MapHub<ScopeHub>("/hub") |> ignore

    app.Run()

    0 // exit
