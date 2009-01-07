@echo off
cls
SET CURRENT_DIR=%CD%
SET CURRENT_PATH=%PATH%

IF DEFINED MSBUILD_PATH GOTO Build
	echo Preparing environment variables
	
	REM prepare msbuild
	SET MSBUILD_PATH=c:\Windows\Microsoft.NET\Framework\v3.5\
	SET MSBUILD=%MSBUILD_PATH%\msbuild.exe

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

	SET PATH=%PATH%;%MSBUILD_PATH%;%MONO_PATH%;%XUNIT_PATH%

:Build

"%RUBY%" "%RAKE%" %*

SET PATH=%CURRENT_PATH%

SET CHOICE=nothing
echo (Q)uit, (Enter) runs the build again
SET /P CHOICE= 
IF /I "%CHOICE%"=="Q" GOTO :Quit

GOTO Build

:Quit
