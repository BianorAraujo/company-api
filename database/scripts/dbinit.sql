IF NOT EXISTS (SELECT name FROM master.sys.databases WHERE name = N'CompanyDB')
BEGIN
    CREATE DATABASE CompanyDB;

    PRINT 'CompanyDB database created successfully.'
END
ELSE
BEGIN
    PRINT 'CompanyDB database already exists.'
END
GO

USE CompanyDB;
GO

IF EXISTS (SELECT name FROM SYS.objects WHERE name = 'Company')
BEGIN
    PRINT 'Company table already exists.'
END
ELSE
BEGIN
    CREATE TABLE Company (
        Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
        Name VARCHAR(50) NOT NULL,
        Exchange VARCHAR(50) NOT NULL,
        Ticker VARCHAR(5) NOT NULL,
        Isin VARCHAR(12) NOT NULL,
        Website VARCHAR(100) NULL,
        CONSTRAINT U_Company_Isin UNIQUE (Isin)
    );

    PRINT 'Company table created successfully.'
END
GO