@ECHO off
SET argument_1 = %1
SET argument_2 = %2 & REM TODO : add a saving function.
SET program_path = "%cd%"
python "%cd%\parser.py" %1
python "%cd%\writer.py"
dotnet run
