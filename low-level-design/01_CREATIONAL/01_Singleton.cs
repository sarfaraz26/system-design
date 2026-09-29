//Use when only one instance of object is required throughout the application.

using System;

public sealed class Singleton
{
    private static Singleton _instance;
    private static object locker = new object();
    
    private Singleton(){}

    public static Singleton GetInstance()
    {
        //for peformance - to avoid unneccessary checks
        if(_instance == null)    
        {
            //allows only single thread to access inside block
            lock(locker)    
            {
                if(_instance == null)
                    _instance = new Singleton();
            }
        }

        return _instance;
    }
}

public class Client
{
    public static void Main(String[] args)
    {
        Console.WriteLine(Singleton.GetInstance());
    }
}