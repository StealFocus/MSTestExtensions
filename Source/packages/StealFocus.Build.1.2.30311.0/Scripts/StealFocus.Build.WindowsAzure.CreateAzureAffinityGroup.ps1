param
(
	[string]$currentDirectory = $(throw "currentDirectory is a required parameter"),
	[string]$subscriptionName = $(throw "subscriptionName is a required parameter"),
	[string]$affinityGroupName = $(throw "affinityGroupName is a required parameter"),
	[string]$affinityGroupLabel = $(throw "affinityGroupLabel is a required parameter"),
	[string]$affinityGroupLocation = $(throw "affinityGroupLocation is a required parameter")
)

Import-Module $currentDirectory\StealFocus.Build.WindowsAzure.psm1 -DisableNameChecking

Create-AzureAffinityGroup -subscriptionName $subscriptionName -affinityGroupName $affinityGroupName -affinityGroupLabel $affinityGroupLabel -affinityGroupLocation $affinityGroupLocation
