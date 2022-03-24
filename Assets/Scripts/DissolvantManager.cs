using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DissolvantManager
{
	static int lensCount = 0;
	const int lensMaxCount = 10;

	static Dictionary<string, int> indexDictionary = new Dictionary<string, int>();
	static Matrix4x4[] transformsArray = new Matrix4x4[lensMaxCount];
	static Vector4[] positionsArray = new Vector4[lensMaxCount];
	static float[] radiusArray = new float[lensMaxCount];
	static float[] lengthArray = new float[lensMaxCount];

	public static void SetLens(string id, Vector3 position, float radius, float length, Matrix4x4 transform)
	{
		if (indexDictionary.ContainsKey(id))
		{
			transformsArray[indexDictionary[id]] = transform;
			positionsArray[indexDictionary[id]] = position;
			radiusArray[indexDictionary[id]] = radius;
			lengthArray[indexDictionary[id]] = length;
		}
		else if (lensCount < lensMaxCount)
		{
			indexDictionary.Add(id, lensCount);
			transformsArray[indexDictionary[id]] = transform;
			positionsArray[indexDictionary[id]] = position;
			radiusArray[indexDictionary[id]] = radius;
			lengthArray[indexDictionary[id]] = length;
			lensCount++;
		}
	}


	public static void RenderLenses()
	{
		Shader.SetGlobalInt("_LensCount", lensCount);

		Shader.SetGlobalVectorArray("_GLOBALMaskPosition", positionsArray);
		Shader.SetGlobalFloatArray("_GLOBALMaskRadius", radiusArray);
		Shader.SetGlobalFloatArray("_GLOBALMaskLength", lengthArray);
		Shader.SetGlobalMatrixArray("_GLOBALMaskTransform", transformsArray);

		//Debug.Log(lensCount);

		indexDictionary.Clear();

		lensCount = 0;
	}


}
