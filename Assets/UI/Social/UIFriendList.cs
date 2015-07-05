using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIFriendList : UIElement
{
    [SerializeField]
    private GameObject friendInfoPrefab;

    [SerializeField]
    private Transform contentPanel;


    public void addFriend(int id)
    {
        UIFriendInfo obj= ((GameObject)Instantiate(friendInfoPrefab)).GetComponent<UIFriendInfo>();
        obj.Name = id.ToString();
        obj.transform.SetParent(contentPanel);
    }
}
