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
}

public class LinqExample : MonoBehaviour
{
    List<Avengers> alist = new List<Avengers>
    {
        new Avengers {Name = "Iron", Waepon = new string[] {"MK.01", "MK.44", "MK85"}},
        new Avengers {Name = "Thor", Waepon = new string[] {"Mjolnir", "Storm Breaker"}},
        new Avengers {Name = "Captain", Waepon = new string[] {"Mjolnir", "Shield"}},
    };

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
        DebugLog(q);

        // /////////////////////////// All
        q = from hero in alist
            where hero.Waepon.All(item => item.Length == 5)
            select hero.Name;
        DebugLog(q);

        // /////////////////////////// Any
        q = from hero in alist
            where hero.Waepon.Any(item=>item.StartsWith("M"))
            select hero.Name;
        DebugLog(q);
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
