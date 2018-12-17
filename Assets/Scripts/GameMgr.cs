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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using TMPro;
using UnityEngine;
using DG.Tweening;

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
		// 列
//         		for (int y = 0; y < 4; y++)
//         		{
//         			// 行
//         			for (int x = 0; x < 4; x++)
//         			{	
//         				// 如果第一为0，则将下一个不为0的数移动到第一个位置
//         				if (/*x == 0 && */_model.SquareInfo[x, y].Value == 0)
//         				{
//         					Debug.Log("左滑第一个为0");
//         					for (int k = x + 1; k < 4; k++)
//         					{
//         						if (_model.SquareInfo[k, y].Value == 0) continue;
//         						_model.SquareInfo[x, y].Value = _model.SquareInfo[k, y].Value;
//         						_model.SquareInfo[k, y].Value = 0;
//         						//TODO修改位置,将方块移动到该位置
//         						_model.SquareInfo[x, y].UITransform = _model.SquareInfo[k, y].UITransform;
//         						_model.SquareInfo[k, y].UITransform = null;
//         //						_model.SquareInfo[x, y].UITransform.localPosition = _model.SquareInfo[x, y].standardPos.localPosition;
//         						_model.SquareInfo[x, y].UITransform
//         							.DOLocalMove(_model.SquareInfo[x, y].standardPos.localPosition, 0.2f);
//         						//Pk = Pi；并且移动位置
//         						break;
//         					}
//         				}
//         				// 如果第一个方格，值为0的情况，不做任何操作
//         //				if (_model.SquareInfo[x, y].Value == 0) break;
//         				
//         				// 如果非第一个放个，值不为0的情况，找出下一个不为0的数
//         				for (int k = x + 1; k < 4; k++)
//         				{
//         					// 为0则继续查找
//         					if (_model.SquareInfo[k, y].Value == 0) continue;
//         					
//         					// 下一个不为0的数，且与该数相等
//         					if (_model.SquareInfo[x, y].Value == _model.SquareInfo[k, y].Value/* && !_model.SquareInfo[x, y].IsJudged*/) //两个方块的值相等
//         					{
//         						Debug.Log("两个方块相等:" + _model.SquareInfo[x, y].Value);
//         						_model.SquareInfo[x, y].Value = 2 * _model.SquareInfo[x, y].Value;
//         						_model.SquareInfo[k, y].Value = 0;
//         						
//         						_model.SquareInfo[x, y].UITransform = _model.SquareInfo[x, y].UITransform;
//         						var y1 = y;
//         						var x2 = x;
//         						var index0 = k;
//         
//         						_model.SquareInfo[k, y].UITransform
//         							.DOLocalMove(_model.SquareInfo[x, y].UITransform.localPosition, 0.2f)
//         							.OnComplete(() =>
//         								{
//         									_model.SquareInfo[index0, y1].DestroySquare();
//         									_model.SquareInfo[x2, y1].UITransform.GetComponentInChildren<TMP_Text>().text =
//         										"" + _model.SquareInfo[x2, y1].Value;
//         								});
//         
//         //						_model.SquareInfo[k, y].DestroySquare();
//         //						_model.SquareInfo[x, y].UITransform.GetComponentInChildren<TMP_Text>().text =
//         //							"" + _model.SquareInfo[x, y].Value;
//         					}
//         					// 下一个不为0的数，且与该数不相等,
//         					else//两个方块不想等 
//         					{
//         						//if (k == x + 1) continue;
//         						//会出现如果第一个与第二不相等且不为0，会continue掉，
//         						//继续寻找下一个不为零的值，继续判断，如果结果还是：、
//         						//第一个与第二个不想等且都不为零，则会将第三个的信息赋值到第二个的位置并重叠。
//         						if (k != x + 1)
//         						{
//         							Debug.Log(string.Format("K 的值： {0}, X + 1的值：{1}", k, x + 1));
//         							Debug.Log("两个方块相不等:" + _model.SquareInfo[x, y].Value + "————" + _model.SquareInfo[k, y].Value);
//         
//         							//找到了离第一个最近的空位置并赋值, 停止这个循环
//         							_model.SquareInfo[x + 1, y].Value = _model.SquareInfo[k, y].Value;
//         							_model.SquareInfo[x + 1, y].UITransform = _model.SquareInfo[k, y].UITransform;
//         							//_model.SquareInfo[x + 1, y].UITransform.localPosition = _model.SquareInfo[x + 1, y].standardPos.localPosition;
//         							_model.SquareInfo[x + 1, y].UITransform
//         								.DOLocalMove(_model.SquareInfo[x + 1, y].standardPos.localPosition, 0.2f);
//         						
//         							_model.SquareInfo[k, y].Value = 0;
//         							_model.SquareInfo[k, y].UITransform = null;
//         						}
//         					}
//         					break;
//         				}
//         			    
//         			}
//         		}

		for (int y = 0; y < 4; y++)
		{
			for (int x = 0; x < 4; x++)
			{
				//取得这一行第一个数，如果该数不为零，则跳过这一步，
				//若为零则一直寻找，直到找到不为零的数,   将这个数移到第一个数的位置  __  再跳过这一步，
				//如果一直没有找到不为零的数，则整个循环结束，进行下一行的循环
				if (_model.SquareInfo[x, y].Value == 0)
				{
					for (int Px = x + 1; Px < 4; Px++)
					{
						if(_model.SquareInfo[Px, y].Value == 0) continue;
						
						//找i到了不为零的数
						_model.SquareInfo[x, y].Value = _model.SquareInfo[Px, y].Value;
						_model.SquareInfo[Px, y].Value = 0;
						//到了这里说明找到了不为零的数，并且已经替换了位置，停止寻找不为零的for循环，进行下一步
						break;
					}
					//如果第一数没有寻找到不为零的数，则停止掉整个循环。进行下一行的判断
					break;
				}

				//取得这一行第二个数，如果该数不为零，则跳过这一步，
				//若为零一直寻找，直到找到不为零的数在跳过这一步，
				//如果一直没有找到不为零的数，则整个循环结束，进行下一个行的循环
				if (_model.SquareInfo[x + 1, y].Value == 0)
				{
					for (int Lx = x + 1; Lx < 4; Lx++)
					{
						if (_model.SquareInfo[Lx, y].Value == 0) continue;
						
						//找到了不为零的数,将这个数移动到距离第一个数最近的不为零的位置
						if (Lx != x + 1)
						{
							_model.SquareInfo[x + 1, y].Value = _model.SquareInfo[Lx, y].Value;
							_model.SquareInfo[Lx, y].Value = 0;
						}
						//找到了不为零的数,停止寻找不为零的for循环
						break;
					}
					//如果第二数没有寻找到不为零的数，则停止掉整个循环。进行下一行的判断
					break;
				}
				
				//第一个不为零的数和第二个不为零的数进行比较，
				//如果两个数相等，第一个数的值相加，第二个数的值清零，
				//如果两个数不相等，则进行下一个数的判断(相当与x++后再进行循环)
				if (_model.SquareInfo[x, y].Value == _model.SquareInfo[x + 1, y].Value)
				{
					_model.SquareInfo[x, y].Value = 2 * _model.SquareInfo[x, y].Value;
					_model.SquareInfo[x + 1, y].Value = 0;
				}
				else
				{
					//两个数不想等
					//进行下一个值的判断
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
			for (int x = 3; x >= 0; x--)
			{
				if (/*x == 3 && */_model.SquareInfo[x, y].Value == 0)
				{
					Debug.Log("右滑第一个为0");
					for (int k = x - 1; k >= 0; k--)
					{
						if (_model.SquareInfo[k, y].Value == 0) continue;
						_model.SquareInfo[x, y].Value = _model.SquareInfo[k, y].Value;
						_model.SquareInfo[k, y].Value = 0;
						//TODO 修改位置,将方块移动到该位置
						_model.SquareInfo[x, y].UITransform = _model.SquareInfo[k, y].UITransform;
						_model.SquareInfo[k, y].UITransform = null;
//						_model.SquareInfo[x, y].UITransform.localPosition = _model.SquareInfo[x, y].standardPos.localPosition;
						_model.SquareInfo[x, y].UITransform
							.DOLocalMove(_model.SquareInfo[x, y].standardPos.localPosition, 0.2f);
						break;
					}
				}
				
//				if (_model.SquareInfo[x, y].Value == 0) continue;
				
				for (int k = x - 1; k >= 0; k--)
				{
					
					if (_model.SquareInfo[k, y].Value == 0) continue;
					
					if (_model.SquareInfo[x, y].Value == _model.SquareInfo[k, y].Value) //两个方块的值相等
					{
						_model.SquareInfo[x, y].Value = 2 * _model.SquareInfo[x, y].Value;
						_model.SquareInfo[k, y].Value = 0;
						
						_model.SquareInfo[x, y].UITransform = _model.SquareInfo[x, y].UITransform;
						var y1 = y;
						var x2 = x;
						var index0 = k;

						_model.SquareInfo[k, y].UITransform
							.DOLocalMove(_model.SquareInfo[x, y].UITransform.localPosition, 0.2f)
							.OnComplete(() =>
							{
								_model.SquareInfo[index0, y1].DestroySquare();
								_model.SquareInfo[x2, y1].UITransform.GetComponentInChildren<TMP_Text>().text =
									"" + _model.SquareInfo[x2, y1].Value;
							});
						
//						_model.SquareInfo[k, y].DestroySquare();
//						_model.SquareInfo[x, y].UITransform.GetComponentInChildren<TMP_Text>().text =
//							"" + _model.SquareInfo[x, y].Value;
					}
					else
					{
						if (k != x - 1)
						{
							_model.SquareInfo[x - 1, y].Value = _model.SquareInfo[k, y].Value;
							_model.SquareInfo[x - 1, y].UITransform = _model.SquareInfo[k, y].UITransform;
//							_model.SquareInfo[x - 1, y].UITransform.localPosition = _model.SquareInfo[x - 1, y].standardPos.localPosition;
							_model.SquareInfo[x - 1, y].UITransform
								.DOLocalMove(_model.SquareInfo[x - 1, y].standardPos.localPosition, 0.2f);
							_model.SquareInfo[k, y].Value = 0;
							_model.SquareInfo[k, y].UITransform = null;
						}
					}
					break;
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
			for (int y = 0; y < 4; y++)
			{ 
				if (/*y == 0 && */_model.SquareInfo[x, y].Value == 0)
				{
					Debug.Log("上划滑第一个为0");
					for (int k = y + 1; k < 4; k++)
					{
						if (_model.SquareInfo[x, k].Value == 0) continue;
						_model.SquareInfo[x, y].UITransform = _model.SquareInfo[x, k].UITransform;
						_model.SquareInfo[x, y].Value = _model.SquareInfo[x, k].Value;
						//Pk = Pi；并且移动位置
						_model.SquareInfo[x, k].UITransform = null;
						_model.SquareInfo[x, k].Value = 0;
//						_model.SquareInfo[x, y].UITransform.localPosition = _model.SquareInfo[x, y].standardPos.localPosition;
						_model.SquareInfo[x, y].UITransform
							.DOLocalMove(_model.SquareInfo[x, y].standardPos.localPosition, 0.2f);
						break;
					}
				}
				
//				if (_model.SquareInfo[x, y].Value == 0) continue;
				
				for (int k = y + 1; k < 4; k++)
				{
					if (_model.SquareInfo[x, k].Value == 0) continue;
					
					Debug.Log("两个方块相等");
					if (_model.SquareInfo[x, y].Value == _model.SquareInfo[x, k].Value ) //两个方块的值相等
					{
						_model.SquareInfo[x, y].Value = 2 * _model.SquareInfo[x, y].Value;
						_model.SquareInfo[x, k].Value = 0;
						//TODO
						_model.SquareInfo[x, y].UITransform = _model.SquareInfo[x, y].UITransform;
						var y1 = y;
						var x2 = x;
						var index0 = k;

						_model.SquareInfo[k, y].UITransform
							.DOLocalMove(_model.SquareInfo[x, y].UITransform.localPosition, 0.2f)
							.OnComplete(() =>
							{
								_model.SquareInfo[x2, index0].DestroySquare();
								_model.SquareInfo[x2, y1].UITransform.GetComponentInChildren<TMP_Text>().text =
									"" + _model.SquareInfo[x2, y1].Value;
							});
						
//						_model.SquareInfo[x, k].DestroySquare();
//						_model.SquareInfo[x, y].UITransform.GetComponentInChildren<TMP_Text>().text =
//							"" + _model.SquareInfo[x, y].Value;
					}
					else
					{
						if (y + 1 != k)
						{
							_model.SquareInfo[x, y + 1].Value = _model.SquareInfo[x, k].Value;
							_model.SquareInfo[x, y + 1].UITransform = _model.SquareInfo[x, k].UITransform;
//							_model.SquareInfo[x, y + 1].UITransform.localPosition = _model.SquareInfo[x, y + 1].standardPos.localPosition;
							_model.SquareInfo[x, y + 1].UITransform
								.DOLocalMove(_model.SquareInfo[x, y + 1].standardPos.localPosition, 0.2f);
							
							_model.SquareInfo[x, k].Value = 0;
							_model.SquareInfo[x, k].UITransform = null;
						}
					}
					break;
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
			for (int y = 3; y >= 0; y--)
			{
				if (/*y == 3 && */_model.SquareInfo[x, y].Value == 0)
				{
					Debug.Log("下划滑第一个为0");
					for (int k = y - 1; k >= 0; k--)
					{
						if (_model.SquareInfo[x, k].Value == 0) continue;
						_model.SquareInfo[x, y].UITransform = _model.SquareInfo[x, k].UITransform;
						_model.SquareInfo[x, y].Value = _model.SquareInfo[x, k].Value;
						//Pk = Pi；并且移动位置
						_model.SquareInfo[x, k].UITransform = null;
						_model.SquareInfo[x, k].Value = 0;
//						_model.SquareInfo[x, y].UITransform.localPosition = _model.SquareInfo[x, y].standardPos.localPosition;
						_model.SquareInfo[x, y].UITransform
							.DOLocalMove(_model.SquareInfo[x, y].standardPos.localPosition, 0.2f);
						break;      
					}
				}
				
//				if (_model.SquareInfo[x, y].Value == 0) continue;
				
				for (int k = y - 1; k >= 0; k--)
				{
					if (_model.SquareInfo[x, k].Value == 0) continue;
					if (_model.SquareInfo[x, y].Value == _model.SquareInfo[x, k].Value) //两个方块的值相等
					{
						_model.SquareInfo[x, y].Value = 2 * _model.SquareInfo[x, y].Value;
						_model.SquareInfo[x, k].Value = 0;
						//TODO
						_model.SquareInfo[x, y].UITransform = _model.SquareInfo[x, y].UITransform;
						var y1 = y;
						var x2 = x;
						var index0 = k;

						_model.SquareInfo[k, y].UITransform
							.DOLocalMove(_model.SquareInfo[x, y].UITransform.localPosition, 0.2f)
							.OnComplete(() =>
							{
								_model.SquareInfo[x2, index0].DestroySquare();
								_model.SquareInfo[x2, y1].UITransform.GetComponentInChildren<TMP_Text>().text =
									"" + _model.SquareInfo[x2, y1].Value;
							});
						
//						_model.SquareInfo[x, k].DestroySquare();
//						_model.SquareInfo[x, y].UITransform.GetComponentInChildren<TMP_Text>().text =
//							"" + _model.SquareInfo[x, y].Value;
					}
					else
					{
						if (k != y - 1)
						{
							_model.SquareInfo[x, y - 1].Value = _model.SquareInfo[x, k].Value;
							_model.SquareInfo[x, y - 1].UITransform = _model.SquareInfo[x, k].UITransform;
//							_model.SquareInfo[x, y - 1].UITransform.localPosition = _model.SquareInfo[x, y - 1].standardPos.localPosition;
							_model.SquareInfo[x, y - 1].UITransform
								.DOLocalMove(_model.SquareInfo[x, y - 1].standardPos.localPosition, 0.2f);
							
							_model.SquareInfo[x, k].Value = 0;
							_model.SquareInfo[x, k].UITransform = null;
						}
					}
					break;
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
