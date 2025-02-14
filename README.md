# Company Api

![last-commit](https://badgen.net/github/last-commit/BianorAraujo/company-api) ![license](https://badgen.net/github/license/BianorAraujo/company-api)

This application was created as a code challenge to validate my skills in a hiring process.

In this system you can create a company record specifying the Name, Stock Ticker, Exchange, Isin code, and optionally a website. You will be able to get companies list, get company by Id, get company by Isin code and update company information as well.

## Technologies Used

* Angular 16
* Bootstrap 5
* .NET 9
* Dapper ORM
* XUnit
* Moq
* SQL Server
* Docker


## How to start

This project was builted to be easily tested.

You'll need to install the Docker CLI or Docker Desktop, [click here](https://www.docker.com) to download.

Clone this repository and go to the directory where it was cloned.

Run this command to up all containers:
```
docker compose -f compose.yaml up --build -d
```

Now, the Application and the API are running, including the database and tables that were also created.

It's ready to use!

Accesss the application in the following url:
```
http://localhost:4200
```

Access the API in the following url:
```
http://localhost:5055/scalar/v1
```

For stopping the containers:
```
docker compose -f compose.yaml down
```

## Images

### Frontend

<img width="1254" alt="CompanyApp" src="https://github.com/user-attachments/assets/3619e2ab-011c-42b1-9889-816379ab29e0" />

### Backend

<img width="1238" alt="CompanyApi" src="https://github.com/user-attachments/assets/a3f46fb9-f864-488a-a32e-411cad5e00df" />

### Unit Tests

<img width="1270" alt="image" src="https://github.com/user-attachments/assets/527d3920-3ca6-451d-92d8-413d094c55ba" />

