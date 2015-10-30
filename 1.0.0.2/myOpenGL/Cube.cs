using System;
using System.Collections.Generic;
using System.Text;

namespace OpenGL
{
    class Cube : TransformableObject
    {
        Wall leftWall, rightWall, frontWall, backWall, ceiling, floor;

        public Cube(Wall leftWall, Wall rightWall, Wall frontWall, Wall backWall, Wall ceiling, Wall floor)
        {
            this.frontWall = frontWall;
            this.backWall = backWall;
            this.rightWall = rightWall;
            this.leftWall = leftWall;
            this.ceiling = ceiling;
            this.floor = floor;
        }

        public void Draw(int picFrontSide)
        {
            GL.glDisable(GL.GL_LIGHTING);
            GL.glColor4f(1.0f, 1.0f, 1.0f, 1.0f);
            GL.glEnable(GL.GL_TEXTURE_2D);
            GL.glEnable(GL.GL_BLEND);
            GL.glEnable(GL.GL_CULL_FACE);

            if (picFrontSide == 1)
                GL.glCullFace(GL.GL_FRONT); //front side of pictures
            else
                GL.glCullFace(GL.GL_BACK); //back side of pictures

            DrawWallsWithTextures(picFrontSide);
            ApplyTransformation();
        }

        void DrawWallsWithTextures(int multMirrorPicture)
        {

            frontWall.DrawWithTexture(multMirrorPicture, true);
            backWall.DrawWithTexture(multMirrorPicture, true);
            leftWall.DrawWithTexture(multMirrorPicture, false);
            rightWall.DrawWithTexture(multMirrorPicture, false);
            ceiling.DrawWithTexture(multMirrorPicture, false);
            floor.DrawWithTexture(multMirrorPicture, false);

        }
        
    }
}
