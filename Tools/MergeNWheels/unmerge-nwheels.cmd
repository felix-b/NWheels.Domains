@echo off

echo ABOUT TO UNMERGE NWHEELS PROJECTS FROM Wheels.Domains.sln
echo HIT ANY KEY TO PROCEED OR CTRL+C TO ABORT
echo - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

pause

cd /D %~dp0

..\..\..\NWheels\Source\Bin\Core\ntool.exe merge-solution --source --unmerge ..\..\..\NWheels\Source\NWheels.sln --target ..\..\Source\NWheels.Domains.sln --projects nwheels-projects-to-merge.txt

pause
