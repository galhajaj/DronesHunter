using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldUpdatedListener : MonoBehaviour
{
    [SerializeField]
    private Text _textGold = null;

    void Start()
    {
        DataManager.Instance.Event_GoldUpdated += onGoldUpdated; // listen to event
        onGoldUpdated(); // one time update
    }

    void OnDisable()
    {
        DataManager.Instance.Event_AmmoUpdated -= onGoldUpdated; // stop listening
    }

    void Update()
    {

    }

    private void onGoldUpdated()
    {
        _textGold.text = DataManager.Instance.Gold.ToString();
    }
}