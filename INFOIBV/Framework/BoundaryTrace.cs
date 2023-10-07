using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using INFOIBV.Framework;

namespace INFOIBV.Framework;

//
// Adapted from "Principles of Digital Image Processing Core Algorithms" by Authors Wilhelm Burger & Mark J. Burge, 2009, Pages 23-24
//

public class BoundaryTrace
{
    private readonly sbyte[,] _delta =
    {
        {1, 0},
        {1, 1},
        {0, 1},
        {-1, 1},
        {-1, 0},
        {-1, -1},
        {0, -1},
        {1, -1},
    };

    private List<Contour> _outerContours, _innerContours;

    private readonly byte _foreground = 1;
    private readonly byte _background = 0;

    private readonly byte[,] _input;
    private readonly int _height;
    private readonly int _width;

    public BoundaryTrace(byte[,] input)
    {
        _input = input;
        _width = input.GetLength(0);
        _height = input.GetLength(1);

        (_outerContours, _innerContours) = CombinedContourLabeling();
    }

    /// <summary>
    /// Returns the sets of outer and inner contours and a label map.
    /// </summary>
    public (List<Contour>, List<Contour>) CombinedContourLabeling()
    {
        var innerContours = new List<Contour>(50);
        var outerContours = new List<Contour>(50);

        var labelMap = new int[_width,_height];

        for (var u = 0; u < _width-1; u++)
        {
            for (var v = 0; v < _height-1; v++)
            {
                labelMap[u, v] = 0;
            }
        }

        var regionCounter = 0;

        for (var v = 0; v < _height-1 ; v++)
        {
            var label = 0;
            for (var u = 0; u < _width-1; u++)
            {
                if (_input[u, v] == _foreground)
                {
                    if (label != 0)
                        labelMap[u, v] = label;
                    else
                    {
                        label = labelMap[u, v];
                        if (label == 0)
                        {
                            regionCounter += 1;
                            label = regionCounter;
                            outerContours.Add(TraceContour(u, v, 0, label, labelMap));
                            labelMap[u, v] = label;
                        }
                    }
                }
                else
                {
                    if (label != 0)
                    {
                        if (labelMap[u, v] == 0)
                        {
                            innerContours.Add(TraceContour(u-1, v, 1, label, labelMap));
                        }

                        label = 0;
                    }
                }
            }
        }

        return (outerContours, innerContours);

    }

    /// <summary>
    /// Returns the Contour from a single point.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="direction"></param>
    /// <param name="label"></param>
    /// <param name="labelMap"></param>
    /// <returns></returns>
    private Contour TraceContour(int x, int y, int direction, int label, int[,] labelMap)
    {
        int xT,yT, xP,yP, xC,yC;
        int dirNext;

        (xT, yT, dirNext) = FindNextPoint(x, y, direction, labelMap);
        var contour = new Contour(xT, yT, label);
        xP = x;
        yP = y;
        xC = xT;
        yC = yT;
        var done = (x == xT && y == yT);

        while (!done)
        {
            labelMap[xC, yC] = label;
            var dirSearch = (dirNext + 6) % 8;
            int xN, yN;
            (xN, yN, dirNext) = FindNextPoint(xC, yC, dirSearch, labelMap);
            xP = xC;
            yP = yC;
            xC = xN;
            yC = yN;

            done = ((xP == x && yP == y) && (xC == xT && yC == yT));
            if (!done)
                contour.AddPoint(xN,yN);
        }

        return contour;
    }

    /// <summary>
    /// Finds the next point for the TraceContour method.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="direction"></param>
    /// <param name="labelMap"></param>
    /// <returns></returns>
    private (int, int, int) FindNextPoint(int x, int y, int direction, int[,] labelMap)
    {
        int xD, yD;
        for (var i = 0; i < 7; i++)
        {
           xD = x + _delta[direction,0];
           yD = y + _delta[direction,1];

           if (xD < 0 || yD < 0 || xD > _width || yD > _height)
               break;

           if (_input[xD, yD] == _background)
           {
               labelMap[xD, yD] = -1;
               direction = (direction + 1) % 8;
           }
           else
               return (xD, yD, direction);
        }

        return (x, y, direction);
    }

    public List<(int,int)> TraceBoundary()
    {
        throw new NotImplementedException();
    }


    public byte InLargestShape()
    {
        throw new NotImplementedException();
    }

}


public class Contour
{
    private int _x;
    private int _y;
    private int _label;

    private List<(int, int)> _points;
    public Contour(int x, int y, int label)
    {
        _x = x;
        _y = y;
        _label = label;
        _points = new List<(int, int)>();
    }

    public void AddPoint(int x, int y)
    {
        _points.Add((x,y));
    }

    public (int, int) GetPoint()
    {
        return (_x, _y);
    }

    public List<(int,int)> GetList()
    {
        return _points;
    }

}

