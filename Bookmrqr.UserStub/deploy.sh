#!/bin/bash

echo $PWD
#dotnet build

dotnet publish ./Bookmrqr.UserStub.csproj -o ./../../Publish/UserStub

echo 'start UserStub...'
dotnet ../../Publish/UserStub/Bookmrqr.UserStub.dll