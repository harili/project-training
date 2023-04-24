# Subject TDD

### Step 1 - the Business Model
This exercise is a challenge of hexagonal architecture c#, it is implemented by step with a first focus on the business domain in TDD.

Implementation of the business logic of a bank account:

Expected functionalities:
```
1. Money deposit
2. Money withdrawal
3. View current balance
4. Consult previous transactions
```


And in this exercise, you have to propose a suitable object modeling of the entities needed for these functionalities. With unit tests in addition


### Step 2 - API Adapter

Develop functionalities in an API (Web API .NET for example)

### Step 3 - Persistance adapter

Implementation of persistance adapter of your choice (SQLlite, H2, ...).

Unlike MVC architecture, hexagonal architecture requires developing and validating the business domain before working on any other software brick. Your commit history SHOULD reflect this order. This domain MUST be validated by unit tests (example .NET :xUnit + Fact). For implementing tests, you can use a TDD approach.
