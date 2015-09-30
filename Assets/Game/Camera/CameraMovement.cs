﻿using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    private Vector3 goalPosition;
    private Vector3 startPosition;

    private int goToYOffset = 2;

    [SerializeField]
    private float speed;

    [SerializeField]
    private int scrollSpeed;

    [SerializeField]
    private float maxFieldView;
    [SerializeField]
    private float minFieldView;

    [SerializeField]
    private int width;
    public int Width
    {
        get { return width; }
        set { width = value; }
    }

    [SerializeField]
    private int height;
    public int Height
    {
        get { return height; }
        set { height = value; }
    }

    [SerializeField]
    private bool noMovement;

    private int zoomGoal;

    Camera myCamera;

    private Vector3 nextMove;
    private float frameSpeed;

	// Use this for initialization
	void Start () {
        myCamera = GetComponent<Camera>();
        startPosition = transform.position;
        zoomGoal = 60;
        goalPosition = startPosition;
	}

    public void moveInDirection(Vector3 direction)
    {
        nextMove += direction * speed * Time.deltaTime;
    }

    public void moveUp()
    {
        if (goalPosition.y < height / 2)
            moveInDirection(new Vector3(0, 1, 0));
    }

    public void moveDown()
    {
        if (goalPosition.y > -height / 2)
            moveInDirection(new Vector3(0, -1, 0));
    }

    public void moveLeft()
    {
        if (goalPosition.x > -width / 2)
            moveInDirection(new Vector3(-1, 0, 0));
    }

    public void moveRight()
    {
        if (goalPosition.x < width / 2)
                moveInDirection(new Vector3(1, 0, 0));
    }
    
    private void zoom(int delta)
    {
        zoomGoal += delta;
        zoomGoal = (int)Mathf.Clamp(zoomGoal, minFieldView, maxFieldView);
    }

    public void zoomIn()
    {
        int delta = -scrollSpeed;
        zoom(delta);
    }

    public void zoomOut()
    {
        int delta = scrollSpeed;
        zoom(delta);
    }

	// Update is called once per frame
    void Update()
    {
        //Check mouse position
        if(!noMovement)
        {
            Vector2 mousePos = Input.mousePosition;
            if (mousePos.x > Screen.width)
                moveRight();
            else if (mousePos.x < 0)
                moveLeft();
            if (mousePos.y > Screen.height)
                moveUp();
            else if (mousePos.y < 0)
                moveDown();
        }

        if (Input.GetKey(KeyCode.RightArrow))
            moveRight();
        if (Input.GetKey(KeyCode.LeftArrow))
            moveLeft();
        if (Input.GetKey(KeyCode.UpArrow))
            moveUp();
        if (Input.GetKey(KeyCode.DownArrow))
            moveDown();

         move();
	}

    private void move()
    {
        float frameSpeed = speed * Time.deltaTime;
        nextMove.x = Mathf.Clamp(nextMove.x, -frameSpeed, frameSpeed);
        nextMove.y = Mathf.Clamp(nextMove.y, -frameSpeed, frameSpeed);
        nextMove.y = Mathf.Clamp(nextMove.y, -frameSpeed, frameSpeed);
        goalPosition += nextMove;
        transform.position = Vector3.Lerp(transform.position, goalPosition, speed * Time.deltaTime);
        myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, zoomGoal, 0.3f);
        nextMove = Vector3.zero;
    }

    public void goToPosition(Vector3 position)
    {
        goalPosition.x = position.x;
        goalPosition.y = position.y - goToYOffset;
    }
}
