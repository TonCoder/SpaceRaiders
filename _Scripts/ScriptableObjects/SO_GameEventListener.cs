using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName="Events/EventListener", fileName="new EventListener")]
public class SO_GameEventListener : ScriptableObject
{
	public SO_GameEvents _event;
    public UnityEvent _response;

    private void OnEnabled(){ _event.RegisterListener(this);}
    private void OnDisabled(){ _event.UnregisterListener(this);}

    public void OnEventRaised(){
        _response.Invoke();
    }
}
