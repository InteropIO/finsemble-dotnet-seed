# Multi-Window Example

Set a `multiWindowRoot` variable in the manifest that points to folder where _MultiWindowExample.exe_ is built (e.g. _C:/Users/andy/Documents/SourceCode/finsemble-dll/MultiWindowExample/bin/Debug_). Copy _multiWindowComponents.json_ to _configs/applications_ in the seed project, and add the following to the `importConfig` array: `"$applicationRoot/configs/application/multiWindowComponents.json"`.

```JSON
        "multiWindowRoot": "C:/Users/andy/Documents/SourceCode/finsemble-dll/MultiWindowExample/bin/Debug",
        "importConfig": [
            "$applicationRoot/configs/application/config.json",
            "$applicationRoot/configs/application/multiWindowComponents.json"
        ],
```