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
		//生成初始方块
		SpwanInitSquare();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			MoveToUp();
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			MoveToDown();
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			MoveToLeft();
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			MoveToRight();
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
		for (int i = 0; i < 3; i++)
		{
			_ctrl.SpwanSquare();
		}
	}


	#endregion

	#region 方块移动
	/// <summary>
	/// 向上移动
	/// </summary>
	private void MoveToUp()
	{
		for (int j = 0; j < 4; j++)
		{
			for (int i = 0; i < 4; i++)
			{
				if (_model.SquareValue[i, j] == 0)
				{
					for (int k = i + 1; k < 4; k++)
					{
						if (_model.SquareValue[k, j] != 0)
						{
							_model.SquareValue[i, j] = _model.SquareValue[k, j];
							_model.SquareValue[k, j] = 0;
							//TODO 修改位置,将方块移动到该位置
							//Pk = Pi；并且移动位置
							break;
						}      
					}
				}else if (_model.SquareValue[i, j] != 0)
				{
					for (int k = i + 1; k < 4; k++)
					{
						if (_model.SquareValue[k, j] != 0)
						{
							if (_model.SquareValue[i, j] == _model.SquareValue[k, j]) //两个方块的值相等
							{
								_model.SquareValue[i, j] = 2 * _model.SquareValue[i, j];
								_model.SquareValue[k, j] = 0;
								//TODO
								break;
							}else if (_model.SquareValue[i, j] != _model.SquareValue[k, j])//两个方块的值不相等
							{
								//先将value[k,j] 的值赋给一个中间值
								//再将value[k,j] 的值设为0（即将移动）
								//再将value[i+1，j] 的值替换成中间值
								//这样可以避免在k == i+1 时将值清零的情况
								int medianValue = _model.SquareValue[k, j];
								_model.SquareValue[k, j] = 0;
								_model.SquareValue[i + 1, j] = medianValue;
								//TODO
								break;
							}
						}
					}
				}
			}
		}

		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				if (_model.SquareValue[i,j] == 0)
				{
					continue;
				}

				Debug.Log("("+ i + "," + j+"): " + _model.SquareValue[i,j]);
			}
		}
		
	}

	private void MoveToDown()
	{
		for (int j = 0; j < 4; j++)
		{
			for (int i = 3; i >= 0; i--)
			{
				if (_model.SquareValue[i, j] == 0)
				{
					for (int k = i - 1; k >= 0; k--)
					{
						if (_model.SquareValue[k, j] != 0)
						{
							_model.SquareValue[i, j] = _model.SquareValue[k, j];
							_model.SquareValue[k, j] = 0;
							//TODO 修改位置,将方块移动到该位置
							//Pk = Pi；并且移动位置
							break;
						}      
					}
				}else if (_model.SquareValue[i, j] != 0)
				{
					for (int k = i - 1; k >= 0; k--)
					{
						if (_model.SquareValue[k, j] != 0)
						{
							if (_model.SquareValue[i, j] == _model.SquareValue[k, j]) //两个方块的值相等
							{
								_model.SquareValue[i, j] = 2 * _model.SquareValue[i, j];
								_model.SquareValue[k, j] = 0;
								//TODO
								break;
							}else if (_model.SquareValue[i, j] != _model.SquareValue[k, j])//两个方块的值不相等
							{
								//先将value[k,j] 的值赋给一个中间值
								//再将value[k,j] 的值设为0（即将移动）
								//再将value[i+1，j] 的值替换成中间值
								//这样可以避免在k == i+1 时将值清零的情况
								int medianValue = _model.SquareValue[k, j];
								_model.SquareValue[k, j] = 0;
								_model.SquareValue[i - 1, j] = medianValue;
								//TODO
								break;
							}
						}
					}
				}
			}
		}

		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				if (_model.SquareValue[i,j] == 0)
				{
					continue;
				}
				Debug.Log("("+ i + "," + j+"): " + _model.SquareValue[i,j]);
			}
		}
	}

	private void MoveToLeft()
	{
		for (int j = 0; j < 4; j++)
		{
			for (int i = 0; i < 4; i++)
			{
				if (_model.SquareValue[i, j] == 0)
				{
					for (int k = j + 1; k < 4; k++)
					{
						if (_model.SquareValue[i, k] != 0)
						{
							_model.SquareValue[i, j] = _model.SquareValue[i, k];
							_model.SquareValue[i, k] = 0;
							//TODO 修改位置,将方块移动到该位置
							//Pk = Pi；并且移动位置
							break;
						}      
					}
				}
				
				if (_model.SquareValue[i, j] != 0)
				{
					for (int k = j + 1; k < 4; k++)
					{
						if (_model.SquareValue[k, j] != 0)
						{
							Debug.Log("两个方块相等");
							if (_model.SquareValue[i, j] == _model.SquareValue[i, k]) //两个方块的值相等
							{
								_model.SquareValue[i, j] = 2 * _model.SquareValue[i, j];
								_model.SquareValue[i, k] = 0;
								//TODO
								break;
							}else if (_model.SquareValue[i, j] != _model.SquareValue[i, k])//两个方块的值不相等
							{
								//先将value[k,j] 的值赋给一个中间值
								//再将value[k,j] 的值设为0（即将移动）
								//再将value[i+1，j] 的值替换成中间值
								//这样可以避免在k == i+1 时将值清零的情况
								int medianValue = _model.SquareValue[i, k];
								_model.SquareValue[i, k] = 0;
								_model.SquareValue[i, j + 1] = medianValue;
								//TODO
								break;
							}
						}
						else
						{
							
						}
					}
				}
			}
		}
		
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				if (_model.SquareValue[i,j] == 0)
				{
					continue;
				}
				Debug.Log("("+ i + "," + j+"): " + _model.SquareValue[i,j]);
			}
		}
	}

	private void MoveToRight()
	{
		for (int i = 0; i < 4; i++)
		{
			for (int j = 3; j >= 0; j--)
			{
				if (_model.SquareValue[i, j] == 0)
				{
					for (int k = j - 1; k >= 0; k--)
					{
						if (_model.SquareValue[i, k] != 0)
						{
							_model.SquareValue[i, j] = _model.SquareValue[i, k];
							_model.SquareValue[i, k] = 0;
							//TODO 修改位置,将方块移动到该位置
							//Pk = Pi；并且移动位置
							break;
						}      
					}
				}
				
				if (_model.SquareValue[i, j] != 0)
				{
					for (int k = j - 1; k >= 0; k--)
					{
						if (_model.SquareValue[k, j] != 0)
						{
							if (_model.SquareValue[i, j] == _model.SquareValue[i, k]) //两个方块的值相等
							{
								_model.SquareValue[i, j] = 2 * _model.SquareValue[i, j];
								_model.SquareValue[i, k] = 0;
								//TODO
								break;
							}else if (_model.SquareValue[i, j] != _model.SquareValue[i, k])//两个方块的值不相等
							{
								//先将value[k,j] 的值赋给一个中间值
								//再将value[k,j] 的值设为0（即将移动）
								//再将value[i+1，j] 的值替换成中间值
								//这样可以避免在k == i+1 时将值清零的情况
								int medianValue = _model.SquareValue[i, k];
								_model.SquareValue[i, k] = 0;
								_model.SquareValue[i, j - 1] = medianValue;
								//TODO
								break;
							}
						}
					}
				}
			}
		}
		
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				if (_model.SquareValue[i,j] == 0)
				{
					continue;
				}
				Debug.Log("("+ i + "," + j+"): " + _model.SquareValue[i,j]);
			}
		}
	}

	#endregion
}
