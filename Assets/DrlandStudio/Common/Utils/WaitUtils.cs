using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drland.Common.Utils
{
    public static class WaitUtils
    {
        public static IEnumerator Wait(float delay)
        {
            yield return new WaitForSeconds(delay);
        }

        public static IEnumerator WaitToDo(float delay, System.Action action)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }
    }
}
