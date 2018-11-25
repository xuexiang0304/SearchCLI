# Introduction

SearchCLI is a command line application which can search through provided data and display the search result.

# Assumptions
1. The relationship between given data.
The user's id is linked with ticket's submitter_id and assignee_id.
The organization's id linked with ticket's organization_id and user's organization_id.

2. Search constraints
The search is fine search which means it only matches the whole world. For example, if search "am", it will not match "ample"; if search "2015", it will not match "2015-10-10". Also, the search is case sensitive. For example, if we search "new york", "New York" will NOT be matched.

# How to run SearchCLI
Please choose from the following instructions.
### 1. Run the application by third-party tool -- .NET Core CLI 
- Install [.NET Core CLI](https://www.microsoft.com/net/learn/dotnet/hello-world-tutorial#install)  on your local machine
- Open a termial/command prompt 
- Navigate to the Release/netcoreapp2.1
- Run command `dotnet SearchCLI.dll`
- Following the instruction display on the terminal/command prompt

### 2. Run the self-contained application for Max OS
- Navigate to the Release/netcoreapp2.1/osx.10.11-x64
- Double click on the executable file 
- Following the instruction display on the terminal/command prompt

### 3. Run the self-contained application for Windows 10
- Navigate to the Release/netcoreapp2.1/win10-x64
- Double click on the executable file 
- Following the instruction display on the command prompt

### 4. Run in debug mode with IDE
- Install Visual studio
- Build and run the project

# Explaination of the instruction displaying on the terminal
After running the appliction successfully, you should be able to see a piece of intruction shown below:
```
Please select from the following options by type the number:
1. Search through Users, Tickets and Orgnizations and display related entities.
2. Search through Users and display related entities.
3. Search through Organizations and display related entities
4. Search through Tickets and display related entities
5. Exit
```
Enter the number of the option you want to choose, then type the `return`/`enter` button. Then you will see another line of instruction which asking you to type in the search value.
```
Please enter your search value
```
Type in the search value and then the `return`/`enter` button. You will see the search result. The search result of each option will be clarified in the Features section.

# Features
### 1. Search through multiple data resource (users.json, tickets.json, organization) and return result at once.
There are three groups of results will be returned.
1. Users and the related tickets and organizations for each users in the result.
2. Tickets and related tickets and organizations for each ticket in the result.
3. Organizations and related users and tickets for each organization in the result.

### 2. Search through users first and return the result and the related tickets and organizations for each user in the result.
There is only one group of result will be returned. See the example for search "admin" below, the result is showing a user information in the result:
```
There are 24 users show in the result

The details of user Francisca Rasmussen is shown below:

The information of user Francisca Rasmussen:
id:1,
url:http://initech.zendesk.com/api/v2/users/1.json,
external_id:74341f74-9c79-49d5-9611-87ef9b6eb75f,
name:Francisca Rasmussen,
alias:Miss Coffey,
created_at:2016-04-15T05:19:46 -10:00,
active:True,
verified:True,
shared:False,
locale:en-AU,
timezone:Sri Lanka,
last_login_at:2013-08-04T01:03:27 -10:00,
email:coffeyrasmussen@flotonic.com,
phone:8335-422-718,
signature:Don't Worry Be Happy!,
organization_id:119,
tags:[Springville,Sutton,Hartsville/Hartley,Diaperville],
suspended:True,
role:admin

User Francisca Rasmussen belong to organization Multron.

The information of organization (Multron) is shown below:
id:119,
url:http://initech.zendesk.com/api/v2/organizations/119.json,
external_id:2386db7c-5056-49c9-8dc4-46775e464cb7,
name:Multron,
domain_names:[bleeko.com,pulze.com,xoggle.com,sultraxin.com],
created_at:2016-02-29T03:45:12 -11:00,
details:Non profit,
shared_tickets:Non profit,
tags:[Erickson,Mccoy,Wiggins,Brooks]

There are 2 tickets submitted by user Francisca Rasmussen(1)

The information of ticket (fc5a8a70-3814-4b17-a6e9-583936fca909) is shown below:
id:fc5a8a70-3814-4b17-a6e9-583936fca909,
external_id:http://initech.zendesk.com/api/v2/tickets/fc5a8a70-3814-4b17-a6e9-583936fca909.json,
created_at:2016-07-08T07:57:15 -10:00.
type:problem,
subject:A Nuisance in Kiribati,
description:Ipsum reprehenderit non ea officia labore aute. Qui sit aliquip ipsum nostrud anim qui pariatur ut anim aliqua non aliqua.,
priority:high,
status:open,
submitter_id:1,
assignee_id:19,
organization_id:120,
tags:Minnesota,New Jersey,Texas,Nevada,
has_incidents:True,
due_at:,
via:voice


The information of ticket (cb304286-7064-4509-813e-edc36d57623d) is shown below:
id:cb304286-7064-4509-813e-edc36d57623d,
external_id:http://initech.zendesk.com/api/v2/tickets/cb304286-7064-4509-813e-edc36d57623d.json,
created_at:2016-03-30T11:43:24 -11:00.
...


There are 2 tickets assgined by user Francisca Rasmussen(1)

The information of ticket (1fafaa2a-a1e9-4158-aeb4-f17e64615300) is shown below:
id:1fafaa2a-a1e9-4158-aeb4-f17e64615300,
external_id:http://initech.zendesk.com/api/v2/tickets/1fafaa2a-a1e9-4158-aeb4-f17e64615300.json,
created_at:2016-01-15T11:52:49 -11:00.
type:problem,
subject:A Problem in Russian Federation,
description:Elit exercitation veniam commodo nulla laboris. Dolore occaecat cillum nisi amet in.,
priority:low,
status:solved,
submitter_id:44,
assignee_id:1,
organization_id:115,
tags:Georgia,Tennessee,Mississippi,Marshall Islands,
has_incidents:True,
due_at:2016-08-07T04:10:34 -10:00,
via:voice


The information of ticket (13aafde0-81db-47fd-b1a2-94b0015803df) is shown below:
id:13aafde0-81db-47fd-b1a2-94b0015803df,
external_id:http://initech.zendesk.com/api/v2/tickets/13aafde0-81db-47fd-b1a2-94b0015803df.json,
created_at:2016-03-30T08:35:27 -11:00.
type:task,
subject:A Problem in Malawi,
...
```
### 3. Search through tickets first and return the result and the related users and organizations for each user in the result.
There is only one group of result will be returned. See the example for search "New York" below, the result is showing a user information in the result:
```
The information of ticket (5f7a19db-432e-4d6f-8c29-ba121aed5d68) is shown below:
id:5f7a19db-432e-4d6f-8c29-ba121aed5d68,
external_id:http://initech.zendesk.com/api/v2/tickets/5f7a19db-432e-4d6f-8c29-ba121aed5d68.json,
created_at:2016-05-28T06:33:28 -10:00.
type:question,
subject:A Problem in Cambodia,
description:Ea nisi aliquip sint et. Ea do sunt ex mollit tempor voluptate sint in Lorem nisi aliqua.,
priority:high,
status:hold,
submitter_id:40,
assignee_id:23,
organization_id:123,
tags:Massachusetts,New York,Minnesota,New Jersey,
has_incidents:True,
due_at:2016-08-12T05:40:30 -10:00,
via:web

The ticket was submitted by user Burgess England(40)

The information of user Burgess England:
id:40,
url:http://initech.zendesk.com/api/v2/users/40.json,
external_id:e79e166e-fcb5-4604-9466-1ea9777c6eb5,
name:Burgess England,
alias:Mr Neal,
created_at:2016-07-16T09:13:47 -10:00,
active:True,
verified:False,
shared:True,
locale:zh-CN,
timezone:Taiwan,
last_login_at:2015-10-20T01:25:19 -11:00,
email:nealengland@flotonic.com,
phone:9675-282-161,
signature:Don't Worry Be Happy!,
organization_id:113,
tags:[Riceville,Ribera,Caberfae,Breinigsville],
suspended:True,
role:end-user

The ticket was assgined by user Francis Bailey(23)

The information of user Francis Bailey:
id:23,
url:http://initech.zendesk.com/api/v2/users/23.json,
external_id:e9db9277-af4a-4ca6-99e0-291c8a97623e,
name:Francis Bailey,
alias:Miss Singleton,
created_at:2016-03-21T07:12:28 -11:00,
active:True,
verified:False,
shared:False,
locale:en-AU,
timezone:Antarctica,
last_login_at:2012-12-01T11:14:01 -11:00,
email:singletonbailey@flotonic.com,
phone:9584-582-815,
signature:Don't Worry Be Happy!,
organization_id:101,
tags:[Leola,Graball,Yogaville,Tivoli],
suspended:False,
role:agent

The ticket is from organisation: 123

The information of organization (Terrasys) is shown below:
id:123,
url:http://initech.zendesk.com/api/v2/organizations/123.json,
external_id:12831719-9173-47c7-8834-fa5b26877393,
name:Terrasys,
domain_names:[isoplex.com,equicom.com,premiant.com,combogen.com],
created_at:2016-04-23T04:40:09 -10:00,
details:MegaCorp,
shared_tickets:MegaCorp,
tags:[Fisher,Forbes,Koch,Lester]

----------------------------------------------------------------------------------
```
### 4. Search through organizations first and return the result and the related users and tickets for each user in the result.
There is only one group of result will be returned. See the example for search "kage.com" below, the result is showing a user information in the result:
```
There are 1 organizations show in the result

The information of organization (Enthaze) is shown below:
id:101,
url:http://initech.zendesk.com/api/v2/organizations/101.json,
external_id:9270ed79-35eb-4a38-a46f-35725197ea8d,
name:Enthaze,
domain_names:[kage.com,ecratic.com,endipin.com,zentix.com],
created_at:2016-05-21T11:10:28 -10:00,
details:MegaCorp,
shared_tickets:MegaCorp,
tags:[Fulton,West,Rodriguez,Farley]


There are 4 users belongs to organization Enthaze(101). The information of these users are shown below:

The information of user Loraine Pittman:
id:5,
url:http://initech.zendesk.com/api/v2/users/5.json,
external_id:29c18801-fb42-433d-8674-f37d63e637df,
name:Loraine Pittman,
alias:Mr Ola,
created_at:2016-06-12T08:49:19 -10:00,
active:True,
verified:False,
shared:False,
locale:zh-CN,
timezone:Monaco,
last_login_at:2013-07-03T06:59:27 -10:00,
email:olapittman@flotonic.com,
phone:9805-292-618,
signature:Don't Worry Be Happy!,
organization_id:101,
tags:[Frizzleburg,Forestburg,Sandston,Delco],
suspended:False,
role:admin


The information of user Francis Bailey:
id:23,
url:http://initech.zendesk.com/api/v2/users/23.json,
external_id:e9db9277-af4a-4ca6-99e0-291c8a97623e,
name:Francis Bailey,
...

The information of user Haley Farmer:
id:27,
url:http://initech.zendesk.com/api/v2/users/27.json,
external_id:ee53ec4a-8ae1-4090-8f27-ce511cc292f7,
name:Haley Farmer,
...


The information of user Herrera Norman:
id:29,
url:http://initech.zendesk.com/api/v2/users/29.json,
external_id:5cf7c032-b3cb-4c87-afa1-57fc9f94e9a1,
name:Herrera Norman,
...


There are 4 tickets belongs to organization Enthaze(101). The information of these tickets are shown below:

The information of ticket (b07a8c20-2ee5-493b-9ebf-f6321b95966e) is shown below:
id:b07a8c20-2ee5-493b-9ebf-f6321b95966e,
external_id:http://initech.zendesk.com/api/v2/tickets/b07a8c20-2ee5-493b-9ebf-f6321b95966e.json,
created_at:2016-03-21T11:18:13 -11:00.
type:question,
subject:A Drama in Portugal,
description:Laborum exercitation officia nulla in. Consequat et commodo fugiat velit magna sunt mollit.,
priority:low,
status:hold,
submitter_id:50,
assignee_id:17,
organization_id:101,
tags:Ohio,Pennsylvania,American Samoa,Northern Mariana Islands,
has_incidents:True,
due_at:2016-08-04T12:30:08 -10:00,
via:web

The information of ticket (c22aaced-7faa-4b5c-99e5-1a209500ff16) is shown below:
id:c22aaced-7faa-4b5c-99e5-1a209500ff16,
external_id:http://initech.zendesk.com/api/v2/tickets/c22aaced-7faa-4b5c-99e5-1a209500ff16.json,
created_at:2016-07-11T08:52:25 -10:00.
...


The information of ticket (89255552-e9a2-433b-970a-af194b3a39dd) is shown below:
id:89255552-e9a2-433b-970a-af194b3a39dd,
external_id:http://initech.zendesk.com/api/v2/tickets/89255552-e9a2-433b-970a-af194b3a39dd.json,
...


The information of ticket (27c447d9-cfda-4415-9a72-d5aa12942cf1) is shown below:
id:27c447d9-cfda-4415-9a72-d5aa12942cf1,
external_id:http://initech.zendesk.com/api/v2/tickets/27c447d9-cfda-4415-9a72-d5aa12942cf1.json,
...
```
# Architecture Design
## Design Goal
### 1 Quality
The main design goals for this system are:
#### Usability
End users shall be able to use this system easily without extensive training.
#### Scalability
This system shall be able to scale in the future, such as add a new feature.
#### Maintainability
The components of this system shall be able to modified without huge cost.
### 2 Principle
1. High cohesion and low coupling to support maintainability and scalability.
2. Single-responsiblity principle — A class should have one and only one reason to change, meaning that a class should have only one job.
3. Open-closed principle — Objects or entities should be open for extension, but closed for modification.
4. Liskov substitution principle — Every subclass/derived class should be substitutable for their base/parent class.
5. Interface segregation principle — A client should never be forced to implement an interface that it doesn't use or clients shouldn't be forced to depend on methods they do not use.
6. Dependency Inversion Principle — Entities must depend on abstractions not on concretions. It states that the high level module must not depend on the low level module, but they should depend on abstractions.
## High-Level Design
To achieve the goal of 'low coupling, high cohesion; open to extension, close to modification'. Layering Design and the design pattern for each layer have been applied. The application contains 5 main layers.
#### 1. Presentation layer
Presentation layer is the entry point in this application which is in charge of interacting with users. Also, it interacts with the Service layer according to users' reaction. This layer plays an important role of separating the user interface with domain logics.
#### 2. Service layer
Service layer interacts with the Presentation layer and Domain Logic layer which helps preventing exposing domain logics to the Presentation layer. Also, this layer can help sharing logic with multiple user interfaces without exposing domain logic details. Service layer gives the system more flexibilities to reuse domain logics.
In this search CLI, the system provides 4 services through this service layer without exposing domain logic details to the presentation layer.
#### 3. Domain Logic layer
The domain logic layer aims to implement the business rules. For example, all the search processes happen in this layer. Domain model pattern has been chosen and implemented which provides high extensibility for the system. Additional modules or changes can be easily added into the system if needed in the future. Besides, the domain logic of the system is divided into different modules according to different models or related operations, relevant functions are grouped into one module, which performs high cohesion and low coupling between modules.
#### 4. Entity
Entity layers is the layer to define all entities. There are 6 entities involved in the current system, including User, UserResult, Ticket, TicketResult, Organization, OrganizationResult. Those entities are used cross the system.
#### 5. Data Layer
Data Layer takes care of fetching data from data source and converting to useful objects or entities. In this search CLI, the data sources are json files. In this layer, these data are converted to User, Ticket and Organization entities. Separating data layer from the domain logic layer makes it easier to debug and extend the ways to get data. Same domain logics can be applied for data from different data resource, so it can improve the reusability as well.
