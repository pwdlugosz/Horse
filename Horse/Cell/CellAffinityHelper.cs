﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Equus.Horse
{

    /// <summary>
    /// Method set that supports converting cell affinities to and from strings
    /// </summary>
    public static class CellAffinityHelper
    {

        /// <summary>
        /// Convets text to a cell affinity
        /// </summary>
        /// <param name="Text">String to be parsed</param>
        /// <returns>Cell affinity</returns>
        public static CellAffinity Parse(string Text)
        {

            switch (Text.Trim().ToUpper())
            {

                case "BOOL":
                case "BOOLEAN":
                case "B":
                    return CellAffinity.BOOL;

                case "INT":
                case "INT64":
                case "I":
                    return CellAffinity.INT;

                case "DOUBLE":
                case "NUM64":
                case "D":
                    return CellAffinity.DOUBLE;

                case "DATE_TIME":
                case "DATE":
                case "T":
                    return CellAffinity.DATE_TIME;

                case "STRING":
                case "TEXT":
                case "S":
                    return CellAffinity.STRING;

                case "BLOB":
                case "HASH":
                case "H":
                    return CellAffinity.BLOB;

                default:
                    throw new Exception("Text is not a valid affinity: " + Text);

            }

        }

        /// <summary>
        /// Convets a cell affinity to a string
        /// </summary>
        /// <param name="Affinity">Cell affinity</param>
        /// <returns>String version of a cell affinity</returns>
        public static string ToString(CellAffinity Affinity)
        {
            return Affinity.ToString();
        }

        /// <summary>
        /// Returns the highest precedence data type
        /// </summary>
        /// <param name="A1">First type to compare</param>
        /// <param name="A2">Second type to compare</param>
        /// <returns>The highest cell precedence</returns>
        public static CellAffinity Highest(CellAffinity A1, CellAffinity A2)
        {

            if (A1 == CellAffinity.STRING || A2 == CellAffinity.STRING)
                return CellAffinity.STRING;
            else if (A1 == CellAffinity.BLOB || A2 == CellAffinity.BLOB)
                return CellAffinity.BLOB;
            else if (A1 == CellAffinity.DOUBLE || A2 == CellAffinity.DOUBLE)
                return CellAffinity.DOUBLE;
            else if (A1 == CellAffinity.INT || A2 == CellAffinity.INT)
                return CellAffinity.INT;
            else if (A1 == CellAffinity.DATE_TIME || A2 == CellAffinity.DATE_TIME)
                return CellAffinity.DATE_TIME;
            else
                return CellAffinity.BOOL;

        }

        /// <summary>
        /// Returns the highest precedence data type
        /// </summary>
        /// <param name="Affinity">A collection of cell afffinities</param>
        /// <returns>The highest cell precedence</returns>
        public static CellAffinity Highest(IEnumerable<CellAffinity> Affinity)
        {

            if (Affinity.Count() == 0)
                return CellAffinity.BOOL;
            else if (Affinity.Count() == 1)
                return Affinity.First();

            CellAffinity a = CellAffinity.BOOL;
            foreach (CellAffinity b in Affinity)
            {
                a = CellAffinityHelper.Highest(a, b);
            }
            return a;

        }

        /// <summary>
        /// Returns the lowest precedence data type
        /// </summary>
        /// <param name="A1">First type to compare</param>
        /// <param name="A2">Second type to compare</param>
        /// <returns>The lowest cell precedence</returns>
        public static CellAffinity Lowest(CellAffinity A1, CellAffinity A2)
        {

            if (A1 == CellAffinity.BOOL || A2 == CellAffinity.BOOL)
                return CellAffinity.BOOL;
            else if (A1 == CellAffinity.DATE_TIME || A2 == CellAffinity.DATE_TIME)
                return CellAffinity.DATE_TIME;
            else if (A1 == CellAffinity.INT || A2 == CellAffinity.INT)
                return CellAffinity.INT;
            else if (A1 == CellAffinity.DOUBLE || A2 == CellAffinity.DOUBLE)
                return CellAffinity.DOUBLE;
            else if (A1 == CellAffinity.BLOB || A2 == CellAffinity.BLOB)
                return CellAffinity.BLOB;
            else 
                return CellAffinity.STRING;

        }

        /// <summary>
        /// Returns the lowest precedence data type
        /// </summary>
        /// <param name="Affinity">A collection of cell afffinities</param>
        /// <returns>The lowest cell precedence</returns>
        public static CellAffinity Lowest(IEnumerable<CellAffinity> Affinity)
        {

            if (Affinity.Count() == 0)
                return CellAffinity.STRING;
            else if (Affinity.Count() == 1)
                return Affinity.First();

            CellAffinity a = CellAffinity.STRING;
            foreach(CellAffinity b in Affinity)
            {
                a = CellAffinityHelper.Lowest(a, b);
            }
            return a;

        }

    }

}
