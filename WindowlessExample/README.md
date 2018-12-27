 1. Download the .NET Seed Repository and build your WindowlessExample .NET application from 
 the *WPFExample project* directory
 
  2. Add your application to applicationRoot/configs/application/component.json
 with any of your custom configs. You may copy the example below but
 add the appropriate path to your .exe file
 
  
  ```javascript
		"WPFwindowless": {
			"window": {
				"windowType": "native",
				"path": "yourPath/WindowlessExample.exe"
			},
			"foreign": {
				"components": {
					"App Launcher": {
						"launchableByUser": true
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
 
  In the Apps menu you should now see *WPFwindowless* as an option. Open 
  central logger and set log level to *log*. Access the Apps menu and select Windowless
  Example. Confrm that when the Windowless app is selected in the menu a message is sent to the log. 
