# Infrastructure

The current PaaS for `aquifer-server` is Azure with Azure SQL (MSSQL) and Web
App Services.

## Azure SQL Configuration

This section exists because Azure SQL is not that intuitive to configure. After
creating the database, you'll need to go in as the user who is assigned as AD
Admin, and run this statement for each user that needs access (replacing
`<user-here>` with the full username of the person (or bot):
```
CREATE USER [<user-here>] FROM EXTERNAL PROVIDER  WITH DEFAULT_SCHEMA=[dbo]
GO
sys.sp_addrolemember @rolename = N'db_owner', @membername = N'<user-here>'
GO
```

## GitHub Actions Configuration

In order to allow GitHub actions to run migrations and deploy the service
you'll need to setup the [Azure Login Action](https://github.com/marketplace/actions/azure-login).
The instructions you'll follow on that page are the `OpenID Connect (OIDC)
based Federated Identity Credentials` instructions. When you create your App
Registration, make sure you note the `Name` (e.g. you could call it GitHub
Actions), since this is what you'll use in the above SQL statement to give it
permissions.

After creating the App Registration, you'll need to assign it to the
subscription. You can do this by going to the Subscription object, then the IAM
tab, then using the Add button to create a role assignment for `SqlDb Migration
Role` for the App Registration you just made.
