# MyTurn

Supply your own `appSetting.safe` and `connStr.safe` files.  Not checking in since they contain secrets.

`appSetting.safe` contains all appSetting, normally found in *.config files.

```xml
<appSettings>
    <add key="webpages:Version" value="3.0.0.0"/>
    <add key="webpages:Enabled" value="false"/>
    <add key="ClientValidationEnabled" value="true"/>
    <add key="UnobtrusiveJavaScriptEnabled" value="true"/>
</appSettings>
```

`connStr.safe` contains database connection strings, normally found in *.config files.

```xml
<connectionStrings>
	<add name="MyTurn" connectionString="data source=[dbName];initial catalog=MyTurn;Integrated Security=true;;MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />
</connectionStrings>
```
