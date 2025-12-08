namespace Retroscope
module Scope =
    open System
    open System.Threading.Tasks
    open Microsoft.AspNetCore.SignalR.Client
    open Microsoft.Extensions.DependencyInjection
    open System.Text.Json.Serialization
    open SVG

    type Scope(hub: HubConnection) =
        member _.Title(title: string) : Async<unit> = async {
            do! hub.InvokeAsync("Title", title) |> Async.AwaitTask
        }
        
        member this.TitleAsync(title: string) : Task =
            this.Title title |> Async.StartAsTask :> Task

        member _.Draw(element : Element) : Async<unit> = async {
            do! hub.InvokeAsync("Draw", element) |> Async.AwaitTask
        }

        member this.DrawAsync(element : Element) : Task =
            this.Draw element |> Async.StartAsTask :> Task

        member _.Clear() : Async<unit> = async {
            do! hub.InvokeAsync "Clear" |> Async.AwaitTask
        }   

        member this.ClearAsync() : Task =
            this.Clear() |> Async.StartAsTask :> Task

    let Connect(baseUri: string) : Async<Scope> = async {
        let wasUriParsed, parsedBaseUri = Uri.TryCreate(baseUri, UriKind.Absolute)

        if not wasUriParsed then
            failwithf "The provided base URI '%s' is not a valid absolute URI." baseUri

        let hubUri = new Uri(parsedBaseUri, "/probe")

        let hub = 
            HubConnectionBuilder()
                .WithUrl(hubUri.ToString())
                .AddJsonProtocol(fun options ->
                    options.PayloadSerializerOptions.Converters.Add(JsonFSharpConverter())
                )
                .Build()

        do! hub.StartAsync() |> Async.AwaitTask
        return Scope(hub)
    }

    let ConnectDefault = Connect "https://localhost:44300"

    let ConnectAsync(baseUri: string) : Task<Scope> =
        Connect baseUri |> Async.StartAsTask

    let ConnectDefaultAsync() : Task<Scope> =
        ConnectDefault |> Async.StartAsTask
