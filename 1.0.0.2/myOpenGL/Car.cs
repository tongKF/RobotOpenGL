using System;
using System.Collections.Generic;
using System.Text;

namespace OpenGL
{
    class Car
    {
        Milkshape.Character carModel;

        uint CAR_LIST, CAR;
        uint CAR_SHADOW_LIST, CAR_SHADOW;

        public float Drive = 0;

        public Car()
        {

            carModel = new Milkshape.Character("f360.ms3d");
        }

        public void Draw(bool isForShades)
        {

            GL.glPushMatrix();
            GL.glScalef(-1, 1, 1);
            GL.glRotatef(-90, 0, 1, 0);
            GL.glTranslatef(7, 0, 0);
            GL.glTranslatef(0, 0, (Drive % 46) - 23);
            if (!isForShades)
            {
                GL.glEnable(GL.GL_LIGHTING);
                GL.glCallList(CAR);
            }
            else
            {
                GL.glDisable(GL.GL_LIGHTING);
                GL.glCallList(CAR_SHADOW);
            }
            GL.glPopMatrix();

        }

        public void PrepareAndDraw()
        {
            CAR_LIST = GL.glGenLists(1);
            CAR = CAR_LIST + 1;
            GL.glPushMatrix();
            GL.glNewList(CAR, GL.GL_COMPILE);
            GL.glTranslatef(0, -5.5f, 0);
            GL.glScalef(0.1f, 0.1f, 0.1f);

            carModel.DrawModel(true, 1);
            GL.glPopMatrix();
            GL.glEndList();
            GL.glPopMatrix();
        }

        public void PrepareAndDrawShadow()
        {
            CAR_SHADOW_LIST = GL.glGenLists(1);
            CAR_SHADOW = CAR_SHADOW_LIST + 1;
            GL.glPushMatrix();
            GL.glNewList(CAR_SHADOW, GL.GL_COMPILE);
            GL.glTranslatef(0, -5.5f, 0);
            GL.glScalef(0.1f, 0.1f, 0.1f);
            carModel.DrawModel(false, 1);
            GL.glPopMatrix();
            GL.glEndList();
            GL.glPopMatrix();
        }
    }
}
