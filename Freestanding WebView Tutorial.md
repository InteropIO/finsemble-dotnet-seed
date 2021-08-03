# Integrating freestanding apps with WebView (using FDC3)

_Intended audience: .Net developers looking to leverage Finsemble's FDC3 support for existing .NET apps. The examples are for WPF, but the concepts also apply to Winforms._

## Terminology
* **FDC3:** An API that financial apps can use to share data with other applications, independent of the desktop agent that houses them. In this example, Finsemble is the desktop agent.
* **Finsemble seed:** The entry point for configuring a Finsemble smart desktop.
* **Freestanding app:** An app launched separately from Finsemble that can still interoperate with apps that Finsemble did launch.
* **WebView:** A control to show web content in a Winform or WPF application.

## Using the example project
The C# code that we use in this tutorial is also available in the FreestandingWPFExample project. Follow along using the [MainWindow.xaml.cs](./FreestandingWPFExample/MainWindow.xaml.cs) file for C# code. 

Normally, the FDC3-enabled web app you load in your WebView is hosted on a server. To keep things simple, here we've provided a static html page that calls some simple JavaScript. See [StaticHtmlPage.cs](./FreestandingWPFExample/StaticHtmlPage.cs) to see it in action.

**WARNING: Before building the example project, you must add your own license key to [MainWindow.xaml.cs](./FreestandingWPFExample/MainWindow.xaml.cs).**

## Getting started
_Goal: Take a WPF WebView app, and integrate it into Finsemble as a freestanding app._

1. Tell Finsemble to expect a freestanding app.
    
    Finsemble keeps track of what apps are available in the _appd.json_ file. For freestanding apps to interoperate with other Finsemble apps you must register an appId for them within _appd.json_. 
    
    For this example, we use the appId 'MyApp'.
    
    Add this entry under the `appd` property in your Finsemble seed's _public/config/appd.json_ file:
    ```json
    "MyApp": {
        "appId": "MyApp",
        "name": "MyApp", 
        "manifest": {}
    }
    ```
   
2. Use a WebView to inject Finsemble's JavaScript Adapter into the web app.

    In the MainWindow.xaml.cs file, after MainWindow has been initialized, inject a reference to the JavaS
    cript Adapter source file. When Finsemble is running locally, the location of that file is `"http://localhost:3375/build/finsemble/finsemble-javascript-adapter.js"`. You can import it like this:

    ```cs
	private string finsembleJsAdapterSrc = "http://localhost:3375/build/finsemble/finsemble-javascript-adapter.js";
    
    // Variable browser is of type IBrowser, and was created using EngineFactory. See sample page for details.
    browser.InjectJsHandler = new Handler<InjectJsParameters>(async args =>
        {
            // Import and run Finsemble for every web page
            await args.Frame.ExecuteJavaScript($@"
                import('{finsembleJsAdapterSrc}')
            ");
        }
    );
    ```

3. Start the Finsemble API in the web app

    After the WebView opens and the JavaScript Adapter has been injected into the page, start the Finsemble API by providing it with the name of the web app: "MyApp"
    
    To do this, update the injected JavaScript from the previous step with this code:

    ```cs
	private string finsembleJsAdapterSrc = "http://localhost:3375/build/finsemble/finsemble-javascript-adapter.js";
    private string fsblStartApp = "FSBL.startApp({appName: \"MyApp\", windowName: \"MyApp-window\"})";
    
    // Variable browser is of type IBrowser, and was created using EngineFactory. See sample page for details.
    browser.InjectJsHandler = new Handler<InjectJsParameters>(async args =>
        {
            // Import and run Finsemble for every web page
            await args.Frame.ExecuteJavaScript($@"
                import('{finsembleJsAdapterSrc}')
				.then(module => {{ {fsblStartApp} }})
            ");
        }
    );
    ```

4. The webapp is now running in the WebView and is connected to Finsemble. Let's add an FDC3 call to exchange data with another Finsemble app.

    Ideally, the app in your WebView is already using FDC3. We have an example page that calls the FDC3 API in [StaticHtmlPage.cs](./FreestandingWPFExample/StaticHtmlPage.cs). 
    
    In that example, we use the fdc3 `raiseIntent` method. Here's an example of what that JavaScript could look like:

    ```js
    fdc3.raiseIntent("ViewChart", {
        {
            type: "fdc3.instrument",
            name: "AAPL",
            id: {
                ticker: "AAPL"
            }
        }
    });
    ```

5. Putting it all together.

    Launch Finsemble from your seed. Your Finsemble needs to be configured with at least one FDC3-compatible app to test. You can use the included ChartIQ example app.

    After Finsemble starts and is running, open your WPF app. Perform the necessary actions to get the FDC3 call to happen. If you use any of the examples provided in Step 4, the "ViewChart" intent will be raised. This would cause ChartIQ to pop up, displaying the "AAPL" symbol.
    
    ## See also
    For more information about freestanding apps, see [Integrating native applications](https://documentation.finsemble.com/tutorial-integratingNativeApplications.html)
