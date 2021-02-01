@echo off
:: Batch to evaluate command line and print results
:: Requires QBASIC (included with MSDOS 5 and 6)
:: Uses the 'TEMP' directory if defined
if '%1==' echo No formula specified!
if '%1==' goto end
> %temp%\cal$.bas echo :on error goto errr
>>%temp%\cal$.bas echo print %1 %2 %3 %4 %5 %6 %7 %8 %9
>>%temp%\cal$.bas echo system
>>%temp%\cal$.bas echo errr: print "ERROR":system
qbasic /run %temp%\cal$.bas
del %temp%\cal$.bas
:end
