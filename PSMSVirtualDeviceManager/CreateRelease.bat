@echo off

:: Remove old release folder
rmdir /S /Q ".\release"
mkdir ".\release"

:: Temporary copy all files to a new folder.
xcopy /S /I /Y ".\bin\x64\Release\*" ".\release\PSMSVirtualDeviceManager"

:: Create release zip file.
.\7zip\7za.exe a -tzip ".\release\PSMSVirtualDeviceManager.zip" -w ".\release\PSMSVirtualDeviceManager" 

:: Remove temp folder
:: rmdir /S /Q ".\release\PSMSVirtualDeviceManager"

pause