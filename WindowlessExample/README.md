# File System Access Via a Native Service

This repo contains the windowless C# component that:

- Browses for folders
- Lists a folder's contents
- Writes a file to disk

See the [finsemble-seed recipe](https://github.com/ChartIQ/finsemble-seed/tree/recipes/file-system-access-via-native-service) for information about the HTML5 component.

## Installation

To install and run the file-system-access-via-native-service example please follow the instructions below:

1. Compile the WindowlessExample project using Visual Studio
1. Either add the configuration in [_windowlessComponent.json_](./windowlessComponent.json) to your _configs/application/components.json_ file OR copy the file to your project (e.g. in a _src/components/native_ folder) and import it in your _configs/application/config.json_ file:
    ```
    	...,
        "importConfig": [
            ...,
            "$applicationRoot/components/native/windowlessComponent.json"
        ]
    ```
1. Define the `windowlessRoot` variable in your manifest (e.g. _configs/application/manifest-local.json_) file to point to the output directory of your Visual Studio build of the WindowlessExample component:

    ```
        ...
        "finsemble": {
            ...
            "windowlessRoot": "C:/Users/andy/Documents/SourceCode/finsemble-dotnet-seed/WindowlessExample/bin/Debug",
            ...
        }
        ...
    ```
1. Install the HTML5 component, as described in the component's [README](https://github.com/ChartIQ/finsemble-seed/tree/recipes/file-system-access-via-native-service/README.md) file
1. Build and run Finsemble and try launching the **File Browser** component
1. Click the **Browse for Folder** button to select a folder from the file system
1. Once a folder has been selected, click the **List Folder Contents** button to list the files found in the selected folder
1. After selecting a folder, you can specify a filename and file contents a and click the **Write File** button to write the file to disk
