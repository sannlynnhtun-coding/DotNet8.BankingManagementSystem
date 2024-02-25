AccountCreation
```sql
CREATE TABLE Accounts (
    AccountNo NVARCHAR(50),
    CustId NVARCHAR(50),
    AccountType NVARCHAR(50),
    Description NVARCHAR(255),
    Balance DECIMAL(18, 2)
);

```
Deposit

```sql

```

ManageCustomerForm
```sql
CREATE TABLE Customer (
    CustId NVARCHAR(50),
    Name NVARCHAR(50),
    PhnNumber NVARCHAR(15)
);
```
ManageEmployeeForm
```sql
CREATE TABLE Employee (
    EmpId NVARCHAR(50),
    Name NVARCHAR(50),
    PhnNumber NVARCHAR(15),
    Salary DECIMAL(18, 2),
    Designation NVARCHAR(255)
);

```

TransferAccount
```sql
CREATE TABLE Transfers (
    Tf_ID INT,
    F_Acc NVARCHAR(50),
    To_Acc NVARCHAR(50),
    Date NVARCHAR(15),
    Amount DECIMAL(18, 2)
);

```

Withdraw
```sql

```