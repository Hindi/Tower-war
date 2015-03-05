using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FriendList : MonoBehaviour {

    [SerializeField]
    private Database database;
    [SerializeField]
    private UIFriendList uiFriendList;

    private List<Friend> friends;

    void Start()
    {
        friends = new List<Friend>();
    }

    public void loadFriendList(int playerId)
    {
        database.listFriendship(playerId, listFriendShipSuccessCallback);
    }

    public void listFriendShipSuccessCallback(string answ)
    {
        string[] ids = answ.Split(',');
        foreach (string s in ids) 
        {
            try
            {
                int id = int.Parse(s);
                friends.Add(new Friend(id));
                uiFriendList.addFriend(id);
            }
            catch { continue; }
        }
    }

}
