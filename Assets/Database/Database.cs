using UnityEngine;
using System;
using System.Collections;

public class Database : MonoBehaviour
{
    public void login(string username, string password, Callback loginSuccessfull, Callback<string> loginFail)
    {
        WWWForm form = new WWWForm();
        form.AddField("user", username);
        form.AddField("password", password);
        WWW w = new WWW("http://www.vstuder.com/towerWar/login.php", form);
        StartCoroutine(loginCoroutine(w, loginSuccessfull, loginFail));
    }

    public void registerAccount(string username, string password, Callback registerSuccessfull, Callback<string> registerFail)
    {
        WWWForm form = new WWWForm();
        form.AddField("user", username);
        form.AddField("password", password);
        WWW w = new WWW("http://www.vstuder.com/towerWar/register.php", form);
        StartCoroutine(registerCoroutine(w, registerSuccessfull, registerFail));
    }

    public void getLevel(int id, Callback<string> levelSuccessfull, Callback<string> levelFail)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("action", "get");
        WWW w = new WWW("http://www.vstuder.com/towerWar/level.php", form);
        StartCoroutine(levelCoroutine(w, levelSuccessfull, levelFail));
    }

    public void setLevel(int id, int level, Callback<string> levelSuccessfull = null, Callback<string> levelFail = null)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("action", "set");
        form.AddField("level", level);
        WWW w = new WWW("http://www.vstuder.com/towerWar/level.php", form);
        StartCoroutine(levelCoroutine(w, levelSuccessfull, levelFail));
    }

    public void requestFriendship(int id1, int id2, Callback<string> levelSuccessfull = null, Callback<string> levelFail = null)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id1);
        form.AddField("action", "request");
        form.AddField("friendId", id2);
        WWW w = new WWW("http://www.vstuder.com/towerWar/relationship.php", form);
        StartCoroutine(friendshipCoroutine(w, levelSuccessfull, levelFail));
    }

    //id of the player that refuses
    public void refuseFriendship(int id, Callback<string> levelSuccessfull = null, Callback<string> levelFail = null)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("action", " refuse");
        WWW w = new WWW("http://www.vstuder.com/towerWar/relationship.php", form);
        StartCoroutine(friendshipCoroutine(w, levelSuccessfull, levelFail));
    }

    public void confirmFriendship(int id1, int id2, Callback<string> levelSuccessfull = null, Callback<string> levelFail = null)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id1);
        form.AddField("action", "confirm");
        form.AddField("friendId", id2);
        WWW w = new WWW("http://www.vstuder.com/towerWar/relationship.php", form);
        StartCoroutine(friendshipCoroutine(w, levelSuccessfull, levelFail));
    }

    public void listFriendship(int id, Callback<string> levelSuccessfull = null, Callback<string> levelFail = null)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("action", "list");
        WWW w = new WWW("http://www.vstuder.com/towerWar/relationship.php", form);
        StartCoroutine(friendshipCoroutine(w, levelSuccessfull, levelFail));
    }

    public void deleteFriendship(int id, Callback<string> levelSuccessfull = null, Callback<string> levelFail = null)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("action", "delete");
        WWW w = new WWW("http://www.vstuder.com/towerWar/relationship.php", form);
        StartCoroutine(friendshipCoroutine(w, levelSuccessfull, levelFail));
    }

    void Start()
    {
        listFriendship(1, success, error);
    }

    public void success(string e)
    {
        Debug.Log(e);
        //confirmFriendship(2,1, success2, error);
    }

    public void success2(string e)
    {
        Debug.Log(e);
    }

    public void error(string e)
    {
        Debug.Log(e);
    }

    IEnumerator friendshipCoroutine(WWW w, Callback<string> friendSuccessfull, Callback<string> friendFail)
    {
        yield return w;
        string message = "";
        if (w.error == null)
        {
            if (friendSuccessfull != null)
                friendSuccessfull(w.text);
        }
        else
        {
            message += "ERROR: " + w.error + "\n";
            if (friendFail != null)
                friendFail(message);
        }
    }

    IEnumerator levelCoroutine(WWW w, Callback<string> levelSuccessfull, Callback<string> levelFail)
    {
        yield return w;
        string message = "";
        if (w.error == null)
        {
            if (levelSuccessfull != null)
                levelSuccessfull(w.text);
        }
        else
        {
            message += "ERROR: " + w.error + "\n";
            if (levelFail != null)
                levelFail(message);
        }
    }

    IEnumerator loginCoroutine(WWW w, Callback loginSuccessfull, Callback<string> loginFail)
    {
        yield return w;
        string message = "";
        if (w.error == null)
        {
            if (w.text == "login-SUCCESS")
            {
                loginSuccessfull();
            }
            else
            {
                message += w.text;
                loginFail(message);
            }
        }
        else
        {
            message += "ERROR: " + w.error + "\n";
            loginFail(message);
        }
    }

    IEnumerator registerCoroutine(WWW w, Callback registerSuccessfull, Callback<string> registerFail)
    {
        yield return w;
        string message = "";
        if (w.error == null)
        {
            message += w.text;
            registerSuccessfull();
        }
        else
        {
            message += "ERROR: " + w.error + "\n";
            registerFail(message);
        }
    }
}
