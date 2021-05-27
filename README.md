# EmailProviderResolver 
A library used to figure out the email provider based on an email address

## Features

### General

* It can detect if the email provider is Google, Microsoft or other
* It uses [DnsClient.NET](https://github.com/MichaCo/DnsClient.NET) to query for DNS mx records 
* Dns queries use a caching mechanism to reduce latency in case 

## How to Build/Run it
To build this project, you must have the latest [.NET 5 SDK](https://dotnet.microsoft.com/download) installed.
Just clone the repository and open the solution in Visual Studio 2019.

To run a prebuilt sample on windows, open comand prompt, navigate to `/samples/EmailProviderResolver` and execute via command `emailproviderresolver <email>` from the appropriate runtime folder.

EmailProviderResolver is targeting .NET5 and will run on any supported platform as long as the .NET5 SDK or runtime is installed.

## Examples
`emailproviderresolver marko.nose@gmail.com`

Example output: `Google`