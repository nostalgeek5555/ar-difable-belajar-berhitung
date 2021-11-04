using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class Grid : MonoBehaviour
{
    public ColumnType columnType;
    public List<Area> areaList;
    //public Vector3 areaSize;
    public float cellSize;
    public Transform gridParent;
    public List<Vector3> gridPointList;
    public enum ColumnType { even, odd };
    [System.Serializable]
    public class Area
    {
        public Vector3 size;
        public Vector3 position;
        public List<Vector3> points;
    }

    #region Item Points
    [Button]
    public void SetUpLayerPoints()
    {
        gridPointList.Clear();
        foreach (var area in areaList)
        {
            area.points.Clear();
            SetUpPoints(area, columnType);
        }
    }

    private void SetUpPoints(Area area, ColumnType type)
    {
        float spaceX = cellSize / 4;
        float spaceY = cellSize / 4;
        Vector3 pointer = area.position;
        Vector3 areaSize = area.size;

        //midle row
        pointer = SetUpHorizontalRowPoints(pointer, spaceX, area, type);

        //all top rows
        float size = area.position.y + areaSize.y * 0.5f;
        for (float j = area.position.y; j < size; j += spaceY)
        {
            pointer.x = area.position.x;
            pointer.y = j;
            if (pointer.y + spaceY < size)
            {
                pointer.y += spaceY;
                //Instantiate(spawnPoint, point + gridParent.position, Quaternion.identity, transform);
                pointer = SetUpHorizontalRowPoints(pointer, spaceX, area, type);
            }
        }

        //all bottom rows
        float size2 = area.position.y - areaSize.y * 0.5f;
        for (float j = area.position.y; j > size2; j -= spaceY)
        {
            pointer.x = area.position.x;
            pointer.y = j;
            if (pointer.y - spaceY > size2)
            {
                pointer.y -= spaceY;
                //Instantiate(spawnPoint, point + gridParent.position, Quaternion.identity, transform);
                pointer = SetUpHorizontalRowPoints(pointer, spaceX, area, type);
            }
        }
    }

    // populate points on a row
    private Vector3 SetUpHorizontalRowPoints(Vector3 pointer, float space, Area area, ColumnType type)
    {
        Vector3 areaSize = area.size;

        // add mid point if odd
        float offset = type == ColumnType.odd ? 0f : space * 0.5f;
        switch (type)
        {
            case ColumnType.even:
                gridPointList.Add(new Vector3(pointer.x + offset, pointer.y, pointer.z));
                gridPointList.Add(new Vector3(pointer.x - offset, pointer.y, pointer.z));
                area.points.Add(new Vector3(pointer.x + offset, pointer.y, pointer.z));
                area.points.Add(new Vector3(pointer.x - offset, pointer.y, pointer.z));
                break;
            case ColumnType.odd:
                gridPointList.Add(pointer);
                area.points.Add(pointer);
                break;
        }

        float size = area.position.x + areaSize.x * 0.5f;
        for (float i = area.position.x + offset; i < size; i += space)
        {
            pointer.x = i;
            if (pointer.x + space < size)
            {
                pointer.x += space;
                gridPointList.Add(pointer);
                area.points.Add(pointer);
            }
        }

        float size2 = area.position.x - areaSize.x * 0.5f;
        for (float i = area.position.x - offset; i > size2; i -= space)
        {
            pointer.x = i;
            if (pointer.x - space > size2)
            {
                pointer.x -= space;
                gridPointList.Add(pointer);
                area.points.Add(pointer);
            }
        }
        return pointer;
    }

    public Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }

    public Vector3 DegreeToVector3(float degree)
    {
        var r = RadianToVector2(degree * Mathf.Deg2Rad);
        return new Vector3(r.x, 0, r.y);
    }
    #endregion

    void OnDrawGizmosSelected()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.yellow;
        if (areaList != null)
        {
            foreach (var item in areaList)
            {
                Gizmos.DrawWireCube(item.position, item.size);
                //Utility.DrawArrow(Vector3.zero, Vector3.forward * -item.size.z * 0.5f, 0.5f, 35, true);
            }
        }
        foreach (var item in gridPointList)
        {
            Gizmos.DrawWireSphere(item, 0.08f);
        }

    }
}
