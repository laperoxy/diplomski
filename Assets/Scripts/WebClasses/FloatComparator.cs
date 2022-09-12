
using System;

public class FloatComparator
{
    
    private const float FLOAT_COMPARISON_TOLERANCE = 0.05f;
    
    public static bool Equal(float a, float b)
    {
        return Math.Abs(a - b) <= FLOAT_COMPARISON_TOLERANCE;
    }
    
    public static bool NotEqual(float a, float b)
    {
        return Math.Abs(a - b) > FLOAT_COMPARISON_TOLERANCE;
    }
    
    public static bool EqualWithCustomTolerance(float a, float b, float tolerance)
    {
        return Math.Abs(a - b) <= tolerance;
    }
    
    public static bool NotEqualWithCustomTolerance(float a, float b, float tolerance)
    {
        return Math.Abs(a - b) > tolerance;
    }
    
    
    
}
