using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using INFOIBV.Framework;

namespace INFOIBV.Framework;

//
// Adapted from "Principles of Digital Image Processing Core Algorithms" by Authors Wilhelm Burger & Mark J. Burge, 2009, Pages 288-292
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

    //0  = unlabeled
    //-1 = previously visited background pixel
    //>0 = a valid label
    private byte[,] _pixelArray;
    private int[,] _labelArray;

    private readonly byte _foreground = 1;
    private readonly byte _background = 0;

    private byte[,] _input;
    private int _height;
    private int _width;

    public BoundaryTrace(byte[,] input)
    {
        _input = input;
        _height = input.GetLength(0);
        _width = input.GetLength(1);
        MakeAuxArrays();
    }

    void MakeAuxArrays()
    {
        _pixelArray = new byte[_height+2, _width+2];
        _labelArray = new int[_height+2, _width+2];
        for (var v = 0; v < _height+2; v++)
        {
            for (var u = 0; u < _width+2; u++)
            {
                if (_input[v-1, u-1] == 0)
                    _pixelArray[v, u] = _background;
                else
                    _pixelArray[v, u] = _foreground;
            }
        }
    }

    private List<(int, int)> TraceContour(int xS, int yS, int label, int directionS, List<(int,int)> contour)
    {
        int xT, yT; // T = successor of starting point 
        int xP, yP; // P = previous contour point
        int xC, yC; // C = current contour point

        int directionNext, x, y;
        (directionNext, x, y) = FindNextPoint(xS,yS,directionS);
        contour.Add((x,y));

        xP = xS; yP = yS;
        xC = xT = x;
        yC = yT = y;

        bool done = (xS==xT && yS==yT); //true if isolated pixel

        while (!done)
        {
            _labelArray[yC, xC] = label;/////NNAANNNI
            int directionSearch = (directionNext + 6) % 8;
            xP = xC ; yP = yC;
            (directionNext, x, y) = FindNextPoint(xC, yC, directionSearch);
            xC = x;
            yC = y; 
            // Are we back at starting position?
            done = (xP==xS && yP==yS && xC==xT && yC==yT);
            if (!done)
                contour.Add((x,y));
        }

        return contour;
    }

    private (int,int,int) FindNextPoint(int pX, int pY, int direction)
    {
        int xR = pX, yR = pY;
        for (var i = 0; i < 7; i++)
        {
            var x = pX + _delta[direction, 0];
            var y = pY + _delta[direction, 1];

            if (_input[x,y] == _background)
            {
                _labelArray[y,x] = -1; ///Hier ook
                direction = (direction + 1) % 8;
            }
            else
            {
                xR = x; 
                yR = y;
                break;
            }
        }
        
        return (direction, xR, yR);
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

