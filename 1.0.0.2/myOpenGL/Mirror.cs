using System;
using System.Collections.Generic;
using System.Text;

namespace OpenGL
{
    class Mirror
    {
        float radius;
        GLUquadric obj;

        public Mirror()
        {
            obj = GLU.gluNewQuadric(); //!!!
        }
        ~Mirror()
        {
            GLU.gluDeleteQuadric(obj); //!!!
        }
        public void DrawFrame()
        {
            //draw the Frame of mirror
            radius = 0.18f;
            GL.glPushMatrix();
            GL.glColor3f(0.3f, 0.1f, 0.0f);
            GL.glTranslatef(0, -1, 3);
            GLU.gluSphere(obj, radius, 20, 20);
            GL.glTranslatef(0, 1, -3);
            GL.glTranslatef(0, -1, -3f);
            GLU.gluCylinder(obj, radius, radius, radius * 33, 20, 20);
            GLU.gluSphere(obj, radius, 20, 20);
            GL.glTranslatef(0, -5.9f, 0);
            GLU.gluCylinder(obj, radius, radius, radius * 33, 20, 20);
            GLU.gluSphere(obj, radius, 20, 20);
            GL.glRotatef(-90, 1, 0, 0);
            GLU.gluCylinder(obj, radius, radius, radius * 33, 20, 20);
            GLU.gluSphere(obj, radius, 20, 20);
            GL.glScalef(-1, 1, 1);
            GL.glTranslatef(0, -6, 0);
            GLU.gluCylinder(obj, radius, radius, radius * 33, 20, 20);
            GLU.gluSphere(obj, radius, 20, 20);
            GL.glPopMatrix();
        }

        public void Draw()
        {
            GL.glEnable(GL.GL_LIGHTING);
            GL.glBegin(GL.GL_QUADS);
            //!!! for blended REFLECTION 
            GL.glColor4d(0, 0, 1, 0.5);
            GL.glVertex3d(-3, -3, 0);
            GL.glVertex3d(-3, 3, 0);
            GL.glVertex3d(3, 3, 0);
            GL.glVertex3d(3, -3, 0);
            GL.glEnd();

        }
    }
}
