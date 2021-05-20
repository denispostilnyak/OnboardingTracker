

# OnboardingTracker

**Vacancy**

- Title
- Status
- Salary Max
- Assigned Recruiter
- Work Experience
- Seniority Level
- Skillset
- Job Type: full time, part time, etc

**Candidates**
- Origin
- Mobile
- Email
- Experience Years
- Current Job Information
- Skillset

**Interviews**
- Title
- Interviewers
- From/To
- Candidate

####  Style Guideline

---

**Pt.1. Naming Conventions**

---

-  Private fields should be named in camelCase.

- Public properties, fields, methods, etc. should be named in PascalCase.

- Variables should have self-descriptive names that should mention their type.

- DRY.

- Class names should refer to their purpose and functions, mentioning interfaces and/or classes that they implement.

  | ✅Do this                                                     | ❌ Instead of this                                        |
  | ------------------------------------------------------------ | -------------------------------------------------------- |
  | ```private readonly OnboardingTrackerContext dbContext;```   | ```private readonly OnboardingTrackerContext _ctx;```    |
  | ```var vacancyStatus = mapper.Map<VacancyStatus>(request);``` | ```var mapped = mapper.Map<VacancyStatus>(request);```   |
  | ```AVeryLongNamespace.Commands.DoSmth```                     | ```AVeryLongNamespace.Commands.DoSmthCommand```          |
  | ```CandidateValidator : AbstractValidator<CreateCandidate>``` | ```FieldsChecker : AbstractValidator<CreateCandidate>``` |



**Pt.2. Controllers**

---

- Should always be named in plural form.

- Should not re-introduce attributes applied to the base controller.

- One controller per entity/model.

- All action methods **have to be**  annotated with **ProducesResponseTypeAttribute.**

- All response types that indicate an unsuccessful request should be denoted in the base controller.

- Controllers **may not** contain any business logic.

- Should explicitly return status codes according to REST.

- ``null`` in ``*GetById`` queries should not throw ```NotFoundException``` and instead return a ``204 No Content`` with an empty JSON.

  

**Pt.3. Application Layer and general code style**

---

- Nest handler and validator  inside your query/command class.

- Should you need to return an array, do it via a separate model.

- ```c#
  if(true)
  {
  	some single line statement;
  }
  ```

  instead of 

  ```c#
  if(true)
      do smth...;
  ```

- If you are thinking of moving some parts of  configuration code from ```Startup.cs``` and can't think of a proper place to put it into, leave it as it is.

-----

**Pt.4 User Secrets**

During development all sensitive information is stored in user secrets file on developer's local machine. 

In order to be able to debug the application using dev environment, you need to set up your local user secrets file accordingly. All variables that are provided from your user secrets are specifically commented out in **appsettings.Development.json** file.

This paragraph describes in detail which sections and variables should be provided from your user secrets file. Unless explicitly stated, all of the sections mentioned below  mustn't be nested and are located in the root of your json document.

-----

**4.1.Database connection string.**

```json
  {
  ...,
      "Db": {
        "ConnectionString": "Data Source=.;Initial 	Catalog=YourExistingDatabase;Integrated Security=True;"
      },
  ...,
  }
```

Basic connection string example is shown above. It can refer to any database, for example localDB provided by Visual Studio.

-----

**2.AWS S3 storage options.**

```json
{
...,
    "Infrastructure": {
        "Storage": {
          "AmazonS3": {
            "BucketName": "devbucket.name",
            "AccessKeyId": "DEV_ACCESS_KEY",
            "SecretAccessKey": "DEV_SECRET_KEY",
            "Region": "DEV_REGION"
          }
        }
      },
  ...,
  }
```

This section provides variables configures the AWS S3 blob storage for our project.

-----

**3.OAuth2 authentication options.**

```json
{
...,
"OAuth2": {
    "Domain": "DEV_DOMAIN",
    "Audience": "DEV_AUDIENCE",
    "CacheTokenSec": 900
  },
...,
}
```

CacheTokenSec option specifies the duration during which the user profile will be stored in memory cache.

DEV_DOMAIN and DEV_AUDIENCE options have to match those in the section below.

-----

**4.Swagger OAuth2 options.**

```json
"Swagger": {
    "Authorization": {
      "Implicit": {
        "Domain": "DEV_DOMAIN",
        "Audience": "DEV_AUDIENCE",
        "OAuthTokenUrl": "https://DEV_DOMAIN/oauth/token",
        "ClientId": "DEV_CLIENT_ID",
        "ClientSecret": "DEV_CLIENT_SECRET",
        "AuthorizeUrl": "https://DEV_DOMAIN/authorize",
        "Scopes": {
          "openid": "",
          "profile": "",
           "email": ""
        }
      }
    }
  }
```

This section is used to configure swagger to use specific authorization flow and allow user to choose from three available scopes required to get all the necessary information about the user currently logged in.

