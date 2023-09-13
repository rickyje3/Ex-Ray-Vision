using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class DeserializeAPResults : MonoBehaviour
{
    public APResultsArray DeserializedAPResults = new APResultsArray(); //Defines and places things in Array

    [Header("where we're dragging and dropping the json file")]
    public TextAsset InputFile; //where we're dragging and dropping the json file

    public void RTScan()
    {
        //Counts the number of classes in json
        DeserializedAPResults = JsonConvert.DeserializeObject<APResultsArray>(InputFile.text); 
    }

}

[System.Serializable]
public class APResultsArray 
{
    public APResult[] APResults;
}

[System.Serializable]
public class APResult
{
    public string ssid; //name
    public int signal; //strength
    public long encryption; //type 2 or 5 ghz
    public int channel; 
    public int hidden;
}


