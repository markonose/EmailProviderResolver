# EmailProviderResolver 
MiniDig is an example implementation of a DNS lookup command line utility which uses the DnsClient library.
It is supposed to work similar to the well-known `dig` command line tool on Linux, with a lot fewer options of course.

## How to Build/Run it
To run it, open a command line windows, or bash, navigate to `/samples/EmailProviderResolver` and run `emailproviderresolver <email>`.

EmailProviderResolver  is targeting .NET5 and will run on any supported platform as long as the .NET5 SDK or runtime is installed.

## Examples
emailproviderresolver marko.nose@gmail.com

Example output: Google