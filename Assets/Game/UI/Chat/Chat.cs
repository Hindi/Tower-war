using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum chatTextType
{
    chat,
    warning,
    error
}

public class Chat : MonoBehaviour
{
    [SerializeField]
    private RectTransform messageContainer;
    [SerializeField]
    private GameObject messagePrefab;
    [SerializeField]
    private Color warningColor;
    [SerializeField]
    private Color chatColor;
    [SerializeField]
    private Color errorColor;
    [SerializeField]
    private InputField chatInput;

    [SerializeField]
    private int maxMessageCount;

    private Queue<GameObject> messageQueue;

	// Use this for initialization
    void Start()
    {
        messageQueue = new Queue<GameObject>();
        sendMessage(chatTextType.chat, "pouet");
        sendMessage(chatTextType.warning, "pouet");
        sendMessage(chatTextType.error, "pouet");
	}

    string ColorToHex(Color32 color)
    {
        string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2") + color.a.ToString("X2");
        return hex;
    }
	
    private string setRichtext(chatTextType e, string text)
    {
        string colorcode = "";
        switch(e)
        {
            case chatTextType.chat:
                colorcode = ColorToHex(chatColor);
                break;
            case chatTextType.warning:
                colorcode = ColorToHex(warningColor);
                break;
            case chatTextType.error:
                colorcode =  ColorToHex(errorColor);
                break;
            default:
                colorcode = ColorToHex(chatColor);
                break;
        }
        return "<color=#" + colorcode + ">" + text + "</color>";
    }

    private void enqueue(GameObject obj)
    {
        messageQueue.Enqueue(obj);
        if (messageContainer.childCount > maxMessageCount)
            Destroy(messageQueue.Dequeue());
    }

    public void sendMessage(chatTextType e, string text)
    {
        text = setRichtext(e, text);
        GameObject obj = GameObject.Instantiate(messagePrefab);
        obj.GetComponent<Text>().text = text;
        obj.transform.SetParent(messageContainer);
        obj.transform.SetAsLastSibling();
        enqueue(obj);
    }

    public void sendPlayerMessage()
    {
        //TODO Filter
        //TODO Add network
        if (chatInput.text != "")
        {
            sendMessage(chatTextType.chat, "You : " + chatInput.text);
            chatInput.text = "";
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && chatInput.isFocused)
            sendPlayerMessage();
    }
}
