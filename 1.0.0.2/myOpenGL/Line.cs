using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace OpenGL
{
    class Line : TransformableObject
    {
        Color color;
        Vertex v1, v2;

        #region Properties
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        public Vertex Vertex1
        {
            get
            {
                return v1;
            }
            set
            {
                v1 = value;
            }
        }
        public Vertex Vertex2
        {
            get
            {
                return v2;
            }
            set
            {
                v2 = value;
            }
        }
        #endregion

        public Line(Vertex v1, Vertex v2, Color color)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.color = color;
        }

        public void Draw()
        {
            GL.glBegin(GL.GL_LINES);
            GL.glColor3f(color.R, color.G, color.B);
            GL.glVertex3f(v1.X, v1.Y, v1.Z);
            GL.glVertex3f(v2.X, v2.Y, v2.Z);
            ApplyTransformation();
            GL.glEnd();
        }

            
    }
}
