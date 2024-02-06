// See https://aka.ms/new-console-template for more information
using MojZabavniDal.Interface;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        IDataRepo DataRepo;
        DataRepo = new MojZabavniDal.Repo.DataRepo();

    }
}