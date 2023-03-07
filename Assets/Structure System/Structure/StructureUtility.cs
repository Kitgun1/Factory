using KiMath;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public static class StructureUtility
    {

        #region Points

        #region GetPoints

        public static List<StructurePointData> GetPoints(this Structure structure, PointState pointState)
        {
            List<StructurePointData> result = new List<StructurePointData>();

            if (structure != null)
                foreach (var point in structure.StructurePoints)
                    if (point.State == pointState)
                        result.Add(point);

            return result;
        }

        public static List<StructurePointData> GetPoints(this Structure structure, PriorityType priority)
        {
            List<StructurePointData> result = new List<StructurePointData>();

            if (structure != null)
                foreach (var point in structure.StructurePoints)
                    if (point.Priority == priority)
                        result.Add(point);

            return result;
        }

        public static List<StructurePointData> GetPoints(this Structure structure, PointState pointState, PriorityType priority)
        {
            List<StructurePointData> result = new List<StructurePointData>();

            if (structure != null)
                foreach (var point in structure.StructurePoints)
                    if (point.Priority == priority && point.State == pointState)
                        result.Add(point);

            return result;
        }

        public static StructurePointData? GetPoint(this Structure structure, Direction direction)
        {
            if (structure != null)
                foreach (var point in structure.StructurePoints)
                    if (point.Axis == direction)
                        return point;
            return null;
        }

        public static StructurePointData? GetPoint(this Structure structure, Direction direction, PointState pointState)
        {
            if (structure != null)
                foreach (var point in structure.StructurePoints)
                    if (point.Axis == direction && point.State == pointState)
                        return point;

            return null;
        }

        public static StructurePointData? GetPoint(this Structure structure, Direction direction, PointState pointState, PriorityType priority)
        {
            if (structure != null)
                foreach (var point in structure.StructurePoints)
                    if (point.Axis == direction && point.State == pointState && point.Priority == priority)
                        return point;

            return null;
        }

        #endregion

        #region PointWithProduct

        public static StructurePointData? GetPointWithProduct(this Structure structure)
        {
            List<StructurePointData> points = structure.StructurePoints;

            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Product != null)
                {
                    return points[i];
                }
            }

            return null;
        }

        public static StructurePointData? GetPointWithProduct(this Structure structure, PointState pointState)
        {
            List<StructurePointData> points = structure.StructurePoints;

            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Product != null && points[i].State == pointState)
                {
                    return points[i];
                }
            }

            return null;
        }

        #endregion

        #endregion

        #region Structures

        public static Structure GetStructure(this MapManage map, Vector2Int position)
        {
            return map.GetStructure(position.x, position.y);
        }

        public static Structure GetStructure(this MapManage map, Vector2Int position, Direction direction)
        {
            return map.GetStructure(position.x + direction.ToAxis().x, position.y + direction.ToAxis().y);
        }

        public static List<Structure> GetStructures(this MapManage map, Vector2Int position)
        {
            List<Structure> result = new List<Structure>();
            foreach (var axis in MathK.ToAxes())
            {
                result.Add(map.GetStructure(position.x + axis.x, position.y + axis.y));
            }
            return result;
        }

        #endregion

    }
}