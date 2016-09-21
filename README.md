# ModelMapper
Maps one type object to another. 
<br/>Supporting custom names and ignored properties.

#Installation

You can install package from nuget: <code>Install-Package ModelMapping</code>

#Code Example

```
  public class User
  {
      public int Id { get; set; }
      public string Name { get; set; }
  }
  public class UserVM
  {
      public int Id { get; set; }
      public string Name { get; set; }
  }
  IModelMapper<User, UserVM> mapper = new ModelMapper<User, UserVM>();
  var user = new User() { Id = 1, Name = "beqa" };
  UserVM viewmodel = mapper.MapToViewModel(user);
  // viewmodel.Id == 1
  // viewmodel.Name == "beqa"
```
