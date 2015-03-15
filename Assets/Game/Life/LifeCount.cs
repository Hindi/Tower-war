using UnityEngine;
using System.Collections;

public class LifeCount : MonoBehaviour {

    private Counter counter;

    [SerializeField]
    private UILife uiLife;

    PhotonView photonView;

	// Use this for initialization
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        counter = GetComponent<Counter>();
        counter.EndCountingCallback = lost;
        updateUI(counter.CurrentAmount);
        EventManager.AddListener(EnumEvent.REACHEDBASE, reachedBaseListener);
	}

    public void reachedBaseListener()
    {
        counter.decreaseAmount();
        updateUI(counter.CurrentAmount);
    }

    public void lost()
    {
        EventManager.Raise(EnumEvent.GAMELOST);
    }

    private void updateUI(int value)
    {
        uiLife.updateMyLifeLabel(counter.CurrentAmount);
        photonView.RPC("updateLifeLabelRPC", PhotonTargets.Others, value);
    }

    [RPC]
    public void updateLifeLabelRPC(int value)
    {
        uiLife.updateEnemyLifeLabel(counter.CurrentAmount);
    }
}
