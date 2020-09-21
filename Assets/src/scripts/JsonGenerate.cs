using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonGenerate : MonoBehaviour
{
    public Dictionary<string, Dictionary<string, int>> json;
    void MovementGenerator()
    {
        List<Dictionary<string, int>> engine = new List<Dictionary<string, int>>();
        this.json = new Dictionary<string, Dictionary<string, int>>();
        
        for (int i = 0; i < 4; i++)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();
            temp.Add("rotacao", Random.Range(0, 361));
            temp.Add("movimento", Random.Range(-7, 7));
            engine.Add(temp);
            this.json.Add("Motor"+(i+1), engine[i]);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovementGenerator();
        
    }
}
