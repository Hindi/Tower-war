using UnityEngine;
using System.Collections;

public class UIHostOrJoin : MonoBehaviour
{
    [SerializeField]
    private UIServerList serverListUI;

    public void host()
    {
        PhotonNetwork.CreateRoom("Boite");
        serverListUI.setActive(false);
    }

    public void join()
    {
        serverListUI.join();
        serverListUI.setActive(false);
    }
}
