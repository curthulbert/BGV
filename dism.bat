@echo off
if '%1=='Shell goto comshell
if exist %1 goto do_it

:help
echo Uses the DEBUG command to dump and disassemble. 
echo Rename EXE files before examining them!
echo Will delete the file AX.BAT if it exists.
echo.
echo Usage: %0 codefile [disfile]
echo LIST is used if disfile not specified
echo.
goto done

:do_it
command /e:5000 /c %0 Shell %1 %2
goto cleanup 

:comshell
shift

:: set this to your lister program...
set list=LIST

:: Get hex filelength in evar CX
echo R>d_$
echo Q>>d_$
debug %1<d_$>d_$$
find "CX"<d_$$>d_$.bat
echo set CX=%%5>ax.bat
call d_$.bat

echo Disassembling %CX% (hex) bytes...

:: Add FF to evar CX
:: (yes - batch file math!)
echo H %CX% FF>d_$
echo Q>>d_$
debug<d_$>d_$$
find "  "<d_$$>d_$$$
echo N d_$.bat>d_$
echo E 0100 'AX '>>d_$
echo RCX>>d_$
echo 3>>d_$
echo W>>d_$
echo Q>>d_$
debug<d_$>nul
type d_$$$>>d_$.bat
echo set CX=%%1>ax.bat
call d_$.bat

:: Dump and disassemble from 0100 to evar CX
echo D 100 %CX%>d_$
echo.>>d_$
echo U 100 %CX%>>d_$
echo.>>d_$
echo Q>>d_$
if '%2==' goto listit
debug %1<d_$>%2
goto done

:listit
debug %1<d_$>dism_out
call %list% dism_out
goto done

:: Clean up temp files
:cleanup
del d_$??
del d_$.bat
del ax.bat
if exist dism_out del dism_out
:done
