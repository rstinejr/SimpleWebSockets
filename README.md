# SimpleWebSockets
Minimal example of WebSockets in .NET Core

This project lays out a minimal client and server that illustrate the use of WebSockets
in .NET Core. It borrows heavily from the AspNet GitHup project
[EchoApp](https://github.com/aspnet/WebSockets/tree/dev/samples/EchoApp) and
ITQ blog post
[NET 4.5 WebSocket client without a browser](http://itq.nl/net-4-5-websocket-client-without-a-browser/).

## Build Notes

The project was build on 64-bit Windows 10, using dotnet 1.1.0 and VisualStudio 2017. It was also built and run on 
64-bit CentOS 7.1.

## To Build

First, install dotnet 1.1.

Download the project by opening a command window and executing 

`git clone https://github.com/rstinejr/SimpleWebSockets.git`

To build WebSocketsServer from the commandline, 
1. `cd SimpleWebSockets\WebSocketsServer`
2. `dotnet restore`
3. `dotnet build`

Expected output from the *build* command is:

```
Build succeeded.
    0 Warning(s)
        0 Error(s)
```

While still in the *WebSocketsServer* directory, start the server by executing 
`dotnet run`. On success, the daemon's console output will end with

```
Now listening on: http://localhost:5000
Application started. Press Ctrl+C to shut down.
```

To build WebSocketsClient from the commandine, open a new command window and
1. `cd SimpleWebSockets\WebSocketsClient`
2. `dotnet restore`
3. `dotnet build`

The output from the *build* command should end with

```
Build succeeded.
    0 Warning(s)
        0 Error(s)
```

To upload, e.g., *Payload.exe* to the WebSocketServer, enter `dotnet run Payload.exe`.

Note that the client reports writing 8704 bytes, and the server reports receiving the same 
number of bytes.



