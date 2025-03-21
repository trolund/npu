param location string = resourceGroup().location
param cosmosDbAccountName string
param databaseName string
param containerName string
param functionStorageAccountName string
param functionAppName string

param appServicePlanName string
param appServiceName string
param funcServicePlanName string

resource cosmosDbAccount 'Microsoft.DocumentDB/databaseAccounts@2024-08-15' = {
  name: cosmosDbAccountName
  location: location
  kind: 'GlobalDocumentDB'
  properties: {
    enableFreeTier: true
    databaseAccountOfferType: 'Standard'
    locations: [
      {
        locationName: location
      }
    ]
    consistencyPolicy: {
      defaultConsistencyLevel: 'Session'
    }
  }
}

resource appServicePlan 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: 'F1'
    tier: 'Free'
  }
}

resource appService 'Microsoft.Web/sites@2022-03-01' = {
  name: appServiceName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      appSettings: [
        {
          name: 'COSMOS_DB_CONNECTION_STRING'
          value: cosmosDbAccount.listConnectionStrings().connectionStrings[0].connectionString
        }
        { name: 'COSMOS_DB_NAME', value: databaseName }
        { name: 'COSMOS_CON_NAME', value: containerName }
      ]
    }
  }
}

resource funcServicePlan 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: funcServicePlanName
  location: location
  kind: 'functionapp'
  sku: {
    tier: 'Dynamic' // ✅ Consumption Plan (Pay-per-use)
    name: 'Y1'
  }
}

resource funcStorageAccount 'Microsoft.Storage/storageAccounts@2022-09-01' = {
  name: functionStorageAccountName
  location: location
  kind: 'StorageV2'
  sku: {
    name: 'Standard_LRS'
  }
}

resource functionApp 'Microsoft.Web/sites@2022-03-01' = {
  name: functionAppName
  location: location
  kind: 'functionapp'
  properties: {
    serverFarmId: funcServicePlan.id
    siteConfig: {
      appSettings: [
        {
          name: 'AzureWebJobsStorage'
          value: 'DefaultEndpointsProtocol=https;AccountName=${funcStorageAccount.name};AccountKey=${funcStorageAccount.listKeys().keys[0].value};EndpointSuffix=core.windows.net'
        }
        {
          name: 'FUNCTIONS_WORKER_RUNTIME'
          value: 'dotnet-isolated' // ✅ Correct for .NET Isolated
        }
        {
          name: 'DOTNET_ISOLATED_VERSION'
          value: '8.0' // ✅ Ensure correct .NET version (adjust if needed)
        }
        {
          name: 'COSMOS_DB_CONNECTION_STRING'
          value: cosmosDbAccount.listConnectionStrings().connectionStrings[0].connectionString
        }
        { name: 'COSMOS_DB_NAME', value: databaseName }
        { name: 'COSMOS_CON_NAME', value: containerName }
      ]
    }
  }
}

output functionAppUrl string = functionApp.properties.defaultHostName
output appServiceUrl string = appService.properties.defaultHostName
