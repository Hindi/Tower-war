using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextDB : MonoBehaviour 
{
    Dictionary<string, string> engDict;
    Dictionary<string, string> frDict;
    Dictionary<string, string> curdict;

    private static TextDB instance;
    public static TextDB Instance()
    {
        if (instance == null)
            instance = GameObject.FindObjectOfType<TextDB>().GetComponent<TextDB>();

        return instance;
    }

	// Use this for initialization
	void Start () {
        engDict = new Dictionary<string, string>();
        frDict = new Dictionary<string, string>();

        CSVReader.FillDictionaries(frDict, engDict);

        switch(Application.systemLanguage)
        {
            case SystemLanguage.French:
                curdict = frDict;
                break;
            case SystemLanguage.English:
            default:
                curdict = engDict;
                break;
        }

        EventManager.Raise(EnumEvent.LOADTEXTS);
	}

    public string getText(string key, params string[] replaceList)
    {
        if (curdict.ContainsKey(key))
        {
            string value = curdict[key];
            for(int i = 0; i < replaceList.Length; ++i)
            {
                value = value.Replace("%" + i, replaceList[i]);
            }
            return value;
        }
        else
            return "######################";
    }
}
