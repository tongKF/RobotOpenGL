using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace OpenGL
{
    class ProjectionLight : TransformableObject
    {
        Color color;
        float radius;

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

        public float Radius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value;
            }
        }
        #endregion

        public ProjectionLight()
        {
            color = new Color();
            radius = 0.0f;
        }

        public void Draw()
        {
            GL.glColor3f(color.R, color.G, color.B);
            GLUT.glutSolidSphere(radius, 8, 8);
            ApplyTransformation();
        }
    }
}
