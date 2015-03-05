using UnityEngine;
using System.Collections;

public class Friend {

    public Friend(int id)
    {

    }

    private int id;
    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    private string name;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    private int level;
    public int Level
    {
        get { return level; }
        set { level = value; }
    }

}
