using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestWhere : MonoBehaviour
{
    //List<string> list = new List<string>() { "ert", "ett", "rty" };
    //private void OnEnable()
    //{
    //    List<Student_1> stuList = new List<Student_1>()
    //        {
    //            new Student_1(){ID=1,Name="John",Chinese=92,Math=88,English=92},
    //            new Student_1(){ID=2,Name="Mary",Chinese=87,Math=94,English=82},
    //            new Student_1(){ID=3,Name="KangKang",Chinese=89,Math=91,English=96},
    //            new Student_1(){ID=4,Name="Maria",Chinese=88,Math=65,English=94},
    //            new Student_1(){ID=5,Name="Ben",Chinese=70,Math=91,English=82},
    //        };

    //    //lambda表达式 表达式内部填的是判断条件
    //    var t1 = (Student_1)stuList.Where(p => p.English == 82);
    //    // Linq 语句
    //    //var t1 = from e in stuList where e.English == 82 select e;
    //    Debug.Log(t1.Name);
    //}
    // Start is called before the first frame update
    void Start()
    {
        List<string> fruits =
    new List<string> { "apple", "passionfruit", "banana", "mango",
                    "orange", "blueberry", "grape", "strawberry" };

        List<string> query = (List<string>)fruits.Where(fruit => fruit.Length < 6);
        string  s=query[0];
        foreach (string fruit in query)
        {
            Debug.Log(fruit);
        }
        /*
         This code produces the following output:

         apple
         mango
         grape
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
