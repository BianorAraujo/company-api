# Company Api

![last-commit](https://badgen.net/github/last-commit/BianorAraujo/company-api) ![license](https://badgen.net/github/license/BianorAraujo/company-api)

This application was created as a code challenge to validate my skills in a hiring process.

In this system you can create a company record specifying the Name, Stock Ticker, Exchange, Isin code, and optionally a website. You will be able to get companies list, get company by Id, get company by Isin code and update company information as well.

### Technologies Used

* Angular 16
* Angular Material
* .NET 9
* Dapper ORM
* SQL Server
* Docker


### How to start

This project was builted to be easily tested.

You'll need to install the Docker CLI or Docker Desktop, [click here](https://www.docker.com) to download.

Clone this repository and go to the directory where it was cloned.

Run this command:
```
docker compose -f compose.yaml up --build -d
```

Url to access the application:
```
http://localhost:4200
```

Url to access the API:
```
http://localhost:5055/scalar/v1
```


### Images

