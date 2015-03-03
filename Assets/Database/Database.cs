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
        WWW w = new WWW("http://www.vstuder.com/towerWar/getLevel.php", form);
        StartCoroutine(levelCoroutine(w, levelSuccessfull, levelFail));
    }

    public void setLevel(int id, int level, Callback<string> levelSuccessfull = null, Callback<string> levelFail = null)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("action", "set");
        form.AddField("level", level);
        WWW w = new WWW("http://www.vstuder.com/towerWar/getLevel.php", form);
        StartCoroutine(levelCoroutine(w, levelSuccessfull, levelFail));
    }

    void Start()
    {
        setLevel(0, 4);
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
