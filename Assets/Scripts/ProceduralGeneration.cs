using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGeneration
{
    /// <summary>
    /// Logistic function
    /// </summary>
    /// <param name="x">Input variable x</param>
    /// <param name="L">The curve's maximum value</param>
    /// <param name="x0">The x-value of the sigmoid's midpoint</param>
    /// <param name="k">The steepness of the curve</param>
    /// <returns>The y output variable</returns>
    public static double Logistic(double x, double L, double x0, double k)
    {
        return L / (1 + Mathf.Exp((float)(-k * (x - x0))));
    }
}
