# BestHackerNews API - RESTful API
This project is an ASP.NET Core Web API application that retrieves the details of the first `n` "best stories" from the Hacker News API. It provides an endpoint to fetch the best stories sorted by their score in descending order.

## How to Run the Application
  1. Clone this repository to your local machine:
      **git clone <repository-url>**
  2. Navigate to the project directory:
      **cd BestHackerNews**
  3. Build the project:
      **dotnet build**
  4. Run the project:
      **dotnet run**
  5. The API will start running locally. You can access it at https://localhost:5001 by default.
  
## Endpoints
GET /api/best-stories/{n}: Retrieves the details of the first n best stories from the Hacker News API, sorted by score in descending order.

Example usage: GET https://localhost:5001/api/best-stories/10

Swagger UI url is as follows : https://localhost:5001/swagger/index.html
## Caching Mechanisms
The application implements caching mechanisms to cache responses from the Hacker News API for a duration of 1 minute. This helps improve performance and reduces the load on the Hacker News servers.
## Assumptions
1. The application assumes that the Hacker News API endpoints are accessible and respond with the expected data structure.
2. It's assumed that the StoryDetail class correctly represents the structure of story details returned by the Hacker News API.
3. Input n provided would always be less than number of id's provided by https://hacker-news.firebaseio.com/v0/beststories.json
## Enhancements and Future Considerations
1. Enhance error handling and logging to provide more detailed error messages.
2. Create unit test cases for the rest api project
3. Add validation checks to handle edge cases for input data.
4. Implement rate limiting and circuit breaking techniques to prevent overloading the Hacker News API, especially during periods of high traffic.
## Notes
Hacker News API url used are placed in appsettings.json
