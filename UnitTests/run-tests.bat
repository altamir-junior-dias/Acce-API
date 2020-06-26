dotnet test /p:CollectCoverage=true /p:CoverletOutput=TestResults/ /p:CoverletOutputFormat=opencover

dotnet %UserProfile%\.nuget\packages\reportgenerator\4.5.2\tools\netcoreapp2.0\ReportGenerator.dll "-reports:testresults\Coverage.OpenCover.xml" "-targetdir:coveragereport" "-filefilters:-*Exception.cs;-*Wrapper.cs;-*Injector.cs" -reporttypes:Html