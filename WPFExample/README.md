1. Download the .NET Seed Repository and build your WPF .NET application from 
the *WPFExample project* directory

2. Add your application to applicationRoot/configs/application/component.json
with your any of your custom configs. You may copy the example below but
add the appropriate path to your .exe file

 
```javascript
		"WPF": {
			"window": {
				"windowType": "native",
				"path": "yourPath/FinsembleWPFDemo.exe",
				"frame": false,
				"resizable": true,
				"autoShow": true,
				"top": "center",
				"left": "center",
				"width": 400,
				"height": 432
			},
			"component": {
				"inject": false,
				"spawnOnStartup": false
			},
			"foreign": {
				"services": {
					"dockingService": {
						"canGroup": true,
						"isArrangable": true
					}
				},
				"components": {
					"App Launcher": {
						"launchableByUser": true
					},
					"Window Manager": {
						"showLinker": true,
						"FSBLHeader": true,
						"persistWindowState": true,
						"title": "WPFDemo"
					}
				}
			}
		},
...
```

Additional custom configs found at:
https://documentation.chartiq.com/finsemble/tutorial-ConfigReference.html, 
see *components* dropdown

3. Run Finsemble

In the Apps menu you should now see *WPF* as an option. Open this application and
begin testing the snaping, docking and data sharing via drag & drop or linking. 
