using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUpdatedListener : MonoBehaviour
{
    [SerializeField]
    private Text _textAmmo = null;

	void Start ()
    {
        DataManager.Instance.Event_AmmoUpdated += onAmmoUpdated; // listen to event
        onAmmoUpdated(); // one time update
	}
	
    void OnDisable()
    {
        DataManager.Instance.Event_AmmoUpdated -= onAmmoUpdated; // stop listening
    }

    void Update ()
    {
		
	}

    private void onAmmoUpdated()
    {
        _textAmmo.text = DataManager.Instance.Ammo.ToString();
    }
}
