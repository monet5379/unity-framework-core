using System.Collections.Generic;
using UnityEngine;

namespace Monet5379
{
    public class AlphabetComparer : IComparer<Component>
    {
        public int Compare(Component a, Component b)
        {
            if (a != null && b != null)
            {
                string nameA = a.gameObject.name;
                string nameB = b.gameObject.name;
                return nameA.CompareTo(nameB);
            }

            return 0;
        }
    }
}