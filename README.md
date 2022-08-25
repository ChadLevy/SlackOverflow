# SlackOverflow: Challenge of Challenge Coding

AccuLynx Dev Code Challenge of Challenging Coding. [Requirements](https://github.com/ChadLevy/challengeofchallengecoding/blob/main/Requirements.md)

## Requirements:

- .NET Core 6

## How to run:

1. Clone the repository
2. Open Terminal
3. Navigate to SlackOverflow\SlackOverflow.Web
4. Run command: `dotnet build`
5. Run command: `dotnet run --environment Production`

# Potential Improvements

- Add caching to avoid repeated calls to the Stack Overflow API.
- Use ViewModels to avoid sharing models. `StackOverflowClient` and `SlackOverflowService` are too tightly-coupled.
