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

## GitHub Actions Configuration

In order to allow GitHub Actions to run migrations, GitHub Actions is setup as
an Application inside of Azure Portal.

It's configured with Federated Credentials that allow each of the
repos/environments to authenticate via OIDC.

The Application will link to an Enterprise Application Service Principal which
needs to be setup with a role assignment of `SqlDb Migration Role` for the
databases.

Lastly, using the above SQL you'll need to give the Service Principal
permissions within the MSQQL DB itself.
