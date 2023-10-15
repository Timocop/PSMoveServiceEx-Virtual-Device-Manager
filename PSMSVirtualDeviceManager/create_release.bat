@echo off

REM Remove old release folder
rmdir /S /Q ".\release"
mkdir ".\release"

REM Temporary copy all files to a new folder.
xcopy /S /I /Y ".\bin\x64\Release\*" ".\release\PSMSVirtualDeviceManager"

REM Create release zip file.
.\7zip\7za.exe a -tzip ".\release\PSMSVirtualDeviceManager.zip" -w ".\release\PSMSVirtualDeviceManager" 

REM Remove temp folder
rmdir /S /Q ".\release\PSMSVirtualDeviceManager"

pause