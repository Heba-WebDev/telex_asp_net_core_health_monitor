# Telex ASP .NET Core Health Monitor 

## Introduction
This is a telex integration that checks the overall health of an ASP .NET Core application after a set of time (defined in the settings).

## Features

- Monitors the health of the application and returns the status of important metrics like: CPU usage, RAM usage, network activiy, number of thread, disk usuage and garpage collecting metrics. 

## Technologies

<p>
  <a href="https://skillicons.dev">
    <img src="https://skillicons.dev/icons?i=cs,dotnet&perline=11" />
  </a>
</p>

## Installation
### Prerequisites
 Ensure you have the following installed on your system:

- .NET 8 SDK – Download Here
- ASP.NET Core Runtime – Included with .NET SDK

### Steps
 Clone the Repository
```
    git clone https://github.com/Heba-WebDev/telex_asp_net_core_health_monitor.git
    
    cd telex_asp_net_core_health_monitor
```
Install Dependencies
Run the following command inside the project directory:

```
    dotnet restore 
    dotnet build --configuration Release  
```

Run the Application

```
    dotnet run  
```

## Testing the Integration
You can manually test the integration by triggering a tick request:
```
every 10 minutes
curl -X POST http://localhost:5000/tick -H "Content-Type: application/json" -d '{"channel_id": "your_channel_id", "return_url": "your_return_url", "settings": [{"label": "interval", "type": "text", "required": true, "default": "*/10 * * *"}]}'
```

## License 
MIT