﻿Enable-Migrations -Project MORR.Infrastructure -StartUp MORR.API -Verbose

Add-Migration MORR00001 -Project MORR.Infrastructure -StartUp MORR.API -Verbose
Add-Migration MORR00002 -Project MORR.Infrastructure -StartUp MORR.API -Verbose
Add-Migration MORR00003 -Project MORR.Infrastructure -StartUp MORR.API -Verbose

Update-Database -Project MORR.Infrastructure -StartUp MORR.API -Verbose

Script-Migration -From MORR00001 -Project MORR.Infrastructure -StartUp MORR.API -Verbose
Script-Migration -From MORR00002 -Project MORR.Infrastructure -StartUp MORR.API -Verbose