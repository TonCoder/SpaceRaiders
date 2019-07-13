using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Events/Events", fileName="new GameEventList")]
public class SO_GameEvents : ScriptableObject
{
	public List<SO_GameEventListener> listeners = new List<SO_GameEventListener>();

    public void Raise(){
        for (int i = listeners.Count -1; i >= 0; i--)
        {
           listeners[i].OnEventRaised(); 
        }
    }

    public void RegisterListener(SO_GameEventListener listener){
        listeners.Add(listener);
    }
    public void UnregisterListener(SO_GameEventListener listener){
        listeners.Remove(listener);
    }

}
