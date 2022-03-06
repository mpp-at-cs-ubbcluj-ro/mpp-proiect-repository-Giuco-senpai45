using System;
using MPP.model;

namespace MPP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Salut");
            Participant p = new Participant("Dani","dani@mail.com","019234");
            p.Id = 3;
            Console.WriteLine(p.Id);
        }
    } 
}