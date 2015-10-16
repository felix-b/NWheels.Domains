@echo off

echo ABOUT TO MERGE NWHEELS PROJECTS INTO Wheels.Domains.sln
echo HIT ANY KEY TO PROCEED OR CTRL+C TO ABORT
echo - - - - - - - - - - - - - - - - - - - - - - - - - - - -

pause

cd /D %~dp0

..\..\..\NWheels\Source\Bin\Core\ntool.exe merge-solution --source ..\..\..\NWheels\Source\NWheels.sln --target ..\..\Source\NWheels.Domains.sln --projects nwheels-projects-to-merge.txt

pause
