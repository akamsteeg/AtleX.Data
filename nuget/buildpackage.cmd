:: Create the output folder, if it doesn't exist
if not exist .\output md .\output

:: Detect MSBuild executable
msbuildexe=
for /D %%D in (%SYSTEMROOT%\Microsoft.NET\Framework\v4*) do set msbuild=%%D\MSBuild.exe
echo %msbuild%

%msbuild% ..\src\AtleX.Data.sln /p:Configuration=release

:: Build NuGet package
.\tools\nuget pack AtleX.Data.nuspec -OutputDirectory .\output\