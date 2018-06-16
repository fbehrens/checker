# Checker

```bash
# install  load scripts
mono .paket/paket.exe install --generate-load-scripts --load-script-type fsx --load-script-framework netcoreapp2.0


.\.paket\paket.exe install --generate-load-scripts --load-script-type fsx --load-script-framework netcoreapp2.0

mono ./.paket/paket.exe install --generate-load-scripts --load-script-type fsx --load-script-framework net47

# test
dotnet run -p src/checkerTests/checkerTests.fsproj

# install as global tool

dotnet pack -c release -o nupkg
dotnet tool install --add-source ./nupkg -g check


```
