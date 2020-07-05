$action = New-ScheduledTaskAction -Execute 'random_background.exe'

$trigger =  New-ScheduledTaskTrigger -AtLogOn

Register-ScheduledTask -Action $action -Trigger $trigger -TaskName  "Random Background" -Description "Execute random_background.exe every time you log in."

Write-Host -NoNewLine 'Press any key to continue...';
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');