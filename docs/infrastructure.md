# Infrastructure

The current PaaS for `aquifer-server` is Azure with Azure SQL (MSSQL) and Web
App Services.

## Azure SQL Configuration

This section exists because with the Active Directory authentication enabled,
authenticating with an external service requires some extra configuration
steps. After creating the database, you'll need to go in as the user who is
assigned as AD Admin, and run this statement for each user that needs access
(replacing `<user-here>` with the full username of the person (or bot):
```
CREATE USER [<user-here>] FROM EXTERNAL PROVIDER  WITH DEFAULT_SCHEMA=[dbo]
GO
sys.sp_addrolemember @rolename = N'db_owner', @membername = N'<user-here>'
GO
```

## Azure Web App Service Configuration

You need to create a Managed Identity for the environment you're deploying.
Note the name and use it with the above SQL statement to create permissions in
the MSSQL DB itself.

Then in the Web App Service, assign the identity.

The connection string should then be configured like so, with `CLIENT_ID` set
to the Client ID of the Managed Identity:

`Server=DB_URL;Authentication=Active Directory MSI;Encrypt=True;User Id=CLIENT_ID;Database=DB_NAME`

## Azure Devops Configuration

In order to allow Azure Devops pipelines to run migrations and deploy the
service you'll need to setup a connection from Azure Devops to Azure itself.
Once the connection is created it should be in Azure as an Enterprise
Application Service Principal. Note the name (you can rename if you want) and
use it with the above SQL statement to create permissions in the MSSQL DB
itself.

Next, you'll need to assign the Service Principal to the subscription. You can
do this by going to the Subscription object, then the IAM tab, then using the
Add button to create a role assignment for `SqlDb Migration Role` for the
Service Principal you just made.
