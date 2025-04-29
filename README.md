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
  
  Message contract, shared between API, Service and EventHistory

  **Database connection:** MSSQL server: localhost, user: sa, password: S3Cur3Passw0rd (Yes, sharing in readme:) )

  **Pending to implement:** logging, observability, unit tests, integration tests

## Suggested project architecture
**High level diagram:**
![alt text](https://github.com/OleksandrOstapenkoDk/equipmetn-live-update/blob/master/EquipmentStatusSystem.png) 

### Microservices Breakdown
**Equipment State Service**

Event consumption, CRUD operations for equipment (add new machine, update state)

**Equipment State History Service**

Saves changes to equipment states database and exposes historical data

**Equipment State Monitor Service**

Provides real-time status overview 

### Technologies
**Frontend** 

React.js or physical devices, like Raspberry Pi

**Backend services**

Latest .NET, EF Core, deployed to kubernetes cluster, Azure or AWS 

**Database**

MSSQL or PostgreSQL

**Event Bus**

Kafka, or AWS EventBridge

**Real-time Updates**

SignalR

**Monitoring**

Grafana

### Physical Components
**Factory Floor Devices** 

Tablets for API calls or Raspberry Pi for automatic updates

**Backend Infrastructure**

Azure or AWS, Kubernetes cluster

### Core Engineering Points
**Event Driven Design:** 

The system is based on events, sent to Event Bus, used for system decoupling and reliability increasing

**Observability:** 

Metrics, logs

**Testing:**

Unit Tests, Integration Tests, End-to-end Tests

**Zero-downtime Deployments:** 

Blue/Green or rolling deployments

