using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        var dict = new Dictionary<string, int>();

        // Dodajemo elemente u rečnik
        dict.Add("jedan", 1);
        dict.Add("dva", 2);
        //dict.Add("dva", 2);
        dict.Add("tri", 3);

        Console.WriteLine("Vrednost za ključ 'dva': " + dict["dva"]);

        dict["dva"] = 5; // ažuriranje vrednosti
        Console.WriteLine("Nova vrednost za ključ 'dva': " + dict["dva"]);

        //Proveravamo da li postoji ključ ili vrednost
        Console.WriteLine("Sadrži ključ 'tri'? " + dict.ContainsKey("tri"));
        Console.WriteLine("Sadrži vrednost 1? " + dict.ContainsValue(1));

        dict.Remove("jedan");

        Console.WriteLine("Preostali elementi:");
        foreach (var kvp in dict)
        {
            Console.WriteLine($"{kvp.Key} -> {kvp.Value}");
        }

        Console.WriteLine($"Broj elemenata: {dict.Count}");

        // Test performansi za dodavanje 10000 elemenata u rečnik
        Stopwatch sw = Stopwatch.StartNew();
        var intDict = new Dictionary<int, int>();
        for (int i = 0; i < 10000; i++)
            intDict.Add(i, i); // dodajemo elemente (ključ i vrednost su isti)
        sw.Stop();

        // Ispis vremena potrebnog za dodavanje 10000 elemenata
        Console.WriteLine($"Dodavanje 10000 elemenata: {sw.ElapsedMilliseconds} ms");
    }
}

