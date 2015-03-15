using UnityEngine;
using System.Collections;

public class FxSpawner : MonoBehaviour {

    private static FxSpawner instance;
    public static FxSpawner Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<FxSpawner>();
            return instance;
        }
    }

    [SerializeField]
    private Factory factory;

    [SerializeField]
    private Catalog catalog;

    public void spawn(int id, Vector3 pos)
    {
        factory.spawn(id, pos);
    }
}
