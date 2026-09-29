// Builder decides HOW to construct a complex object.
// gives chaining support.
// Although objects can be create via parametrized constructor
// but this in helpful in case of complex & optional properties in class.
// Application : Fluent Validation

using System;

public class User
{
	//Required fields
	public int Id {get; set;}
	public string Name {get;set;}
	public int Age {get; set;}
	
	//Optional fields
	public string? Email {get; set;}
	public bool? isNRI {get; set;}
}

public class UserBuilder
{
	private readonly User _user;
	
	public UserBuilder()
	{
		_user = new User();	
	}

	public UserBuilder SetId(int id)
	{
		_user.Id = id;
		return this;
	}
	
	public UserBuilder SetName(string name)
	{
		_user.Name = name;
		return this;
	}
	
	public UserBuilder SetAge(int age)
	{
		_user.Age = age;
		return this;
	}
	
	public User Build()
	{
        //We can add validations as well
        if(string.IsNullOrEmpty(_user.Name))
            throw new Exception("Name cannot be empty");
            
		return _user;
	}
}

public class Program
{
	public static void Main()
	{
		User user = new UserBuilder()
						.SetName("Sarfaraz")
						.SetAge(12)
						.SetId(1)
			.Build();
	}
}