@echo off

REM Remove old updater folder
rmdir /S /Q ".\updater"
mkdir ".\updater"

REM Input new version.
set /p version= "Please input a new version (e.g: 1.2.3.4 or 1.0) :" 
echo Generating new app_version.txt file with version: '%version%'.

REM Write version to file.
echo %version% > ".\updater\app_version.txt"

REM Temporary copy all files to a new folder.
xcopy /S /I /Y ".\bin\x64\Release\*" ".\updater\PSMSVirtualDeviceManager"

REM Create updater sfx file.
.\7zip\7za.exe a -sfx7z_sfx ".\updater\PSMSVirtualDeviceManagerUpdateSFX.dat" -w ".\updater\PSMSVirtualDeviceManager\*" 

REM Remove temp folder
rmdir /S /Q ".\updater\PSMSVirtualDeviceManager"

pause