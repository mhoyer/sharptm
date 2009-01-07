@echo off
cls
SET CURRENT_DIR=%CD%
SET CURRENT_PATH=%PATH%

rem IF DEFINED MSBUILD_PATH GOTO Build
	echo Preparing environment variables

	REM Check if .NET Framework 3.5 is installed
	IF NOT EXIST %SystemRoot%\Microsoft.NET\Framework\v3.5\ (
		echo.
		echo BUILD SCRIPT ERROR: .NET Framework 3.5 is required to compile this project. 1>&2
		echo.
		EXIT /B 1
	)
	
	REM prepare msbuild
	IF NOT DEFINED MSBuildToolsPath SET MSBuildToolsPath=%SystemRoot%\Microsoft.NET\Framework\v3.5\
	IF NOT DEFINED MSBuildBinPath SET MSBuildBinPath=%MSBuildToolsPath%
	SET MSBUILD=%MSBuildToolsPath%\msbuild.exe

	REM prepare Mono
	REM SET MONO_PATH=%CURRENT_DIR%\tools\Mono\
	REM SET MSBuildBinPath=%MONO_PATH%lib\\mono\\2.0
	REM SET MSBuildToolsPath=%MONO_PATH%lib\\mono\\2.0

	REM prepare Rake
	SET RAKE_PATH=%CURRENT_DIR%\tools\rake\
	SET RUBY=%RAKE_PATH%\bin\ruby.exe
	SET RAKE=%RAKE_PATH%\bin\rake.rb

	REM define xUnit variables
	SET XUNIT_PATH=%CURRENT_DIR%\tools\xUnit\
	SET XUNIT_CONSOLE_RUNNER=%XUNIT_PATH%xunit.console.exe

	SET PATH=%PATH%;%MSBUILD_PATH%;%XUNIT_PATH%

:Build

"%RUBY%" "%RAKE%" %*
SET ERRORLEV=%ERRORLEVEL%

SET PATH=%CURRENT_PATH%

IF DEFINED TEAMCITY_BUILD_PROPERTIES_FILE GOTO Quit

REM Loop the build script if it is not run on TeamCity
SET CHOICE=nothing
echo (Q)uit, (Enter) runs the build again
SET /P CHOICE= 
IF /I "%CHOICE%"=="Q" GOTO :Quit

GOTO Build

:Quit

EXIT /B %ERRORLEV%