# AGL Code Challenge

A json web service has been set up at the url: http://agl-developer-test.azurewebsites.net/people.json

You need to write some code to consume the json and output a list of all the cats in alphabetical order under a heading of the gender of their owner.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.


### Assumptions

+ AGL webservice at http://agl-developer-test.azurewebsites.net/people.json does not require authentication or authorization
+ Main purpose of this challenge is to showcase .Net skill. Based on this assumption, .Net core Razore page is chosen instead of cooler SPA setup (Angular, React etc..)
+ Styling and front-end development is not the main concern
+ The solution is written with S.O.L.I.D princliples in mind => some code may seem like an overkill for a simple purpose of consuming an API. However, with this setup, different filter, ordering and different API can be consumed with minimal code changes
+ CI/CD pipeline can be simplified => this solution does not set up proper CI/CD for production

### Tech debt

+ Set up proper logging mechanism
+ Set up middleware and interceptor for HTTPRequest
+ Set up Exception handler middleware
+ This code is not production ready


### Prerequisites

Visual studio 2017/2019 and .net core 3.1

## Deployment

For the sake of simplicity, this solution is already deployed at http://aglcodechallenge.azurewebsites.net


