#!/bin/bash

echo $PWD
#dotnet build

dotnet publish ./Bookmrqr.FinanceService.csproj -o ./../../Publish/FinanceService

echo 'start FinanceService...'
dotnet ../../Publish/FinanceService/Bookmrqr.FinanceService.dll