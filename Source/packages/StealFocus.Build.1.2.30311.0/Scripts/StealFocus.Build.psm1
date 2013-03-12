function Check-Prerequisites
{
	param(
			[bool]$azurePublishActionRequired = $(throw '$azurePublishActionRequired is a required parameter')
		)

	if ($azurePublishActionRequired)
	{
		$programFilesX86EnvironmentVariable = Get-Childitem env:"ProgramFiles(x86)"
		$azureModulePath = $programFilesX86EnvironmentVariable.Value + "\Microsoft SDKs\Windows Azure\PowerShell\Azure\Azure.psd1"
		Import-Module $azureModulePath -erroraction SilentlyContinue
		$azureModule = Get-Module Azure -erroraction SilentlyContinue
		if ($azureModule -eq $null)
		{
			Write-Error "To run this script the 'Windows Azure PowerShell' component is required. Please download and install via the Microsoft Web Platform installer ('http://www.microsoft.com/web/downloads/platform.aspx')."
			Exit
		}
		Write-Host "Check for 'Windows Azure PowerShell' succeeded."
	}
	else
	{
		Write-Host "No 'AzureSubscription' entries in configuration, so skipping check for 'Windows Azure PowerShell'."
	}
}