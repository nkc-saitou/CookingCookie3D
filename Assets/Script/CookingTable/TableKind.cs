using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// テーブルの種類
/// </summary>
public enum TableType
{
    Table = 0,
    ElemTable,
    KneadTable,
    BakingTable,
    ExitTable
}

public class TableKind : MonoBehaviour {

    public TableType type;
}
