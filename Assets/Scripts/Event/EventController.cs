﻿/*
 * Author(s): Isaiah Mann 
 * Description: A single event class that others can subscribe to and call events from
 * Currently relies on event names as strings
 * Event method can be overloaded to implement different event types
 */

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EventController : SingletonController<EventController> 
{
	#region Event Types

	public delegate void NamedEventAction(string nameOfEvent);
	public event NamedEventAction OnNamedEvent;

	public delegate void NamedFloatAction(string valueKey, float key);
	public event NamedFloatAction OnNamedFloatEvent;

	public delegate void AudioEventAction(AudioActionType actionType, AudioType audioType);
	public event AudioEventAction OnAudioEvent;

	public delegate void PPEventAction(LTREvent gameEvent);
	public event PPEventAction OnPPEvent;

	#endregion

	[SerializeField]
	bool alsoCallPPEventsAsStrings;
	[SerializeField]
	bool tryAlsoCallStringsAsPPEvents;

	#region Event Subscription

	public static void Subscribe(NamedEventAction eventAction) 
	{
		if(hasInstance) 
		{
			Instance.OnNamedEvent += eventAction;
		}
	}

	public static void Subscribe(NamedFloatAction eventAction) 
	{
		if(hasInstance) 
		{
			Instance.OnNamedFloatEvent += eventAction;
		}
	}

	public static void Subscribe(AudioEventAction eventAction) 
	{
		if(hasInstance) 
		{
			Instance.OnAudioEvent += eventAction;
		}
	}

	public static void Subscribe(PPEventAction eventAction) 
	{
		if(hasInstance) 
		{
			Instance.OnPPEvent += eventAction;
		}
	}

	public static void Unsubscribe(NamedEventAction eventAction) 
	{
		if(hasInstance) 
		{
			Instance.OnNamedEvent -= eventAction;
		}
	}

	public static void Unsubscribe(NamedFloatAction eventAction) 
	{
		if(hasInstance) 
		{
			Instance.OnNamedFloatEvent -= eventAction;
		}
	}

	public static void Unsubscribe(AudioEventAction eventAction) 
	{
		if(hasInstance) 
		{
			Instance.OnAudioEvent -= eventAction;
		}
	}

	public static void Unsubscribe(PPEventAction eventAction) 
	{
		if(hasInstance)
		{
			Instance.OnPPEvent -= eventAction;
		}
	}

	#endregion

	#region Event Calls

	public static void Event(string eventName, bool isCallback = false) 
	{
		if(hasInstance) 
		{
			if(Instance.OnNamedEvent != null) 
			{
				Instance.OnNamedEvent(eventName);
			}
			if(Instance.tryAlsoCallStringsAsPPEvents && !isCallback) 
			{
				tryCallStringAsPPEvent(eventName);
			}
		}
	}
		

	public static void Event(string valueKey, float value) 
	{
		if(hasInstance && Instance.OnNamedFloatEvent != null) 
		{
			Instance.OnNamedFloatEvent(valueKey, value);
		}
	}

    public static void Event(AudioActionType actionType, AudioType audioType) 
	{
		if(hasInstance && Instance.OnAudioEvent != null) 
		{
			Instance.OnAudioEvent(actionType, audioType);
        }
    }

	public static void Event(LTREvent gameEvent, bool isCallback = false) 
	{
		if(hasInstance) 
		{
			if(Instance.OnPPEvent != null) 
			{	
				Instance.OnPPEvent(gameEvent);
			}
			if(Instance.alsoCallPPEventsAsStrings && !isCallback) 
			{
				callPPEventAsString(gameEvent);
			}
		}
	}

	static void callPPEventAsString(LTREvent gameEvent) 
	{
		Event(gameEvent.ToString(), isCallback:true);
	}

	static bool tryCallStringAsPPEvent(string eventName) 
	{
		try
		{
			Event((LTREvent) Enum.Parse(typeof(LTREvent), eventName), isCallback:true);
			return true;
		}
		catch 
		{
			// Cannot parse enum:
			return false;
		}
	}

	#endregion

}

// Unique Pickup Pup Event Enum
public enum LTREvent 
{
	Play,
	Pause,
	Quit,
	LoadStart,
	LoadHome,
  	LoadShop,
	ScoutingDogLoaded,
  	LoadLivingRoom,
  	LoadShelter,
	ClickDogSlot,
	StartedScouting,
    LoadYard,

}
