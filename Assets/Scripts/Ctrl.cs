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

public class Ctrl : MonoBehaviour
{
	public static Ctrl Instace;

	public Model _Model;
	public View _View;

	private GameObject _gameObject;
	
	private void Awake()
	{
		Instace = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	/// <summary>
	/// 获取View中Root所有的标准坐标数值,并将坐标存储在Model的StandardPos二维数组中
	/// </summary>
	/// <param name="Root">_view中的root</param>
	public void GetAllRootChildren(Transform Root)
	{
		_Model = Model.Instance;
		Transform[] trans = Root.GetComponentsInChildren<Transform>();
		for (int i = 0; i < Root.childCount; i++)
		{
			//取整
			int integer = i / 4;
			//取余
			int remainder = i % 4;
			
//			_Model.StandardPos[remainder, integer] = Root.GetChild(i);
			//SquareObj sObj = new SquareObj(null, 0, Root.GetChild(i));
			Transform tran = trans[i + 1];
			_Model.SquareInfo[remainder, integer] = new SquareObj(null, 0, tran);
		}

//		for (int y = 0; y < 4; y++)
//		{
//			for (int x = 0; x < 4; x++)
//			{
//				Debug.Log(string.Format("（{0},{1}）: {2}", x, y, _Model.StandardPos[x, y]));
//			}
//		}
	}

	/// <summary>
	/// 生成所有方块
	/// </summary>
	public void SpwanAllSquare()
	{
		_View = View.Instance;
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				GameObject obj = Instantiate(Resources.Load<GameObject>(_Model.Path_Num2), Vector3.zero,
					Quaternion.identity, _View.parent);
				obj.transform.localPosition = _Model.StandardPos[i, j].localPosition;
			}
		}
	}

	/// <summary>
	/// 生成一个方块
	/// </summary>
	public void SpwanSquare()
	{
		_View = View.Instance;
		int row = Random.Range(0, 4);
		int col = Random.Range(0, 4);
		if (_Model.SquareInfo[row, col].Value != 0)//如果这个位置已经存在方块
		{
			SpwanSquare();
			return;
		}
		
		//生成预制体
		int random = Random.Range(1, 3);
		if (random == 1)
		{
			_gameObject = Instantiate(Resources.Load<GameObject>(_Model.Path_Num2), _View.parent);			
		}
		else if(random == 2){
			_gameObject = Instantiate(Resources.Load<GameObject>(_Model.Path_Num4), _View.parent);
		}
		_gameObject.transform.localPosition = _Model.SquareInfo[row, col].standardPos.localPosition;
		//存储到对应的二维数组中
		Transform standardTran = _Model.SquareInfo[row, col].standardPos;
		_Model.SquareInfo[row, col] = new SquareObj(_gameObject.transform, GlobalData.GetValue(random), standardTran);
		_gameObject = null;
		Debug.Log(string.Format("生成的物体：{0}，{1}，值：{2}", row, col, GlobalData.GetValue(random)));
	}
}
