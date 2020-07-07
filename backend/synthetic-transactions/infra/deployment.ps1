function New-CrgarApimDeployment
{
    [CmdletBinding()]
    param()

    $Locations = @("eastus", "westus", "westeurope", "northeurope", "westindia", "australiaeast", "brazilsouth")

    $Locations | % {
        $Location = $_
        $Environment = "crgar-apim-synthetictransaction"
        $ResourceGroup = "$Environment-rg"
        $AppName = "$Environment-$Location-app"
        $AppInsightsKey = "b81d4688-59f8-44b9-abe9-2f1dbf9944d2"

        az functionapp create --resource-group $ResourceGroup `
        --consumption-plan-location $Location `
        --runtime dotnet `
        --functions-version 2 `
        --name $AppName `
        --app-insights-key $AppInsightsKey `
        --storage-account "crgarapimsynthweustorage"
    }

    $Locations | % {
        $Location = $_
        $Environment = "crgar-apim-synthetictransaction"
        $ResourceGroup = "$Environment-rg"
        $AppName = "$Environment-$Location-app"
        $AppInsightsKey = "b81d4688-59f8-44b9-abe9-2f1dbf9944d2"

        Write-Host "Starting in $Location"
        az functionapp config appsettings set --name $AppName --resource-group $ResourceGroup --settings "ApplicationEndpoint=https://crgar-apim-demo-apim.azure-api.net/crgar-apim-backend-sensors/Temperature"
        func azure functionapp publish $AppName
        Write-Host "Done with $Location"
    }

}