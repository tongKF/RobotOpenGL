using System;
using System.Collections.Generic;
using System.Text;

namespace OpenGL
{
    class Wall : TransformableObject
    {
    //float[,] coord = new float[3, 3];
        Vertex v1, v2, v3, v4;
        uint textureId;

        public Wall(float[,] coord, uint textureId, Vertex v1, Vertex v2, Vertex v3, Vertex v4)
        {
            //copy coord array
           //for (int i = 0; i < 3; i++)
           //{
           //    for (int j = 0; j < 3; j++)
           //    {
           //        this.coord[i, j] = coord[i, j];
           //    }
           //}
            this.textureId = textureId;
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            this.v4 = v4;
        }


        public void DrawWithTexture(int multMirrorPicture, bool isFrontOrbackWall)
        {
            // multMirrorPicture == -1 : mirror pictures

            uint swapPictures = 0; // if swapPictures == 0 picture left = picture right
            if (multMirrorPicture == -1)
                swapPictures = 1;
            else
                swapPictures = 0;

            if (isFrontOrbackWall)
            {
                if (this.textureId == 12)
                    GL.glBindTexture(GL.GL_TEXTURE_2D, this.textureId + swapPictures);
                else if (this.textureId == 13)
                    GL.glBindTexture(GL.GL_TEXTURE_2D, this.textureId - swapPictures);
            }
            else
                GL.glBindTexture(GL.GL_TEXTURE_2D, this.textureId);

            GL.glBegin(GL.GL_QUADS);
            if (isFrontOrbackWall)
            {
                GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(multMirrorPicture * v1.X, v1.Y, v1.Z);
                GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(multMirrorPicture * v2.X, v2.Y, v2.Z);
                GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(multMirrorPicture * v3.X, v3.Y, v3.Z);
                GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(multMirrorPicture * v4.X, v4.Y, v4.Z);
            }
            else
            {
                GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(v1.X, v1.Y, multMirrorPicture * v1.Z);
                GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(v2.X, v2.Y, multMirrorPicture * v2.Z);
                GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(v3.X, v3.Y, multMirrorPicture * v3.Z);
                GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(v4.X, v4.Y, multMirrorPicture * v4.Z);
            }
            GL.glEnd();
        }
        
    }
}
