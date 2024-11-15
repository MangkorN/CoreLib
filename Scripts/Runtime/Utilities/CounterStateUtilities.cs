using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreLib.Utilities
{
    public static class CounterStateUtilities
    {
        public static void ProcessCounterState(ref int counter, bool incrementCounter, Action onStateChange, Action onStateError)
        {
            if (incrementCounter)
            {
                counter++;

                if (counter == 1)
                {
                    onStateChange?.Invoke();
                    return;
                }
            }
            else
            {
                counter--;

                if (counter < 0)
                {
                    onStateError?.Invoke();
                    counter = 0;
                    return;
                }

                if (counter == 0)
                {
                    onStateChange?.Invoke();
                    return;
                }
            }
        }
    }
}
