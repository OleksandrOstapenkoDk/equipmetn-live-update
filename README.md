# equipmetn-live-update

## Sample Exercise: Application and API for storing history of changes to equipment states

**Running the application:** 
Clone repository, open EquipmentLiveUpdate/EquipmentLiveUpdate.sln, set "docker-compose" as startup project, press <F5>, wait for 2 mins (application has to create database and apply migratrions)

### Application structure
  **EquipmentLiveUpdate.Api**
  
  API for workers to set statuses and monitor current status. Publishes status update event to RabbitMQ
  
  **EquipmentLiveUpdate.Service**
  
  Consumes status update event and saves current status to DB
    
  **EquipmentLiveUpdate.EventHistory**
  
  Consumes status update event and saves to event history
    
  **EquipmentLiveUpdate.Domain**
    
  Domain models
    
  **EquipmentLiveUpdate.Infrastructure**
    
  DB models and repository
  
  **EquipmentLiveUpdate.Contracts**
  
  Message contract, shared beetween API, Service and EventHistory

  **Database connection**: MSSQL server: localhost, user: sa, password: S3Cur3Passw0rd (Yes, sharing in readme:) )

  **Pending to implement** logging, observability, unit tests, integration tests
