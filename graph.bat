@ECHO off

IF [%1]==[] GOTO Error
IF [%2]==[] GOTO Option_One
IF NOT [%2]==[] GOTO Option_Two

:Option_One
python %CONSOLE_GRAPHER%\parser.py %1
python %CONSOLE_GRAPHER%\scriber.py
"F:\tests\2020-07-26_Console_Grapher\bin\Debug\netcoreapp3.1\2020-07-26_Console_Grapher.exe"
GOTO End

:Option_Two
python %CONSOLE_GRAPHER%\parser.py %1
python %CONSOLE_GRAPHER%\scriber.py
"F:\tests\2020-07-26_Console_Grapher\bin\Debug\netcoreapp3.1\2020-07-26_Console_Grapher.exe" %2
GOTO End

:Error
ECHO "No input!"

:End 