#!/bin/bash

echo $PWD
#dotnet build

dotnet publish ./Bookmrqr.Service.csproj -o ./../../Publish/Service

echo 'start Service...'
dotnet ../../Publish/Service/Bookmrqr.Service.dll
