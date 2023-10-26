using UnityEngine;

public class GameResourcesController : MonoBehaviour
{
    private int _resourceA = 1000;
    private int _resourceB = 0;

    public int ResourceA { get { return _resourceA; } }
    public int ResourceB { get { return _resourceB; } }

    // Convertation of resource A to B (1A = 2B)
    public void ConvertAToB(int resourceQuentity)
    {
        if (resourceQuentity <= _resourceA)
        {
            _resourceA -= resourceQuentity;
            _resourceB += resourceQuentity * 2;
        }
    }
}