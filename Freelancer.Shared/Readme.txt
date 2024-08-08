This project is a common reference to both Web and Core projects.
It allows both project to have references to common classes without causing circular references.
E.g. code in the Core project can access a generated Model, and code in the Web project can access the same model with setting a reference to Core
otherwise causing a circlur reference.

Common uses are:
-ModelBuilder models
-ViewComponent models
-Interface definitions
-Request/Response models
-DTO models
-etc