#requires -Version 7.0
# 1. Define target (12:30 PM / 12:30:00)
# Change -Hour to 0 for 12:30 AM
$targetTime = Get-Date -Hour 12 -Minute 30 -Second 0

# 2. Immediate Exit Condition
# If it's already 12:30:01 or later today, do nothing and quit.
if ((Get-Date) -ge $targetTime) {
    Write-Host "The target time (12:30) has already passed. Exiting..."
    exit
}

# 3. Calculation & Idle Wait
$secondsToWait = ($targetTime - (Get-Date)).TotalSeconds
Write-Host "Waiting $secondsToWait seconds until 12:30..."
Start-Sleep -Seconds $secondsToWait

# 4. Inline Sleep Command (Direct .NET Call)
# Arguments: (PowerState, Force, DisableWake)
# Suspend = Sleep, False = Don't force close apps, False = Allow wake timers
Add-Type -AssemblyName System.Windows.Forms
[System.Windows.Forms.Application]::SetSuspendState([System.Windows.Forms.PowerState]::Suspend, $false, $false)