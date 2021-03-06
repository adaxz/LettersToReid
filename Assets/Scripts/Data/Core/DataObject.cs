﻿/*
 * Author: Isaiah Mann
 * Description: Abstract class for storing data in Pickup Pup
 */

using System;
using System.Globalization;
using UnityEngine;
using k = Global;

[Serializable]
public abstract class DataObject 
{
	protected const string BLACK_HEX = k.BLACK_HEX;
	protected const int NONE_INT = k.NONE_VALUE;
	protected const int NOT_FOUND_INT = k.INVALID_VALUE;

	const string HEX_HASH_PREFIX = k.HEX_HASH_PREFIX;
	const string HEX_NUM_PREFIX = k.HEX_NUM_PREFIX;
	const int CORRECT_HEX_NUM_LENGTH = k.CORRECT_HEX_NUM_LENGTH;

	public delegate void DataAction();
	public delegate void DataActionf(float value);

  // Adapated from http://www.bugstacker.com/15/how-to-parse-a-hex-color-string-in-unity-c%23
	protected Color parseHexColor(string hexstring) 
	{
		if(hexstring.StartsWith(HEX_HASH_PREFIX)) 
		{
			hexstring = hexstring.Substring(HEX_HASH_PREFIX.Length);
		}
		else if(hexstring.StartsWith(HEX_NUM_PREFIX)) 
		{
			hexstring = hexstring.Substring(HEX_NUM_PREFIX.Length);
		}

		if(hexstring.Length == CORRECT_HEX_NUM_LENGTH)
		{
			byte r = byte.Parse(hexstring.Substring(0, 2), NumberStyles.HexNumber);
			byte g = byte.Parse(hexstring.Substring(2, 2), NumberStyles.HexNumber);
			byte b = byte.Parse(hexstring.Substring(4, 2), NumberStyles.HexNumber);
			return new Color32(r, g, b, 1);
		}
		else 
		{
			throw new Exception(string.Format("{0} is not a valid color string.", hexstring));
		}
	}
		
	protected string padWithZeroes(int number, int desiredLength) 
	{
		string numberAsString = number.ToString();
		int numberLength = numberAsString.Length;
		if(numberLength < desiredLength) 
		{
			return numberAsString.PadLeft(desiredLength, '0');
		} 
		else 
		{
			return numberAsString;
		}
	}

	protected string formatCost(int cost) 
	{
		// String formatter to concat integer with dollar sign:
		return string.Format("${0}", cost);	
	}

	protected string formatTime(float time)
	{
		int hours = (int) time / 3600;
		int minutes = ((int) time / 60) % 60;
		int seconds = (int) time % 60;
		return string.Format("{0}:{1}:{2}", 
			padWithZeroes(hours, 2), 
			padWithZeroes(minutes, 2),
			padWithZeroes(seconds, 2)
		);

	}

}
