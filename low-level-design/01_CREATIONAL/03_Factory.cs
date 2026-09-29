//Factory decides which object to create rather than caller class decides.
using System;

public interface INotificationService
{
	string Send();
}

public class EmailNotificationService : INotificationService
{
	public string Send()
	{
		return "Email send";
	}
}

public class SMSNotificationService : INotificationService
{
	public string Send()
	{
		return "SMS send";
	}
}

public class NotificationServiceFactory
{
	public static INotificationService Create(string type)
	{
		switch(type)
		{
			case "sms":
					return new SMSNotificationService();
			case "email":
					return new EmailNotificationService();
			default:
				throw new Exception("Invalid type");
		}
	}
}


public class Program
{
	public static void Main(String[] args)
	{
		//type can come from another method as well based on some business logic
		string type = "email";	
		INotificationService obj = NotificationServiceFactory.Create(type);
		
		Console.WriteLine(obj.Send());
	}

}