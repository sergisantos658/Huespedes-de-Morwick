using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DissolvantManager
{
	static int lensCount = 0;
	const int lensMaxCount = 10;

	static Dictionary<string, int> indexDictionary = new Dictionary<string, int>();
	static Vector4[] directionsArray = new Vector4[lensMaxCount];
	static Vector4[] positionsArray = new Vector4[lensMaxCount];
	static float[] radiusArray = new float[lensMaxCount];
	static float[] lengthArray = new float[lensMaxCount];

	public static void SetLens(string id,Vector3 direction, Vector3 position, float radius, float length)
	{
		if (indexDictionary.ContainsKey(id))
		{
			directionsArray[indexDictionary[id]] = direction;
			positionsArray[indexDictionary[id]] = position;
			radiusArray[indexDictionary[id]] = radius;
			lengthArray[indexDictionary[id]] = length;
		}
		else if (lensCount < lensMaxCount)
		{
			indexDictionary.Add(id, lensCount);
			directionsArray[indexDictionary[id]] = direction;
			positionsArray[indexDictionary[id]] = position;
			radiusArray[indexDictionary[id]] = radius;
			lengthArray[indexDictionary[id]] = length;
			lensCount++;
		}
	}


	public static void RenderLenses()
	{
		Shader.SetGlobalInt("_LensCount", lensCount);

		Shader.SetGlobalVectorArray("_GLOBALMaskDirection", directionsArray);
		Shader.SetGlobalVectorArray("_GLOBALMaskPosition", positionsArray);
		Shader.SetGlobalFloatArray("_GLOBALMaskRadius", radiusArray);
		Shader.SetGlobalFloatArray("_GLOBALMaskLength", lengthArray);

		//Debug.Log(lensCount);

		indexDictionary.Clear();

		lensCount = 0;
	}


}
