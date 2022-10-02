#!/bin/bash

echo $PWD
#dotnet build

sh ./Bookmrqr.Service/deploy.sh
sh ./Bookmrqr.Viewer/deploy.sh
sh ./Bookmrqr.UserStub/deploy.sh