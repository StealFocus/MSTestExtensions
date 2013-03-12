param(
		[string]$currentDirectory = $(throw '$currentDirectory is a required parameter'),
		[string]$azurePublishActionRequired = $(throw '$azurePublishActionRequired is a required parameter')
	)

[bool]$azurePublishActionRequiredValue = [System.Convert]::ToBoolean($azurePublishActionRequired)

Import-Module $currentDirectory\StealFocus.Build.psm1 -DisableNameChecking

Check-Prerequisites -azurePublishActionRequired $azurePublishActionRequiredValue
