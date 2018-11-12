open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection

open Giraffe
open MongoDB.Driver

open Todos.HttpHandlers.TodoHttp
open Todos.Implementations.InMongo
open Todos.Models

let routes = 
    choose [ handlers ]

let configureApp(app: IApplicationBuilder) = 
    app.UseGiraffe routes

let configureServices(services: IServiceCollection) =
    let mongo = MongoClient (Environment.GetEnvironmentVariable "MONGO_URL")
    let db = mongo.GetDatabase "todos"
    services.AddGiraffe() |> ignore
    services.AddTodoInMongo(db.GetCollection<Todo>("todos")) |> ignore

[<EntryPoint>]
let main args =
    WebHostBuilder()
        .UseKestrel()
        .Configure(Action<IApplicationBuilder> configureApp)
        .ConfigureServices(configureServices)
        .Build()
        .Run()
    0
