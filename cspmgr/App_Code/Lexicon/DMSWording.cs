using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// WordingItem 的摘要描述
/// </summary>
[System.Runtime.Serialization.DataContract]
public class WordingItem
{
    [System.Runtime.Serialization.DataMember]
    public string WordingType { get; set; }

    [System.Runtime.Serialization.DataMember]
    public string WordingID { get; set; }

    [System.Runtime.Serialization.DataMember]
    public string Chinese_T { get; set; }

    [System.Runtime.Serialization.DataMember]
    public string Chinese_S { get; set; }

    [System.Runtime.Serialization.DataMember]
    public string English { get; set; }

    [System.Runtime.Serialization.DataMember]
    public string Japanese { get; set; }

    [System.Runtime.Serialization.DataMember]
    public string ModifiedTime { get; set; }

    public WordingItem() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sWordingID">WordingID</param>
    /// <param name="sChinese_T">正體中文</param>
    /// <param name="sChinese_S">簡體中文</param>
    /// <param name="sEnglish">英文</param>
    /// <param name="sJapanese">日文</param>
    /// <param name="sModifiedTime">最後修改時間</param>
    public WordingItem(string sWordingType, string sWordingID, string sChinese_T, string sChinese_S, string sEnglish, string sJapanese, string sModifiedTime)
    {
        WordingType = sWordingType;
        WordingID = sWordingID;
        Chinese_T = sChinese_T;
        Chinese_S = sChinese_S;
        English = sEnglish;
        Japanese = sJapanese;
        ModifiedTime = sModifiedTime;
    }

}
