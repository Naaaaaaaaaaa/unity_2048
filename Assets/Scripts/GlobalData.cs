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

public class GlobalData : MonoBehaviour {
    /// <summary>
    /// 获取方块的值是2还是4
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int GetValue(int value)
    {
        if (value == 1)
        {
            return 2;
        }
        return 4;
    }
}
