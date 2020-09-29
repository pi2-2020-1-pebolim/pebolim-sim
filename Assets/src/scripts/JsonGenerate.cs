using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonGenerate : MonoBehaviour
{
    
    [SerializeField]
    private Vector2Int positionRange;
    
    [SerializeField]
    private Vector2Int rotationRange;
    
    [SerializeField]
    private float updateTime;
    public Dictionary<string, Dictionary<string, int>> json;
    void MovementGenerator()
    {
        List<Dictionary<string, int>> engine = new List<Dictionary<string, int>>();
        
        var tempJson = new Dictionary<string, Dictionary<string, int>>();
        
        for (int i = 0; i < 4; i++)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();
            temp.Add("rotation", Random.Range(rotationRange.x, rotationRange.y));
            temp.Add("position", Random.Range(positionRange.x, positionRange.y));
            engine.Add(temp);
            tempJson.Add("laneID"+(i+1), engine[i]);
        }

        this.json = tempJson;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MovementGenerator", 0, updateTime);

    }
    
}
