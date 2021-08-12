using UnityEngine;
using System.Collections;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
	private static T _instance = null;
	public static T instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType(typeof(T)) as T;

				if(_instance == null)
				{
					print (typeof(T).ToString() + " intance is null");
				}
				else
				{
				}

			}
			return _instance;
		}

		set
		{
			_instance = value;
		}
	}

	private void Awake()
	{
		if(_instance == null)
		{
			_instance = this as T;
		}
		
	}


}
