using UnityEngine;
using System.Collections;

public class GameState : State {

    private CameraMovement camera;
    
    /// <summary>Constructor.</summary>
    public GameState(StateManager stateManager)
        : base(stateManager)
    {
    }

    /// <summary>Called on start.</summary>
    /// <returns>void</returns>
    public override void start()
    {
        EventManager.AddListener(EnumEvent.GAMELOST, playerLost);
        camera = Camera.main.GetComponent<CameraMovement>();
	}

    public void playerLost()
    {
        Debug.Log("Lost");
    }

    /// <summary>Called when leaving this state.</summary>
    /// <returns>void</returns>
    public override void end()
    {
        EventManager.RemoveListener(EnumEvent.REACHEDBASE, playerLost);
    }

    /// <summary>Called each frame.</summary>
    /// <returns>void</returns>
    public override void update()
    {

    }

    /// <summary>Called when the lobby scene from Unity is loaded.</summary>
    /// <param name="lvl">Id of the level loaded.</param>
    /// <returns>void</returns>
    public override void onLevelWasLoaded(int lvl)
    {
    }

    /// <summary>Recieves all the necessary inputs (keyboard, gamepad and mouse).</summary>
    /// <param name="key">The input sent.</param>
    /// <returns>void</returns>
    public override void noticeInput(EnumInput key)
    {
        if (key == EnumInput.RETURN)
            EventManager.Raise(EnumEvent.START);
        if (key == EnumInput.UP)
            camera.moveUp();
        else if (key == EnumInput.DOWN)
            camera.moveDown();
        if (key == EnumInput.LEFT)
            camera.moveLeft();
        else if (key == EnumInput.RIGHT)
            camera.moveRight();
        if (key == EnumInput.SCROLLUP)
            camera.zoomIn();
        if (key == EnumInput.SCROLLDOWN)
            camera.zoomOut();
    }

    /// <summary>Recieves all the necessary inputs (touchscreen & mobile phone buttons).</summary>
    /// <param name="key">The input sent.</param>
    /// <param name="inputs">Array containing the touch inputs.</param>
    /// <returns>void</returns>
    public override void noticeInput(EnumInput key, Touch[] inputs)
    {

    }
}
