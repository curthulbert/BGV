
@echo off
if .%3==. echo DIR2BAT batfile runfile filemask [filemask] ...
if .%3==. goto end
echo Compiling...
md \dir2bat$
:: zip up target files...
pkzip25>nul -add -dir \dir2bat$\tempdir.zip %3 %4 %5 %6 %7 %8 %9
pkzip>nul \dir2bat$\dir2bat.zip \dir2bat$\tempdir.zip
:: create a loader...
echo> \dir2bat$\dir2bat.t1 @echo off
echo>>\dir2bat$\dir2bat.t1 md \dir2bat$
echo>>\dir2bat$\dir2bat.t1 pkunzip %%0 \dir2bat$
echo>>\dir2bat$\dir2bat.t1 pkzip25 -ext -dir \dir2bat$\tempdir \dir2bat$
echo>>\dir2bat$\dir2bat.t1 del \dir2bat$\tempdir.zip
echo>>\dir2bat$\dir2bat.t1 cls
echo>>\dir2bat$\dir2bat.t1 echo.
echo>>\dir2bat$\dir2bat.t1 echo Launching %2
echo>>\dir2bat$\dir2bat.t1 echo Waiting for application to end...
echo>>\dir2bat$\dir2bat.t1 start /w \dir2bat$\%2
echo>>\dir2bat$\dir2bat.t1 deltree /y \dir2bat$
echo>>\dir2bat$\dir2bat.t1 cls
:: add an EOF char...
copy \dir2bat$\dir2bat.t1 \dir2bat$\dir2bat.t2 /a>nul
:: append loader and zip and copy to target in current...
copy/b \dir2bat$\dir2bat.t2+\dir2bat$\dir2bat.zip %1>nul
:: clean up...
deltree /y \dir2bat$>nul
:end
