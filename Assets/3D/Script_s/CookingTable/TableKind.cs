using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
