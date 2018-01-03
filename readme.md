# Dotnetcorescaffold

## VSCode-Tweaks
```json
{ "key": "shift+alt+enter", "command": "workbench.action.terminal.runSelectedText", "when": "editorTextFocus" },
{ "key": "ctrl+`", "command": "workbench.action.terminal.focus"},
{ "key": "ctrl+`", "command": "workbench.action.focusActiveEditorGroup", "when": "terminalFocus"}
```
* `Shift-Alt-Enter ` Send current line to terminal
* `Ctrl-Backtick` togle Focus Console Editor

## initialize on github

* Create project on github with MIT License
```
git clone git@github.com:fbehrens/Dotnetcorescaffold.git
cd Dotnetcorescaffold
ls
```

## Create a Dotnetcore Library

* Which templates are available ?
There is a list in the [wiki](https://github.com/dotnet/templating/wiki/Available-templates-for-dotnet-new) is a list,
and there is a website http://dotnetnew.azurewebsites.net


```
mkdir src
cd src
dotnet new console -lang F# -n mechanic
```
This creates a new dotnet core library project.

With the dotnetcommand, we can now compile our Project:
```
dotnet build src/mechanic/mechanic.fsproj

#or
cd src/mechanic
dotnet build 
``` 
Because it is a `library` it has no executable, so there is nothing to run

Let commit our changes into git into `git`
```
echo obj > .gitignore
git add .
git commit -m "Create mechanic project"
```
and then create a Console Project, which will be our TestLibrary
```
mkdir test 
cd test
dotnet new console -lang F# -n mechanicTests
```

Now lets create a solution file, which is a list of projects,
```
dotnet new sln --name mechanic
```
and add the two projects
```
dotnet sln mechanic.sln add src/mechanic/mechanic.fsproj   
dotnet sln mechanic.sln add test/mechanicTests/mechanicTests.fsproj
```

Now we can build the solution (of the two projects) from the project directory
```
dotnet build 
```

Actually it is better to do smaller commits, so its easier to follow what has changed.

Let create a project reference 
```
cd test/mechanicTests                                                                                                                   dotnet add reference ../../src/mechanic/mechanic.fsproj              
```
and run the console-project
```
dotnet run --project test/mechanicTests/mechanicTests.fsproj   
```
## init paket

Download `paket.bootstrapper`, `paket.targets` and `paket.restore.targets` from github into a new folder `.paket`
```
mkdir .paket
cp ~/Downloads/paket.* .paket

mono .paket/pakete.bootstrapper.exe # download latest paket
echo .paket.paket.exe >> .gitignore

mono .paket\paket.exe init
```

Edit `paket.dependencies`, create `test/MechanicTests/paket.references`
and install the dependencies with 
```
mono .paket\paket.exe install
```
