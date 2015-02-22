using UnityEngine;
using System.Collections;

public class DrawGrid : MonoBehaviour {

    public float width;
    public float height;

    [SerializeField]
    private int size;

    void Start()
    {
    }

    void Update()
    {
    }

    void OnDrawGizmos()
    {
        Vector3 pos = transform.position;

        if(height != 0)
        {
            for (float y = pos.y - size; y <= pos.y + size; y += height)
            {
                Gizmos.DrawLine(new Vector3(-size + pos.x, Mathf.Floor(y / height) * height, 0.0f),
                                new Vector3(size + pos.x, Mathf.Floor(y / height) * height, 0.0f));
            }
        }
        if(width != 0)
        {
            for (float x = pos.x - size; x <= pos.x + size; x += width)
            {
                Gizmos.DrawLine(new Vector3(Mathf.Floor(x / width) * width, -size + pos.y, 0.0f),
                                new Vector3(Mathf.Floor(x / width) * width, size + pos.y, 0.0f));
            }
        }
    }
}
