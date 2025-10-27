using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using JetBrains.Annotations;

class Avengers
{
    public string Name { get; set; }
    public string[] Waepon { get; set; }
    public int CategoryId { get; set; }
}

class Category
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
}

public class LinqExample : MonoBehaviour
{
    List<Avengers> alist = new List<Avengers>
    {
        new Avengers {Name = "Iron", Waepon = new string[] {"MK.01", "MK.44", "MK85"}},
        new Avengers {Name = "Thor", Waepon = new string[] {"Mjolnir", "Storm Breaker"}},
        new Avengers {Name = "Captain", Waepon = new string[] {"Mjolnir", "Shield"}},
    };

    List<Avengers> alist2 = new List<Avengers>
    {
        new Avengers { Name = "Iron", CategoryId = 0},
        new Avengers { Name = "Captain", CategoryId = 0},
        new Avengers { Name = "Thor", CategoryId = 1},
        new Avengers { Name = "Loki", CategoryId = 1},
    };

    List<Category> categoryis = new List<Category>
    {
        new Category { Id = 0, CategoryName = "Human"},
        new Category { Id = 1, CategoryName = "Not Human"}
    };

    class Marvel
    {
        public string Name { get; set; }

    }
    class MarvelPhychic : Marvel
    {
        public string HeroType { get; set;}
    
    }

    void Start()
    {
        string[] avengers = { "Iron", "Captain", "Thor", "Hulk", "Black", "Spider" };

        /////////////////////////// OrderBy
        IEnumerable<string> q =
                                from hero in avengers
                                orderby hero.Length
                                select hero;
        //DebugLog(q);

        /////////////////////////// OrderByDescending
        q = from hero in avengers
            orderby hero.Substring(0, 1) descending
            select hero;
        //DebugLog(q);

        q = from hero in avengers
            orderby hero.Length, hero.Substring(0, 1)
            select hero;
        //DebugLog(q);

        q = from hero in avengers
            orderby hero.Length, hero.Substring(0, 1) descending
            select hero;
        //DebugLog(q);


        /////////////////////////// Distinct
        string[] avengers1 = { "Iron", "Iron", "Thor", "Captin" };

        q = from hero in avengers1.Distinct()
            select hero;
        //DebugLog(q);

        /////////////////////////// Except
        string[] avengers21 = { "Iron", "Captain", "Thor", "Hulk" };
        string[] avengers22 = { "Iron", "Hulk", "Captin" };
        q = from hero in avengers21.Except(avengers22)
            select hero;
        //DebugLog(q);

        ///////////////////////////Intersect
        q = from hero in avengers21.Intersect(avengers22)
            select hero;
        //DebugLog(q);

        /////////////////////////// Union
        q = from hero in avengers21.Union(avengers22)
            select hero;
        //DebugLog(q);

        /////////////////////////// Where
        q = from hero in avengers
            where hero.Length == 4
            select hero;
        //DebugLog(q);

        /////////////////////////// All
        q = from hero in alist
            where hero.Waepon.All(item => item.Length == 5)
            select hero.Name;
        //DebugLog(q);

        /////////////////////////// Any
        q = from hero in alist
            where hero.Waepon.Any(item => item.StartsWith("M"))
            select hero.Name;
        //DebugLog(q);

        /////////////////////////// select
        q = from hero in avengers
            select hero.Substring(0, 1);
        //DebugLog(q);

        ////////////////////////// join
        var avengerGroups = from category in categoryis
                            join avenger in alist2 on category.Id
                            equals avenger.CategoryId into avengerGroup
                            select avengerGroup;
        // foreach (var avengerGroup in avengerGroups)
        // {
        //     Debug.Log("Group");
        //     foreach (var avenger in avengerGroup)
        //     {
        //         Debug.Log($"{avenger.Name,20}");
        //     }
        // }

        ////////////////////////// Group
        var numbers = new List<int>() { 3, 2, 4, 66, 13 };
        IEnumerable<IGrouping<int, int>> q2 = from number in numbers
                                              group number by number % 2;
        // foreach (var group in q2)
        // {
        //     Debug.Log(group.Key == 0 ? "Even" : "Odd");
        //     foreach (var i in group)
        //     {
        //         Debug.Log(i);
        //     }
        // }

        ////////////////////////// cast
        Marvel[] marvels = new Marvel[]
        {
            new MarvelPhychic {Name = "Iron", HeroType = "Suit"},
            new MarvelPhychic {Name = "Spider", HeroType = "Suit"},
            new MarvelPhychic {Name = "Thor", HeroType = "God"},
            new MarvelPhychic {Name = "Thanos", HeroType = "Villan"}
        };
        
        var query = from MarvelPhychic marvelPhychic in marvels
                    where marvelPhychic.HeroType == "Suit"
                    select marvelPhychic;
        foreach (var marvel in query)
        {
            Debug.Log(marvel.Name);
        }
        
        


    }


    void DebugLog(IEnumerable<string> query)
    {
        Debug.Log("============ start ============");
        foreach (var item in query)
        {
            Debug.Log(item);
        }
    }
}
