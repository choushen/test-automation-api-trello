# RestSharpProject
This project is a simple example of how to use RestSharp to make API calls to the Trello API. The project is written in C# and uses the NUnit testing framework to run tests against the API.

## Running the project
You can load the project into an IDE/Text editor of your choice and run the tests from there.
I will cover setup for VS code since that is what I used.
To run the tests in VS code you can use the test explorer extension to run the tests. Here are the steps:
- Install the test explorer extension
- Open the test explorer
- Click refresh and you should see the tests

### Troubleshooting
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
