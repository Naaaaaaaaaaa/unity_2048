/**====================================
 *Copyright(C) 2018 by Wipace 
 *All rights reserved. 
 *FileName:     .cs 
 *Author:       CGzhao 
 *Version:      1.0 
 *UnityVersion：2018.2.3 
 *Date:         2018-11-27 
 *Email:		1341674064@qq.com
 *Description:    
 *History: 
======================================**/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Model : MonoBehaviour
{
	public static Model Instance;

	public Ctrl _Ctrl;

	//存储预制体的路径
	public string Path_Num2 = "prefabs/Num_2";
	public string Path_Num4 = "prefabs/Num_4";
	public string Path_Num8 = "prefabs/Num_8";
	public string Path_Num16 = "prefabs/Num_16";
	public string Path_Num32 = "prefabs/Num_32";
	public string Path_Num64 = "prefabs/Num_64";
	public string Path_Num128 = "prefabs/Num_128";
	public string Path_Num256 = "prefabs/Num_256";
	public string Path_Num512 = "prefabs/Num_512";
	public string Path_Num1024 = "prefabs/Num_1024";
	public string Path_Num2048 = "prefabs/Num_2048";
	
	/// <summary>
	/// 存储标准的方块坐标数据
	/// </summary>
	public Transform[,] StandardPos = new Transform[4,4];
	/// <summary>
	/// 存储方块信息数据：坐标， 值
	/// </summary>
	public SquareObj[,] SquareInfo = new SquareObj[4,4];

	private void Awake()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}

}
