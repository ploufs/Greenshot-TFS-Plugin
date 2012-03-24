REM copy file to GreenShot folder
@echo off
cls

set GreenShotPath=%1
set PlugInPath="\Plugins\GreenshotTFSPlugin"

MD %GreenShotPath%%PlugInPath%
copy "bin\Release\GreenshotTFSPlugin.gsp" %GreenShotPath%%PlugInPath%\GreenshotTFSPlugin.gsp

MD %GreenShotPath%\Languages\%PlugInPath%
copy "Languages\*.*" %GreenShotPath%\Languages\%PlugInPath%