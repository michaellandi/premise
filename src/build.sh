#!/bin/bash
dotnet pack --configuration release ./Premise.Data/project.json
dotnet pack --configuration release ./Premise.Data.EntityFramework/project.json
dotnet pack --configuration release ./Premise.Web/project.json
