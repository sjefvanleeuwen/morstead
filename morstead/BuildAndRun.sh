#/bin/bash

dotnet restore
dotnet build --no-restore

# Run the 2 console apps in different windows

dotnet run --project ./src/Vs.Orleans.SiloHost --no-build & 
sleep 10
dotnet run --project ./src/Vs.Rules.OrleansTestClient --no-build &
