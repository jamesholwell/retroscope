open System.Threading.Tasks
open Microsoft.AspNetCore.SignalR
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection
open System.Text.Json.Serialization

type IScopeClient =
    abstract member Title : title:string -> Task

    abstract member Draw : svg:string -> Task

    abstract member Clear : unit -> Task

type ScopeHub() =
    inherit Hub<IScopeClient>()

type IProbeClient = 
    abstract member Title : title:string -> Task

    abstract member Draw : element:Retroscope.SVG.Element -> Task

    abstract member Clear : unit -> Task

type ProbeHub(scopeHubContext: IHubContext<ScopeHub, IScopeClient>) =
    inherit Hub<IProbeClient>()

    member this.Title(title : string) =
        scopeHubContext.Clients.All.Title(title)

    member this.Draw(element:Retroscope.SVG.Element) =
        let svg = Retroscope.SVG.toSvgString element
        let wrappedSvg = "<svg xmlns=\"http://www.w3.org/2000/svg\">" + svg + "</svg>"
        scopeHubContext.Clients.All.Draw(wrappedSvg)

    member this.Clear() =
        scopeHubContext.Clients.All.Clear()

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)
    
    builder.Services
        .AddSignalR()
        .AddJsonProtocol(fun options ->
            options.PayloadSerializerOptions.Converters.Add(JsonFSharpConverter())
        ) |> ignore

    let app = builder.Build()

    // serve static assets
    app.UseDefaultFiles() |> ignore
    app.UseStaticFiles() |> ignore

    // serve SignalR
    app.MapHub<ProbeHub>("/probe") |> ignore
    app.MapHub<ScopeHub>("/scope") |> ignore

    app.Run()

    0 // exit
