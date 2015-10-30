using System;
using System.Collections.Generic;
using System.Text;

namespace OpenGL
{
    class TransformableObject
    {
        bool isMatrixPushed = false;

        public void Translate(float x, float y, float z)
        {
            if (!isMatrixPushed)
            {
                GL.glPushMatrix();
                isMatrixPushed = true;
            }
            GL.glTranslatef(x, y, z);
        }

        public void Rotate(float angle, float x, float y, float z)
        {
            if (!isMatrixPushed)
            {
                GL.glPushMatrix();
                isMatrixPushed = true;
            }
            GL.glRotatef(angle, x, y, z);
        }

        public void Scale(float x, float y, float z)
        {
            if (!isMatrixPushed)
            {
                GL.glPushMatrix();
                isMatrixPushed = true;
            }
            GL.glScalef(x, y, z);
        }

        public void ApplyTransformation()
        {
            if (isMatrixPushed)
            {
                //translate and rotate axes
                GL.glPopMatrix();
                isMatrixPushed = false;
            }
        }
    }
}
