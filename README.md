# 017 Task Based Async Pattern

## Lecture

[![# One To One Relationship](https://img.youtube.com/vi/hNoaqRD51Mo/0.jpg)](https://www.youtube.com/watch?v=hNoaqRD51Mo)

## Instructions

In `HomeEnergyApi/Dtos/HomeDto.cs`
- Create a public class `HomeDto`
    - Give `HomeDto` the following public properties / types
        - OwnerLastName / `string`
            - Give OwnerLastName the "Required" attribute
        - StreetAddress / `string?`
            - Give StreetAddress validation to ensure it cannot be longer than 40 characters
        - City / `string?`
        - MonthlyElectricUsage / `int?`
        - ProvidedUtilities / `ICollection<string>?`
    - Ensure all properties have getters/setters

In `HomeEnergyApi/Models/HomeModel.cs`
- Remove all validation from properties on `Home`

In `HomeEnergyApi/Models/HomeAdminControllers.cs`
- On `HomeAdminController`, create a new public method `Map()`
    - This method should return a `Home`
    - This method should accept one argument of type `HomeDto`
    - The method should create a new `Home` to be returned by passing the `OwnerLastName`, `StreetAddress`, and `City` properties on the passed `HomeDto` into the `Home` constructor.
    - If the passed `HomeDto` has a non-null `MonthlyElectricUsageValue`...
        - Create a new `HomeUsageData` with it's `MonthlyElectricUsage` property set to the value of the passed `HomeDto.MonthlyElectricUsage`
        - On the `Home` to be returned, set `HomeUsageData` to the newly created `HomeUsageData`
    - If the passed `HomeDto` has a non null `ProvidedUtilities`...
        - Add a new `UtilityProvider` on the home to be returned for each value in the `ProvidedUtilites` on the passed `HomeDto`, where its `ProvidedUtility` corresponds to a value in `ProvidedUtilities` on the passed `HomeDto`
            - (ex. If the passed `HomeDto.ProvidedUtilities` is a list with the values "Water" and "Gas", the returned home should have two `UtilityProvider`s, one with a `ProvidedUtility` of "Water" and the other with "Gas")
- Convert `CreateHome()` to take one argument of type `HomeDto`
    - Refactor the method so that the passed `HomeDto` is mapped to a home using the `Map()` function and a home is still saved and returned in the body of the method
- Convert `UpdateHome()` to take one argument of type `HomeDto`
    - Refactor the method so that the passed `HomeDto` is mapped to a home using the `Map()` function and a home is still updated and returned in the body of the method if found.

In your terminal
- ONLY IF you are working on codespaces or a different computer/environment as the previous lesson and don't have `dotnet-ef` installed globally, run `dotnet tool install --global dotnet-ef`, otherwise skip this step
    - To check if you have `dotnet-ef` installed, run `dotnet-ef --version`
- Run `dotnet ef migrations add AddHomeDtoChanges`
- Run `dotnet ef database update`
    
## Additional Information
- Some Models may have changed for this lesson from the last, as always all code in the lesson repository is available to view
- Along with `using` statements being added, any packages needed for the assignment have been pre-installed for you, however in the future you may need to add these yourself

## Building toward CSTA Standards:
- Decompose problems into smaller components through systematic analysis, using constructs such as procedures, modules, and/or objects (3A-AP-17)
- Create artifacts by using procedures within a program, combinations of data and procedures, or independent but interrelated programs (3A-AP-18)
- Evaluate the tradeoffs in how data elements are organized and where data is stored (3A-DA-10)
- Compare and contrast fundamental data structures and their uses (3B-AP-12)
- Construct solutions to problems using student-created components, such as procedures, modules and/or objects (3B-AP-14)

## Resources
- https://en.wikipedia.org/wiki/Data_transfer_object

Copyright &copy; 2025 Knight Moves. All Rights Reserved.
