@ECHO OFF
ECHO Please select a build type to run.
ECHO.
ECHO - Full build (f) - Debug and Release configurations
ECHO - Quick build (q) - Debug configuration only
ECHO.
:question
SET /p buildChoice=Please enter F (for Full build) or Q (for Quick build): 
IF /i %buildChoice% == f (
ECHO.
%windir%\Microsoft.NET\Framework64\v4.0.30319\msbuild.exe ..\Scripts\StealFocus.MSTestExtensions.Build.proj /m /consoleloggerparameters:Verbosity=minimal /fileLogger /fileLoggerParameters:LogFile=SolutionBuild.msbuild.log;verbosity=diagnostic
) ELSE (
	IF /i %buildChoice% == q (
	ECHO.
	%windir%\Microsoft.NET\Framework64\v4.0.30319\msbuild.exe ..\Scripts\StealFocus.MSTestExtensions.Build.proj /m /p:BuildReleaseConfiguration=false;PopulateDropLocationForBuild=false /consoleloggerparameters:Verbosity=minimal /fileLogger /fileLoggerParameters:LogFile=SolutionBuild.msbuild.log;verbosity=diagnostic
	) ELSE (
		ECHO.
		ECHO Invalid selection
		ECHO.
		goto :question
	)
)
ECHO.
pause
