# 020 Automapper

## Lecture

[![# Automapper (Part 1)](https://img.youtube.com/vi/LUOpye2AxVk/0.jpg)](https://www.youtube.com/watch?v=LUOpye2AxVk)
[![# Automapper (Part 2)](https://img.youtube.com/vi/qwR-L7Eh4Gs/0.jpg)](https://www.youtube.com/watch?v=qwR-L7Eh4Gs)

## Instructions

In `HomeEnergyApi/Dtos/HomeProfile.cs`
- Create a public class `HomeProfile` Implementing `Profile`
    - Give `HomeProfile` a constructor taking zero arguments
        - In the body of the constructor, using the lecture as an example and the resource linked at the end of the lesson, write an AutoMapper profile that can replace the functionality in the `Map()` method in `HomeEnergyApi/Models/HomeAdminControllers.cs`

In `HomeEnergyApi/Controllers/HomeAdminControllers.cs`
- Add a private property `mapper` of type `IMapper`
- Add an argument of type `IMapper` to `HomeAdminController`s constructor, and set it's value to the newly created property `mapper`
- Remove the Map() method
- Replace any refrences to the removed `Map()` method, to instead use the newly created `mapper` property

In `HomeEnergyApi/Program.cs`
- Add an AutoMapper service, passing the `HomeProfile` type as an argument.
    
## Additional Information
- Some Models may have changed for this lesson from the last, as always all code in the lesson repository is available to view
- Along with `using` statements being added, any packages needed for the assignment have been pre-installed for you, however in the future you may need to add these yourself

## Building toward CSTA Standards:
- Create prototypes that use algorithms to solve computational problems by leveraging prior student knowledge and personal interests (3A-AP-13) https://www.csteachers.org/page/standards
- Decompose problems into smaller components through systematic analysis, using constructs such as procedures, modules, and/or objects (3A-AP-17) https://www.csteachers.org/page/standards
- Create artifacts by using procedures within a program, combinations of data and procedures, or independent but interrelated programs (3A-AP-18) https://www.csteachers.org/page/standards
- Use and adapt classic algorithms to solve computational problems (3B-AP-10) https://www.csteachers.org/page/standards
- Demonstrate code reuse by creating programming solutions using libraries and APIs (3B-AP-16) https://www.csteachers.org/page/standards

## Resources
- https://github.com/AutoMapper/AutoMapper

Copyright &copy; 2025 Knight Moves. All Rights Reserved.
