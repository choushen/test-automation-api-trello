# RestSharpProject
This project is a simple example of how to use RestSharp to make API calls to the Trello API. The project is written in C# and uses the NUnit testing framework to run tests against the API.


## Prerequisites
- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [Visual Studio Code](https://code.visualstudio.com/) or any other IDE/Text editor of your choice
- [Test Explorer](https://marketplace.visualstudio.com/items?itemName=formulahendry.dotnet-test-explorer) extension for VS code


## Running the project
You can load the project into an IDE/Text editor of your choice and run the tests from there.
I will cover setup for VS code since that is what I used.
To run the tests in VS code you can use the test explorer extension to run the tests. Here are the steps:
- Install the test explorer extension
- Open the test explorer
- Click refresh and you should see the tests


### Troubleshooting
#### Running the tests on windows
If you are running the tests on windows, just be sure to add the environment variables to your system variables and restart your IDE/Text editor.

#### Running the tests on mac
If you are building the project on a mac, you may need to do some additional setup in your user settings to get the tests to run. Here is what you might need to add to your settings.json file:
``` json
{
  "dotnet-test-explorer.testProjectPath": "RestSharpProject/",
  "terminal.integrated.shell.osx": "/bin/zsh",
  "terminal.integrated.env.osx": {
    "TRELLO_RESTSHARP_KEY": "${env:TRELLO_RESTSHARP_KEY}",
    "TRELLO_RESTSHARP_TOKEN": "${env:TRELLO_RESTSHARP_TOKEN}"
  }
}
```


## Author
- [Author](Jacob McKenzie)
- [Email](jacob.mckenzie@icloud.com)
- [LinkedIn](https://www.linkedin.com/in/jacob-mckenzie-0888a7175/)
- [GitHub](https://github.com/choushen)
