using UnityEngine;
using TsiU;

public class UnityLogListener : TILoggerListener
{
	public void log (string msg)
	{
		Debug.Log(msg);
	}
};

public class UnityAITookit
{
	static public void Init() 
	{
		TAIToolkit.Init();
		TLogger.instance.AddLogListener(new UnityLogListener());
		TLogger.INFO("Unity_AITookit initialization");
	}
	static public void Uninit()
	{ 
        TLogger.INFO("Unity_AITookit uninitialization");
		TAIToolkit.Uninit();
	}
};
