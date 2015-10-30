using System;
using System.Collections.Generic;
using System.Text;
using OpenGL;
using System.Drawing;

namespace OpenGL
{
    class Axes : TransformableObject
    {
        float xLen, yLen, zLen;

        Line xAxis, yAxis, zAxis;

        #region Properties

        public Color XColor
        {
            get
            {
                return xAxis.Color;
            }
            set
            {
                xAxis.Color = value;
            }
        }

        public Color YColor
        {
            get
            {
                return yAxis.Color;
            }
            set
            {
                yAxis.Color = value;
            }
        }
        public Color ZColor
        {
            get
            {
                return zAxis.Color;
            }
            set
            {
                zAxis.Color = value;
            }
        }

        public float XLen
        {
            get
            {
                return xLen;
            }
            set
            {
                xLen = value;
            }
        }

        public float YLen
        {
            get
            {
                return yLen;
            }
            set
            {
                yLen = value;
            }
        }

        public float ZLen
        {
            get
            {
                return zLen;
            }
            set
            {
                zLen = value;
            }
        }
        #endregion

        public Axes()
        {
            xAxis = new Line(
                            new Vertex(0.0f, 0.0f, 0.0f),
                            new Vertex(0.0f, 0.0f, 0.0f),
                            Color.Black
                            );
            yAxis = new Line(
                            new Vertex(0.0f, 0.0f, 0.0f),
                            new Vertex(0.0f, 0.0f, 0.0f),
                            Color.Black
                            );
            zAxis = new Line(
                            new Vertex(0.0f, 0.0f, 0.0f),
                            new Vertex(0.0f, 0.0f, 0.0f),
                            Color.Black
                            );

            xLen = yLen = zLen = 0.0f;
        }



        public void Draw()
        {
            xAxis.Vertex1.X = -xLen / 2;
            xAxis.Vertex1.Y = 0.0f;
            xAxis.Vertex1.Z = 0.0f;

            xAxis.Vertex2.X = xLen / 2;
            xAxis.Vertex2.Y = 0.0f;
            xAxis.Vertex2.Z = 0.0f;

            yAxis.Vertex1.X = 0.0f;
            yAxis.Vertex1.Y = -yLen / 2;
            yAxis.Vertex1.Z = 0.0f;

            yAxis.Vertex2.X = 0.0f;
            yAxis.Vertex2.Y = yLen / 2;
            yAxis.Vertex2.Z = 0.0f;

            zAxis.Vertex1.X = 0.0f;
            zAxis.Vertex1.Y = 0.0f;
            zAxis.Vertex1.Z = -zLen / 2;

            zAxis.Vertex2.X = 0.0f;
            zAxis.Vertex2.Y = 0.0f;
            zAxis.Vertex2.Z = zLen / 2;

            xAxis.Draw();
            yAxis.Draw();
            zAxis.Draw();

            ApplyTransformation();

        }

    }
}
