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
using System.Runtime.Remoting;
using TMPro;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
	private View _view;
	private Ctrl _ctrl;
	private Model _model;
	// Use this for initialization
	void Start () {
		_ctrl = Ctrl.Instace;
		_model = Model.Instance;
		_view = View.Instance;
		
		//获取并存储标准坐标
		GetAllRootChildren();

		for (int l = 0; l < 3; l++)
		{
			//生成初始方块
			SpwanInitSquare();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			MoveToUp();
			SpwanInitSquare();
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			MoveToDown();
			SpwanInitSquare();
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			MoveToLeft();
			SpwanInitSquare();
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			MoveToRight();
			SpwanInitSquare();
		}
	}

	#region 初始化游戏
	/// <summary>
	/// 获取Root节点下所有子节点的坐标
	/// 这些坐标是方块的标准坐标
	/// </summary>
	void GetAllRootChildren()
	{
		_ctrl.GetAllRootChildren(_view.Root);
	}

	/// <summary>
	/// 生成三个方块
	/// </summary>
	void SpwanInitSquare()
	{
			_ctrl.SpwanSquare();
	}


	#endregion

	#region 方块移动
	/// <summary>
	/// 向左移动
	/// </summary>
	private void MoveToLeft()
	{
		for (int y = 0; y < 4; y++)
		{
			bool isAdd = false;
			for (int x = 0; x < 4; x++)
			{
				if (x == 0 && _model.SquareInfo[x, y].Value == 0)
				{
					Debug.Log("左滑第一个为0");
					for (int k = x + 1; k < 4; k++)
					{
						if (_model.SquareInfo[k, y].Value == 0) continue;
						_model.SquareInfo[x, y].Value = _model.SquareInfo[k, y].Value;
						_model.SquareInfo[k, y].Value = 0;
						//TODO 修改位置,将方块移动到该位置
						_model.SquareInfo[x, y].UItranTransform = _model.SquareInfo[k, y].UItranTransform;
						_model.SquareInfo[k, y].UItranTransform = null;
						_model.SquareInfo[x, y].UItranTransform.localPosition = _model.SquareInfo[x, y].standardPos.localPosition;
						//Pk = Pi；并且移动位置
						break;
					}
				}

				if (_model.SquareInfo[x, y].Value == 0) continue;
			    
				for (int k = x + 1; k < 4; k++)
				{
					if (_model.SquareInfo[k, y].Value == 0) continue;
					if (_model.SquareInfo[x, y].Value == _model.SquareInfo[k, y].Value && !isAdd) //两个方块的值相等
					{
						_model.SquareInfo[x, y].Value = 2 * _model.SquareInfo[x, y].Value;
						_model.SquareInfo[k, y].Value = 0;
						isAdd = true;
						//TODO 移动并赋值
						_model.SquareInfo[x, y].UItranTransform = _model.SquareInfo[x, y].UItranTransform;
						_model.SquareInfo[k, y].DestroySquare();
						_model.SquareInfo[x, y].UItranTransform.GetComponentInChildren<TMP_Text>().text =
							"" + _model.SquareInfo[x, y].Value;
					}
					else
					{
						//先将value[k,j] 的值赋给一个中间值
						//再将value[k,j] 的值设为0（即将移动）
						//再将value[i+1，j] 的值替换成中间值
						//这样可以避免在k == i+1 时将值清零的情况
//						SquareObj medianObj = new SquareObj(_model.SquareInfo[k, y].transform, _model.SquareInfo[k, y].Value, _model.SquareInfo[k, y].standardPos);
//						_model.SquareInfo[k, y].Value = 0;
//						_model.SquareInfo[k, y].transform = null;
						if (k == x + 1) continue;
						_model.SquareInfo[x + 1, y].Value = _model.SquareInfo[k, y].Value;
						_model.SquareInfo[x + 1, y].UItranTransform = _model.SquareInfo[k, y].UItranTransform;
						_model.SquareInfo[x + 1, y].UItranTransform.localPosition = _model.SquareInfo[x + 1, y].standardPos.localPosition;

						_model.SquareInfo[k, y].Value = 0;
						_model.SquareInfo[k, y].UItranTransform = null;
						break;
					}
				}
			    
			}
		}

		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				if (_model.SquareInfo[i,j].Value == 0)
				{
					continue;
				}

				Debug.Log("("+ i + "," + j+"): " + _model.SquareInfo[i,j].Value);
			}
		}
		
	}


	private void MoveToRight()
	{
		for (int y = 0; y < 4; y++)
		{
			bool isAdd = false;
			for (int x = 3; x >= 0; x--)
			{
				if (x == 3 && _model.SquareInfo[x, y].Value == 0)
				{
					Debug.Log("右滑第一个为0");
					for (int k = x - 1; k >= 0; k--)
					{
						if (_model.SquareInfo[k, y].Value == 0) continue;
						_model.SquareInfo[x, y].Value = _model.SquareInfo[k, y].Value;
						_model.SquareInfo[k, y].Value = 0;
						//TODO 修改位置,将方块移动到该位置
						_model.SquareInfo[x, y].UItranTransform = _model.SquareInfo[k, y].UItranTransform;
						_model.SquareInfo[k, y].UItranTransform = null;
						_model.SquareInfo[x, y].UItranTransform.localPosition = _model.SquareInfo[x, y].standardPos.localPosition;
						break;
					}
				}
				
				if (_model.SquareInfo[x, y].Value == 0) continue;
				
				for (int k = x - 1; k >= 0; k--)
				{
					if (_model.SquareInfo[k, y].Value == 0) continue;
					if (_model.SquareInfo[x, y].Value == _model.SquareInfo[k, y].Value && !isAdd) //两个方块的值相等
					{
						_model.SquareInfo[x, y].Value = 2 * _model.SquareInfo[x, y].Value;
						_model.SquareInfo[k, y].Value = 0;
						isAdd = true;
						//TODO
						_model.SquareInfo[x, y].UItranTransform = _model.SquareInfo[x, y].UItranTransform;
						_model.SquareInfo[k, y].DestroySquare();
						_model.SquareInfo[x, y].UItranTransform.GetComponentInChildren<TMP_Text>().text =
							"" + _model.SquareInfo[x, y].Value;
					}
					else
					{
						//先将value[k,j] 的值赋给一个中间值
						//再将value[k,j] 的值设为0（即将移动）
						//再将value[i+1，j] 的值替换成中间值
						//这样可以避免在k == i+1 时将值清零的情况
//						SquareObj medianObj = new SquareObj(_model.SquareInfo[k, y].transform, _model.SquareInfo[k, y].Value, _model.SquareInfo[k, y].standardPos);
						if (k == x - 1) continue;
						
						_model.SquareInfo[x - 1, y].Value = _model.SquareInfo[k, y].Value;
						_model.SquareInfo[x - 1, y].UItranTransform = _model.SquareInfo[k, y].UItranTransform;
						_model.SquareInfo[x - 1, y].UItranTransform.localPosition = _model.SquareInfo[x - 1, y].standardPos.localPosition;
						
						_model.SquareInfo[k, y].Value = 0;
						_model.SquareInfo[k, y].UItranTransform = null;
						break;
					}
				}
			}
		}

		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				if (_model.SquareInfo[i,j].Value == 0)
				{
					continue;
				}
				Debug.Log("("+ i + "," + j+"): " + _model.SquareInfo[i,j].Value);
			}
		}
	}

	private void MoveToUp()
	{
		for (int x = 0; x < 4; x++)
		{
			bool isAdd = false;
			for (int y = 0; y < 4; y++)
			{ 
				if (y == 0 && _model.SquareInfo[x, y].Value == 0)
				{
					Debug.Log("上划滑第一个为0");
					for (int k = y + 1; k < 4; k++)
					{
						if (_model.SquareInfo[x, k].Value == 0) continue;
						_model.SquareInfo[x, y].UItranTransform = _model.SquareInfo[x, k].UItranTransform;
						_model.SquareInfo[x, y].Value = _model.SquareInfo[x, k].Value;
						//TODO 修改位置,将方块移动到该位置
						//Pk = Pi；并且移动位置
						_model.SquareInfo[x, k].UItranTransform = null;
						_model.SquareInfo[x, k].Value = 0;
						_model.SquareInfo[x, y].UItranTransform.localPosition = _model.SquareInfo[x, y].standardPos.localPosition;
						break;
					}
				}
				
				if (_model.SquareInfo[x, y].Value == 0) continue;
				
				for (int k = y + 1; k < 4; k++)
				{
					if (_model.SquareInfo[x, k].Value == 0) continue;
					Debug.Log("两个方块相等");
					if (_model.SquareInfo[x, y].Value == _model.SquareInfo[x, k].Value && !isAdd) //两个方块的值相等
					{
						_model.SquareInfo[x, y].Value = 2 * _model.SquareInfo[x, y].Value;
						_model.SquareInfo[x, k].Value = 0;
						isAdd = true;
						//TODO
						_model.SquareInfo[x, y].UItranTransform = _model.SquareInfo[x, y].UItranTransform;
						_model.SquareInfo[x, k].DestroySquare();
						_model.SquareInfo[x, y].UItranTransform.GetComponentInChildren<TMP_Text>().text =
							"" + _model.SquareInfo[x, y].Value;
					}
					else
					{
						//先将value[k,j] 的值赋给一个中间值
						//再将value[k,j] 的值设为0（即将移动）
						//再将value[i+1，j] 的值替换成中间值
						//这样可以避免在k == i+1 时将值清零的情况
//						SquareObj medianObj = new SquareObj(_model.SquareInfo[x, k].transform, _model.SquareInfo[x, k].Value, _model.SquareInfo[x, k].standardPos);
						if (y + 1 == k)	continue;
						_model.SquareInfo[x, y + 1].Value = _model.SquareInfo[x, k].Value;
						_model.SquareInfo[x, y + 1].UItranTransform = _model.SquareInfo[x, k].UItranTransform;
						_model.SquareInfo[x, y + 1].UItranTransform.localPosition = _model.SquareInfo[x, y + 1].standardPos.localPosition;
						
						_model.SquareInfo[x, k].Value = 0;
						_model.SquareInfo[x, k].UItranTransform = null;
						break;
					}
				}	
			}
		}
		
		for (int x = 0; x < 4; x++)
		{
			for (int y = 0; y < 4; y++)
			{
				if (_model.SquareInfo[x,y].Value == 0)
				{
					continue;
				}
				Debug.Log("("+ x + "," + y+"): " + _model.SquareInfo[x,y].Value);
			}
		}
	}

	private void MoveToDown()
	{
		for (int x = 0; x < 4; x++)
		{
			bool isAdd = false;
			for (int y = 3; y >= 0; y--)
			{
				if (y == 3 && _model.SquareInfo[x, y].Value == 0)
				{
					Debug.Log("下划滑第一个为0");
					for (int k = y - 1; k >= 0; k--)
					{
						if (_model.SquareInfo[x, k].Value == 0) continue;
						_model.SquareInfo[x, y].UItranTransform = _model.SquareInfo[x, k].UItranTransform;
						_model.SquareInfo[x, y].Value = _model.SquareInfo[x, k].Value;
						//TODO 修改位置,将方块移动到该位置
						//Pk = Pi；并且移动位置
						_model.SquareInfo[x, k].UItranTransform = null;
						_model.SquareInfo[x, k].Value = 0;
						_model.SquareInfo[x, y].UItranTransform.localPosition = _model.SquareInfo[x, y].standardPos.localPosition;
						break;      
					}
				}
				
				if (_model.SquareInfo[x, y].Value == 0) continue;
				for (int k = y - 1; k >= 0; k--)
				{
					if (_model.SquareInfo[x, k].Value == 0) continue;
					if (_model.SquareInfo[x, y].Value == _model.SquareInfo[x, k].Value && !isAdd) //两个方块的值相等
					{
						_model.SquareInfo[x, y].Value = 2 * _model.SquareInfo[x, y].Value;
						_model.SquareInfo[x, k].Value = 0;
						isAdd = true;
						//TODO
						_model.SquareInfo[x, y].UItranTransform = _model.SquareInfo[x, y].UItranTransform;
						_model.SquareInfo[x, k].DestroySquare();
						_model.SquareInfo[x, y].UItranTransform.GetComponentInChildren<TMP_Text>().text =
							"" + _model.SquareInfo[x, y].Value;
					}
					else
					{
						//先将value[k,j] 的值赋给一个中间值
						//再将value[k,j] 的值设为0（即将移动）
						//再将value[i+1，j] 的值替换成中间值
						//这样可以避免在k == i+1 时将值清零的情况
//						SquareObj medianObj = new SquareObj(_model.SquareInfo[x, k].transform, _model.SquareInfo[x, k].Value, _model.SquareInfo[x, k].standardPos);
						if(k == y - 1) continue;
						
						_model.SquareInfo[x, y - 1].Value = _model.SquareInfo[x, k].Value;
						_model.SquareInfo[x, y - 1].UItranTransform = _model.SquareInfo[x, k].UItranTransform;
						_model.SquareInfo[x, y - 1].UItranTransform.localPosition = _model.SquareInfo[x, y - 1].standardPos.localPosition;
						
						_model.SquareInfo[x, k].Value = 0;
						_model.SquareInfo[x, k].UItranTransform = null;
						break;
					}
				}
			}
		}
		
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				if (_model.SquareInfo[i,j].Value == 0)
				{
					continue;
				}
				Debug.Log("("+ i + "," + j+"): " + _model.SquareInfo[i,j].Value);
			}
		}
	}

	#endregion
}
