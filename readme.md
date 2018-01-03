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
dotnet new console -lang F# -n mechanic
echo obj > .gitignore
git add .
git commit -m "Create mechanic project"
```
