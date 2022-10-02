#!/bin/bash

echo $PWD
#dotnet build

dotnet publish ./Bookmrqr.Viewer.csproj -o ./../../Publish/Viewer

echo 'start Viewer...'
dotnet ../../Publish/Viewer/Bookmrqr.Viewer.dll