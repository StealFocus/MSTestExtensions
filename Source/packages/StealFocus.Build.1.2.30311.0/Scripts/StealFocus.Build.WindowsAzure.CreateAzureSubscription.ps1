param
(
	[string]$currentDirectory = $(throw "currentDirectory is a required parameter"),
	[string]$subscriptionName = $(throw "subscriptionName is a required parameter"),
	[string]$subscriptionId = $(throw "subscriptionId is a required parameter"),
	[string]$managementCertificateThumbprint = $(throw "managementCertificateThumbprint is a required parameter")
)

Import-Module $currentDirectory\StealFocus.Build.WindowsAzure.psm1 -DisableNameChecking

Create-AzureSubscription -subscriptionName $subscriptionName -subscriptionId $subscriptionId -managementCertificateThumbprint $managementCertificateThumbprint
