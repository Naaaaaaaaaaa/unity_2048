/**====================================
 *Copyright(C) 2018 by Wipace 
 *All rights reserved. 
 *FileName:     .cs 
 *Author:       CGzhao 
 *Version:      1.0 
 *UnityVersion：2018.2.3 
 *Date:         2018-12-15 
 *Email:		1341674064@qq.com
 *Description:    
 *History: 
======================================**/

using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class View : MonoBehaviour
{
	public static View Instance;

	public Ctrl _Ctrl;

	public Transform Root { get; set; }
	public Transform parent { get; set; }

	private void Awake()
	{
		Instance = this;
		Root = GameObject.Find("Root").GetComponent<Transform>();
		parent = GameObject.Find("parent").GetComponent<Transform>();
	}

	/// <summary>
	/// 获取Root所有子节点的坐标
	/// </summary>
	void GetAllRootChildren()
	{
		_Ctrl = Ctrl.Instace;
		_Ctrl.GetAllRootChildren(Root);
	}
}
