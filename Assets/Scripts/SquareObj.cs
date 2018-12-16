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
using TMPro;
using UnityEngine;

public class SquareObj
{
    public Transform transform;
    /// <summary>
    /// 标准坐标
    /// </summary>
    public Transform standardPos;
    private int _value;
    public int Value
    {
        get { return this._value; }
        set
        {
            this._value = value;
            transform.Find("text").GetComponent<TMP_Text>().text = "" + value;
        }
    }

    public SquareObj(Transform tran, int value, Transform standardPos)
    {
        this.transform = tran;
        this.Value = value;
        this.standardPos = standardPos;
    }

    public void DestroySquare()
    {
        GameObject.Destroy(transform.gameObject);
    }
}
