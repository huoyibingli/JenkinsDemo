@echo off
echo -----------------------------------------------
echo WebDemo����������
echo ----------------------------------------------

%~d0

cd %~dp0

dotnet publish WebDemo\WebDemo.csproj -c Release -o Release\JenkinsDemo  --self-contained false


pause