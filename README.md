# NPU Backend

[![Deploy npu to Azure](https://github.com/trolund/npu/actions/workflows/main.yml/badge.svg?branch=main)](https://github.com/trolund/npu/actions/workflows/main.yml)

## Overview

### Scope

No users are modeled in the system and not authentication is implemented. 

The system is a simple backend for a social platform where users can upload NPU creations and rate others' work. 
The system is hosted on Azure and uses **Azure Cosmos DB** for storing data and **Azure Blob Storage** for storing images.

### Folder structure:

```
📂 ProjectRoot  
├── 📁 .github (GitHub actions)  
│   ├── 📁 workflows (GitHub actions workflows pipeline)  
├── 📁 infrastructure (Azure infrastructure as code)  
├── 📁 scripts (Scripts for local development)  
├── 📁 src (Source code)  
├── 📄 README.md  
└── 📄 Requirements.md  
```

### Project structure (src folder):

```
📂 NPU (9 projects)
├── 📁 NPU.Api (Api controllers)
├── 📁 NPU.ApiTests (Api tests)
├── 📁 NPU.BI (Business logic)
├── 🌐 NPU.Client (Frontend - have not been implemented ❌)
├── 📁 NPU.Data (Data access)
├── 📁 NPU.End2EndTests (End-to-end tests - have not been implemented ❌)
├── 📁 NPU.FuncApp (Azure Function App - have not been implemented ❌))
├── 📁 NPU.Infrastructure
└── 📁 NPU.UnitTests
```

The application have NOT been tested properly and the test project are simply examples of how to write tests.

### Context Diagram

```mermaid
C4Context
    Person(user, "NPU Fan", "Uploads creations and rates others' work")
    System(npuPlatform, "NPU Platform", "A social platform for NPU fans")
    System_Ext(cloudHosting, "Cloud Platform", "Azure hosting the backend and frontend")

    Rel(user, npuPlatform, "Uploads NPU creations, searches, and rates")
    Rel(npuPlatform, cloudHosting, "Hosted on cloud infrastructure")
```

### Container Diagram

```mermaid
C4Container
    Person(user, "NPU Fan", "Interacts with the NPU platform")

    System_Boundary(npuSystem, "NPU Platform") {
        Container(webApp, "Web Application", "React/Next.js", "Allows users to browse, upload, and rate NPU creations")
        Container(mobileApp, "Mobile App", "Flutter/React Native", "Mobile interface for interacting with NPU creations")
        Container(npuBackend, "NPU Backend", "C#/.NET", "Handles business logic and data processing")

        Container(apiGateway, "API Gateway", "Azure API Management", "Routes requests to the backend services")
        Container(blobStorage, "Blob Storage", "Azure Blob Storage", "Stores images of NPU creations")
        ContainerDb(database, "Database", "Azure Cosmos DB", "Stores user data, NPU creations, and ratings")
    }

    Rel(user, webApp, "Uses")
    Rel(user, mobileApp, "Uses")
    Rel(webApp, apiGateway, "Calls API")
    Rel(mobileApp, apiGateway, "Calls API")
    Rel(apiGateway, npuBackend, "Forwards requests")
    Rel(npuBackend, database, "Reads/Writes Data")
    Rel(npuBackend, blobStorage, "Stores/Retrieves Images")
```
