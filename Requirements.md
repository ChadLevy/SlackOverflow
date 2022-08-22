# Goal:

Create a web page that displays a list of recent StackOverflow questions that have an accepted answer and
contain more than 1 answer. Allow visitors to view all the answers for a selected question in a random order
and try to guess which answer was the accepted answer.

We expect this to take about 5-8 hours. Please send us whatever you have completed after 1 week. You do
not need to have all requirements completed.

## Stackoverflow API

https://api.stackexchange.com/docs

# Requirements

1. Use the StackOverflow API to pull and display a list of recent questions that have an accepted answer
and more than 1 answer.
2. Select a question to view all the answers for that question in a random order.
3. Allow visitors to guess which answer they think is the “accepted” answer.
4. After guessing, display if the user guessed the “accepted” answer.

Upload your solution to a public repository on github.com and send us the link.

**Bonus**: Use git from the beginning to have a full commit history
**Extra Bonus**: Send us a link to the live version of your solution. For example, create a free Azure account and
host it as a free website.

# Things to note

- The results of the API above are JSON and will contain different data almost every time it’s requested
- Be warned: the StackExchange API is rate limited and will shut you off after a certain amount of
requests in a time period. (Hint: look at "quota_remaining” in the request) You shouldn’t run into the
quota limit, but just be aware of it.
- Implement this as a website in any programming language you want but must have instructions on how
to run locally
- You can use any library or frameworks to help you along, but you must display the desired
requirements above.
- Visual design will not be judged, but we do expect clean, semantic markup, well organized CSS (if any),
and a relatively intuitive user experience.