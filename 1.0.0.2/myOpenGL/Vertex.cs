using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace OpenGL
{
    class Vertex
    {
        float x, y, z;


        #region Properties
        public float X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }
        public float Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }
        public float Z
        {
            get
            {
                return z;
            }
            set
            {
                z = value;
            }
        }

        #endregion

        public Vertex(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

    }
}
