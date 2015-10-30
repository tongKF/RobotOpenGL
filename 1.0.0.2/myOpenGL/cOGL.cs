using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace OpenGL
{
    class cOGL
    {
        #region Variables

        //Milkshape.Character monster, diver;
        Control panel;
        int Width;
        int Height;
        float xMaxOfCube = 20.0f, yMaxOfCube = 7.0f, zMaxOfCube = 20.0f;


        Axes axes = new Axes();
        ProjectionLight light = new ProjectionLight();
        Mirror mirror = new Mirror();
        Cube cube;
        Car car;

        public Robot Robot;
      
        //enum indicates the possible values of the arrows buttons 
        public enum arrow { forward, backward, right, left };
        //hold the value of the pressed button
        public arrow button;
        //indicates wether WeaponIndex changed
        public bool Weaponchanged = false;
        public float JumpSpeed = 1;
        public float[] LightPosition = new float[4];
        public float[] ScrollValue = new float[14];
        public double[] WillRobotPlace = new double[2];
        public arrow WhatWillDirection = arrow.forward;
        public float Move = 0.0f;

        // hold the current MODELVIEW Matrix
        double[] AccumulatedRotationsTraslations = new double[16];
        double[] ModelVievMatrixBeforeSpecificTransforms = new double[16];
        double[] ModelVievMatrixBeforeSpecificTransformsCAR = new double[16];
        double[] CurrentRotationTraslation = new double[16];
        double[] CurrentRotationTraslationCAR = new double[16];

        public double[] RobotPlace = new double[2],
                        RobotPlaceRight = new double[2],
                        RobotPlaceLeft = new double[2],
                        RobotPlaceForward = new double[2],
                        RobotPlaceBackward = new double[2];

        float[,] frontWallCoordForShadow = new float[3, 3];
        float[,] backWallCoordForShadow = new float[3, 3];
        float[,] leftWallCoordForShadow = new float[3, 3];
        float[,] rightWallCoordForShadow = new float[3, 3];
        float[,] ceilingCoordForShadow = new float[3, 3];
        float[,] floorCoordForShadow = new float[3, 3];


        #region Mouse Control
        #region Translation
        float X = 0.0f;		        // Translate screen to x direction (left or right)
        float Y = 0.0f;	        	// Translate screen to y direction (up or down)
        float Z = 0.0f; 	        // Translate screen to z direction (zoom in or out)

        public float X_Value
        {
            get { return X; }
            set { X = value; }
        }

        public float Y_Value
        {
            get { return Y; }
            set { Y = value; }
        }

        public float Z_Value
        {
            get { return Z; }
            set { Z = value; }
        }
        #endregion

        #region Rotaion
        float rotX = 0.0f;	            // Rotate screen on x axis 
        float rotY = 0.0f;	            // Rotate screen on y axis
        float rotZ = 0.0f;	            // Rotate screen on z axis

        public float RotX
        {
            get { return rotX; }
            set { rotX = value; }
        }

        public float RotY
        {
            get { return rotY; }
            set { rotY = value; }
        }

        public float RotZ
        {
            get { return rotZ; }
            set { rotZ = value; }
        }
        #endregion

        #region Last Rot/Trans
        float last_X = 0.0f;
        float last_Y = 0.0f;
        float last_rotX = 0.0f;
        float last_rotY = 0.0f;

        public float Last_X
        {
            get { return last_X; }
            set { last_X = value; }
        }

        public float Last_Y
        {
            get { return last_Y; }
            set { last_Y = value; }
        }

        public float Last_rotX
        {
            get { return last_rotX; }
            set { last_rotX = value; }
        }

        public float Last_rotY
        {
            get { return last_rotY; }
            set { last_rotY = value; }
        }
        #endregion
        #endregion
        #endregion

        public cOGL(Control pb)
        {

            panel = pb;
            Width = panel.Width;
            Height = panel.Height;

            frontWallCoordForShadow = new float[3, 3];
            backWallCoordForShadow = new float[3, 3];
            leftWallCoordForShadow = new float[3, 3];
            rightWallCoordForShadow = new float[3, 3];
            ceilingCoordForShadow = new float[3, 3];
            floorCoordForShadow = new float[3, 3];

            frontWallCoordForShadow[0, 0] = 19.99f;
            frontWallCoordForShadow[0, 1] = -1;
            frontWallCoordForShadow[0, 2] = 1;

            frontWallCoordForShadow[1, 0] = 19.99f;
            frontWallCoordForShadow[1, 1] = 0;
            frontWallCoordForShadow[1, 2] = 1;

            frontWallCoordForShadow[2, 0] = 19.99f;
            frontWallCoordForShadow[2, 1] = 1;
            frontWallCoordForShadow[2, 2] = 0;


            backWallCoordForShadow[0, 0] = -19.99f;
            backWallCoordForShadow[0, 1] = 1;
            backWallCoordForShadow[0, 2] = 1;

            backWallCoordForShadow[1, 0] = -19.99f;
            backWallCoordForShadow[1, 1] = 0;
            backWallCoordForShadow[1, 2] = 1;

            backWallCoordForShadow[2, 0] = -19.99f;
            backWallCoordForShadow[2, 1] = 1;
            backWallCoordForShadow[2, 2] = 0;

            ////////////////
            leftWallCoordForShadow[0, 0] = 1;
            leftWallCoordForShadow[0, 1] = 1;
            leftWallCoordForShadow[0, 2] = 19.99f;

            leftWallCoordForShadow[1, 0] = 0;
            leftWallCoordForShadow[1, 1] = -1;
            leftWallCoordForShadow[1, 2] = 19.99f;

            leftWallCoordForShadow[2, 0] = -1;
            leftWallCoordForShadow[2, 1] = 0;
            leftWallCoordForShadow[2, 2] = 19.99f;


            rightWallCoordForShadow[0, 0] = 1;
            rightWallCoordForShadow[0, 1] = 1;
            rightWallCoordForShadow[0, 2] = -19.99f;

            rightWallCoordForShadow[1, 0] = 0;
            rightWallCoordForShadow[1, 1] = 1;
            rightWallCoordForShadow[1, 2] = -19.99f;

            rightWallCoordForShadow[2, 0] = 1;
            rightWallCoordForShadow[2, 1] = 0;
            rightWallCoordForShadow[2, 2] = -19.99f;

            ceilingCoordForShadow[0, 0] = -1;
            ceilingCoordForShadow[0, 1] = 6.99f;
            ceilingCoordForShadow[0, 2] = 0;

            ceilingCoordForShadow[1, 0] = 1;
            ceilingCoordForShadow[1, 1] = 6.99f;
            ceilingCoordForShadow[1, 2] = 0;

            ceilingCoordForShadow[2, 0] = 1;
            ceilingCoordForShadow[2, 1] = 6.99f;
            ceilingCoordForShadow[2, 2] = 1;


            floorCoordForShadow[0, 0] = 1;
            floorCoordForShadow[0, 1] = -6.99f;
            floorCoordForShadow[0, 2] = 0;

            floorCoordForShadow[1, 0] = -1;
            floorCoordForShadow[1, 1] = -6.99f;
            floorCoordForShadow[1, 2] = 0;

            floorCoordForShadow[2, 0] = -1;
            floorCoordForShadow[2, 1] = -6.99f;
            floorCoordForShadow[2, 2] = 1;

            InitializeGL();

            Wall frontWall = new Wall(frontWallCoordForShadow, Textures[2],
                            new Vertex(-1.0f, -1.0f, 1.0f),
                            new Vertex(1.0f, -1.0f, 1.0f),
                            new Vertex(1.0f, 1.0f, 1.0f),
                            new Vertex(-1.0f, 1.0f, 1.0f));

            Wall backWall = new Wall(backWallCoordForShadow, Textures[3],
                                        new Vertex(1.0f, -1.0f, -1.0f),
                                        new Vertex(-1.0f, -1.0f, -1.0f),
                                        new Vertex(-1.0f, 1.0f, -1.0f),
                                        new Vertex(1.0f, 1.0f, -1.0f));

            Wall leftWall = new Wall(leftWallCoordForShadow, Textures[4],
                                        new Vertex(-1.0f, -1.0f, -1.0f),
                                        new Vertex(-1.0f, -1.0f, 1.0f),
                                        new Vertex(-1.0f, 1.0f, 1.0f),
                                        new Vertex(-1.0f, 1.0f, -1.0f));

            Wall rightWall = new Wall(rightWallCoordForShadow, Textures[5],
                                        new Vertex(1.0f, -1.0f, 1.0f),
                                        new Vertex(1.0f, -1.0f, -1.0f),
                                        new Vertex(1.0f, 1.0f, -1.0f),
                                        new Vertex(1.0f, 1.0f, 1.0f));

            Wall ceiling = new Wall(ceilingCoordForShadow, Textures[6],
                                        new Vertex(-1.0f, 1.0f, 1.0f),
                                        new Vertex(1.0f, 1.0f, 1.0f),
                                        new Vertex(1.0f, 1.0f, -1.0f),
                                        new Vertex(-1.0f, 1.0f, -1.0f));

            Wall floor = new Wall(floorCoordForShadow, Textures[7],
                                        new Vertex(1.0f, -1.0f, -1.0f),
                                        new Vertex(1.0f, -1.0f, 1.0f),
                                        new Vertex(-1.0f, -1.0f, 1.0f),
                                        new Vertex(-1.0f, -1.0f, -1.0f));


            cube = new Cube(leftWall, rightWall, frontWall, backWall, ceiling, floor);
            //prepare Robot, car and shadows lists
            
            Robot = new Robot(Textures[8]);
            Robot.WeaponIndex = 1;
            Weaponchanged = true;
            Robot.PrepareAndDraw();
            Robot.PrepareAndDrawShadow();

            car = new Car();
            car.PrepareAndDraw();
            car.PrepareAndDrawShadow();




        }

        ~cOGL()
        {
            WGL.wglDeleteContext(m_uint_RC);
        }

        uint m_uint_HWND = 0;

        public uint HWND
        {
            get { return m_uint_HWND; }
        }

        uint m_uint_DC = 0;

        public uint DC
        {
            get { return m_uint_DC; }
        }

        uint m_uint_RC = 0;

        public uint RC
        {
            get { return m_uint_RC; }
        }

        void DrawLight()
        {
            // call this function in order to apply lightning on the objects
            GL.glLightfv(GL.GL_LIGHT0, GL.GL_POSITION, LightPosition);
            GL.glDisable(GL.GL_LIGHTING); //disable lightning in order to glColor3f to work, otherwise spehre color will not been affected 

            //Draw Yellow Light Source
            light.Color = Color.Yellow;
            light.Radius = 0.1f;
            light.Translate(LightPosition[0], LightPosition[1], LightPosition[2]);
            light.Draw();

            ////draw projection line from source to plane
            //GL.glBegin(GL.GL_LINES);
            //GL.glColor3d(0.5, 0.5, 0);
            //GL.glVertex3d(LightPosition[0], LightPosition[1], LightPosition[2]);
            //GL.glVertex3d(LightPosition[0], floor[0, 2] - 0.01, LightPosition[2]);
            //GL.glEnd();

            axes.XLen = 6;
            axes.YLen = 6;
            axes.ZLen = 6;

            axes.XColor = Color.Red;
            axes.YColor = Color.Green;
            axes.ZColor = Color.Blue;

           // axes.Translate(3.0f, -3.0f, 0.0f);
           // axes.Rotate(90.0f, 1.0f, 0.0f, 0.0f);
            //axes.Draw();

        }
        
        double[] WillPlace(float x, float y, float z)
        {
            double[] Robot_place_temp = new double[2];
            Robot_place_temp[0] = RobotPlace[0];
            Robot_place_temp[1] = RobotPlace[1];
            GL.glRotatef(-Robot.WalkAngle, 0, 1, 0);
            GL.glTranslatef(x, y, z);
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, AccumulatedRotationsTraslations);
            WillRobotPlace[0] = AccumulatedRotationsTraslations[12];
            WillRobotPlace[1] = AccumulatedRotationsTraslations[14];


            if (Math.Abs(WillRobotPlace[0]) < Math.Abs(Robot_place_temp[0]) && Math.Abs(Robot_place_temp[0]) > 1.5)
                Robot_place_temp[0] = 18.49f;
            else if (Math.Abs(WillRobotPlace[0]) > Math.Abs(Robot_place_temp[0]) && Math.Abs(Robot_place_temp[0]) < 1.5 && Math.Abs(Robot_place_temp[1]) < 4)
                Robot_place_temp[0] = 1.6f;
            if (Math.Abs(WillRobotPlace[1]) < Math.Abs(Robot_place_temp[1]) && Math.Abs(Robot_place_temp[1]) > 4.5)
                Robot_place_temp[1] = 18.49f;
            else if (Math.Abs(WillRobotPlace[1]) > Math.Abs(Robot_place_temp[1]) && Math.Abs(Robot_place_temp[1]) < 4.5 && Math.Abs(Robot_place_temp[1]) > 4)
                Robot_place_temp[1] = 4.6f;

            //return to orginal position matrix
            GL.glTranslatef(-x, -y, -z);
            GL.glRotatef(Robot.WalkAngle, 0, 1, 0);
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, AccumulatedRotationsTraslations);
            return Robot_place_temp;
        }

        void RotateRobotByDirection()
        {
            switch (button)
            {
                case cOGL.arrow.backward:
                    GL.glRotatef(180, 0, 1, 0);
                    break;
                case cOGL.arrow.right:
                    GL.glRotatef(-90, 0, 1, 0);
                    break;
                case cOGL.arrow.left:
                    GL.glRotatef(90, 0, 1, 0);
                    break;
            }
        }

        void MoveRobotByDirection()
        {
            if (!Robot.IsMoving)
                return;

            //check the direction of walking Robot
            switch (button)
            {
                case arrow.forward:
                    {

                        if (Math.Abs(RobotPlaceForward[1]) < 18.5 && Math.Abs(RobotPlaceForward[0]) < 18.5
                            && (Math.Abs(RobotPlaceForward[1]) > 4.5 || Math.Abs(RobotPlaceForward[0]) > 1.5)
                            || (Robot.Jump > 15 && (Math.Abs(RobotPlaceForward[1]) < 5 && Math.Abs(RobotPlaceForward[0]) < 2)))
                        {
                            GL.glRotatef(-Robot.WalkAngle, 0, 1, 0);
                            GL.glTranslatef(0.2f * Move, 0, 0);
                            GL.glRotatef(Robot.WalkAngle, 0, 1, 0);

                        }
                        WhatWillDirection = button;
                    }
                    break;
                case arrow.backward:
                    {
                        if (Math.Abs(RobotPlaceBackward[1]) < 18.5 && Math.Abs(RobotPlaceBackward[0]) < 18.5
                                        && (Math.Abs(RobotPlaceBackward[1]) > 4.5 || Math.Abs(RobotPlaceBackward[0]) > 1.5)
                                        || (Robot.Jump > 15 && (Math.Abs(RobotPlaceBackward[1]) < 5 && Math.Abs(RobotPlaceBackward[0]) < 2)))
                        {
                            GL.glRotatef(-Robot.WalkAngle, 0, 1, 0);
                            GL.glTranslatef(-0.2f * Move, 0, 0);
                            GL.glRotatef(Robot.WalkAngle, 0, 1, 0);

                        }
                        WhatWillDirection = button;
                    }
                    break;
                case arrow.right:
                    {

                        if (Math.Abs(RobotPlaceRight[1]) < 18.5 && Math.Abs(RobotPlaceRight[0]) < 18.5
                            && (Math.Abs(RobotPlaceRight[1]) > 4.5 || Math.Abs(RobotPlaceRight[0]) > 1.5)
                            || (Robot.Jump > 15 && (Math.Abs(RobotPlaceRight[1]) < 5 && Math.Abs(RobotPlaceRight[0]) < 2)))
                        {
                            GL.glRotatef(-Robot.WalkAngle, 0, 1, 0);
                            GL.glTranslatef(0, 0, 0.2f * Move);
                            GL.glRotatef(Robot.WalkAngle, 0, 1, 0);

                        }
                        WhatWillDirection = button;
                    }
                    break;
                case arrow.left:
                    {
                        if (Math.Abs(RobotPlaceLeft[1]) < 18.5 && Math.Abs(RobotPlaceLeft[0]) < 18.5
                            && (Math.Abs(RobotPlaceLeft[1]) > 4.5 || Math.Abs(RobotPlaceLeft[0]) > 1.5)
                            || (Robot.Jump > 15 && (Math.Abs(RobotPlaceLeft[1]) < 5 && Math.Abs(RobotPlaceLeft[0]) < 2)))
                        {
                            GL.glRotatef(-Robot.WalkAngle, 0, 1, 0);
                            GL.glTranslatef(0, 0, -0.2f * Move);
                            GL.glRotatef(Robot.WalkAngle, 0, 1, 0);

                        }
                        WhatWillDirection = button;
                    }
                    break;
            }
        }

        public void Draw()
        {
            if (m_uint_DC == 0 || m_uint_RC == 0)
                return;

            GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT | GL.GL_STENCIL_BUFFER_BIT);

            GL.glLoadIdentity();

            if (Weaponchanged)
            {
                //if WeaponIndex changed create lists again!
                Robot.PrepareAndDraw();
                Robot.PrepareAndDrawShadow();
                Weaponchanged = false;
            }

            GLU.gluLookAt(ScrollValue[0], ScrollValue[1], ScrollValue[2],
                       ScrollValue[3], ScrollValue[4], ScrollValue[5],
                       ScrollValue[6], ScrollValue[7], ScrollValue[8]);

            #region Mouse Control
            GL.glTranslatef(-X, -Y, -Z);
            GL.glRotatef(rotX, 1.0f, 0.0f, 0.0f);
            GL.glRotatef(rotY, 0.0f, 1.0f, 0.0f);
            GL.glRotatef(rotZ, 0.0f, 0.0f, 1.0f);
            #endregion


            //move camera to initial position - center of cube and above floor
            GL.glTranslatef(0.0f, 0.0f, -1.0f);
            GL.glTranslatef(0.0f, 5.0f, 0.0f);
            GL.glRotatef(-90.0f, 0.0f, 1.0f, 0.0f);
    

            //update light position
            LightPosition[0] = ScrollValue[10];
            LightPosition[1] = ScrollValue[11];
            LightPosition[2] = ScrollValue[12];
            LightPosition[3] = 1.0f;

            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, ModelVievMatrixBeforeSpecificTransforms);
            GL.glLoadIdentity(); // make it identity matrix

            MoveRobotByDirection();

            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, CurrentRotationTraslation);
            GL.glLoadMatrixd(AccumulatedRotationsTraslations); //Global Matrix
            GL.glMultMatrixd(CurrentRotationTraslation);
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, AccumulatedRotationsTraslations);
            GL.glLoadMatrixd(ModelVievMatrixBeforeSpecificTransforms);
            GL.glMultMatrixd(AccumulatedRotationsTraslations);


            //draw actual cube
            cube.Scale(xMaxOfCube, yMaxOfCube, zMaxOfCube);
            cube.Draw(1);

            GL.glRotatef(180, 0, 1, 0);
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, ModelVievMatrixBeforeSpecificTransforms);

            GL.glPushMatrix();
            GL.glLoadIdentity();
            GL.glLoadMatrixd(ModelVievMatrixBeforeSpecificTransforms);
            GL.glMultMatrixd(AccumulatedRotationsTraslations);


            //!!!!!!!!!!! draw orginal Robot
            GL.glPushMatrix();
            RotateRobotByDirection();
            GL.glCullFace(GL.GL_BACK);
            GL.glDisable(GL.GL_CULL_FACE);
            GL.glEnable(GL.GL_LIGHTING);
            GL.glRotatef(-Robot.WalkAngle, 0, 1, 0);
            Robot.Draw(false);
            GL.glPopMatrix();


            GL.glLoadMatrixd(AccumulatedRotationsTraslations); //Global Matrix

            RobotPlace[0] = AccumulatedRotationsTraslations[12];
            RobotPlaceForward[0] = RobotPlaceBackward[0] = RobotPlaceRight[0] = RobotPlaceLeft[0] = AccumulatedRotationsTraslations[12];
            RobotPlace[1] = AccumulatedRotationsTraslations[14];
            RobotPlaceForward[1] = RobotPlaceBackward[1] = RobotPlaceRight[1] = RobotPlaceLeft[1] = AccumulatedRotationsTraslations[14];

            switch (WhatWillDirection)
            {
                case arrow.forward:
                    //Look future to get wallk forward
                    RobotPlaceForward = WillPlace(2.1f, 0, 0);
                    break;
                case arrow.backward:
                     //Look future to get wallk backward
                     RobotPlaceBackward = WillPlace(-2.1f, 0, 0);
                    break;
                case arrow.right:
                     //Look future to get wallk right
                     RobotPlaceRight = WillPlace(0, 0, 2.1f);
                    break;
                case arrow.left:
                     //Look future to get wallk left
                     RobotPlaceLeft = WillPlace(0, 0, -2.1f);
                    break;
            }

            if (((RobotPlace[0] < ((car.Drive % 46) - 23) + 7) && (RobotPlace[0] > ((car.Drive % 46) - 23) - 4))  //in car length
                 && RobotPlace[1] > 5 && RobotPlace[1] < 10 && !Robot.IsJumping) //in car width & not jumping
            {
                Robot.IsCrashWithCar = true;
            }

            //!!!!!!!!!!!
            GL.glDisable(GL.GL_CULL_FACE);
            GL.glPopMatrix();
            GL.glRotatef(-180, 0, 1, 0);

            #region Reflaction to cube
            StartReflaction(-1, 1, -1);     //Reflaction to cube

            cube.Scale(xMaxOfCube - 0.002f, yMaxOfCube - 0.002f, zMaxOfCube - 0.002f);
            cube.Draw(-1);       //decrease 0.002 from each dimension of cube to avoid unexpected lines and -1 for mirrow efect
            GL.glDisable(GL.GL_CULL_FACE);
            GL.glPopMatrix();
            #endregion

            // really draw mirror 
            //( half-transparent ( see its color's alpha byte)))
            // in order to see reflected objects 
            //GL.glPushMatrix();
            //GL.glTranslatef(0, -4, 0);
            //GL.glDepthMask((byte)GL.GL_FALSE);
            //GL.glRotatef(90,0,1,0);
            //DrawMirror();
            //GL.glDepthMask((byte)GL.GL_TRUE);
            //GL.glPopMatrix();

            GL.glScalef(-1, 1, 1);//Some rotation for normal Cube direction
            GL.glRotatef(180, 0, 1, 0);

            #region Reflaction to Robot
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, ModelVievMatrixBeforeSpecificTransforms);
            StartReflaction(-1, 1, 1);  //Start Reflaction to Robot
            GL.glLoadIdentity();
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, CurrentRotationTraslation);
            GL.glLoadMatrixd(AccumulatedRotationsTraslations);
            GL.glMultMatrixd(CurrentRotationTraslation);
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, AccumulatedRotationsTraslations);
            GL.glLoadMatrixd(ModelVievMatrixBeforeSpecificTransforms);
            GL.glMultMatrixd(AccumulatedRotationsTraslations);



            //draw reflected Robot
            GL.glPushMatrix();
            RotateRobotByDirection();
            GL.glCullFace(GL.GL_BACK);
            GL.glCullFace(GL.GL_FRONT);
            GL.glDisable(GL.GL_CULL_FACE);
            GL.glRotatef(-Robot.WalkAngle, 0, 1, 0);
            Robot.Draw(false);
            GL.glPopMatrix();
            GL.glDisable(GL.GL_CULL_FACE);
            GL.glPopMatrix();


            //draw all Robot shadows in the mirror!!
            MakeShadow(ModelVievMatrixBeforeSpecificTransforms, CurrentRotationTraslation);
            DrawLight();   //draw reflected light 
            GL.glDisable(GL.GL_STENCIL_TEST);
            #endregion

            GL.glScalef(1, 1, -1);      //Some rotation for normal Cube direction
            GL.glRotatef(180, 0, 1, 0);
            DrawLight();    //draw actual light
            MakeShadow(ModelVievMatrixBeforeSpecificTransforms, CurrentRotationTraslation); //draw all Robot shadows


            car.Drive += 0.5f;    //for animation of car moving

            //draw car reflection
            StartReflaction(-1, 1, 1);
            car.Draw(false);

            //draw car reflection shadow on floor
            StartReflaction(-1, 1, 1);
            GL.glDisable(GL.GL_LIGHTING);
            GL.glPushMatrix();
            MakeShadowMatrix(floorCoordForShadow);
            GL.glMultMatrixf(cubeXform);
            car.Draw(true);
            GL.glPopMatrix();

            //draw car reflection shadow on back wall
            StartReflaction(-1, 1, 1);
            GL.glDisable(GL.GL_LIGHTING);
            GL.glPushMatrix();
            MakeShadowMatrix(backWallCoordForShadow);
            GL.glMultMatrixf(cubeXform);
            car.Draw(true);
            GL.glPopMatrix();

            //draw car reflection shadow on front wall
            StartReflaction(-1, 1, 1);
            GL.glDisable(GL.GL_LIGHTING);
            GL.glPushMatrix();
            MakeShadowMatrix(frontWallCoordForShadow);
            GL.glMultMatrixf(cubeXform);
            car.Draw(true);
            GL.glPopMatrix();
            GL.glDisable(GL.GL_STENCIL_TEST);
            
            //draw the Frame of mirror
            mirror.DrawFrame();

            //draw mirror again to cover car from being shown on the other side of mirror
            GL.glPushMatrix();
            GL.glTranslatef(0, -4, 0);
            GL.glRotatef(90, 0, 1, 0);
            mirror.Draw();
            GL.glPopMatrix();

            //draw car
            car.Draw(false);

            //draw shadow car on floor
            GL.glPushMatrix();
            MakeShadowMatrix(floorCoordForShadow);
            GL.glMultMatrixf(cubeXform);
            car.Draw(true);
            GL.glPopMatrix();

            //draw shadow car on back wall
            GL.glPushMatrix();
            MakeShadowMatrix(backWallCoordForShadow);
            GL.glMultMatrixf(cubeXform);
            car.Draw(true);
            GL.glPopMatrix();

            //draw shadow car on front wall
            GL.glPushMatrix();
            MakeShadowMatrix(frontWallCoordForShadow);
            GL.glMultMatrixf(cubeXform);
            car.Draw(true);
            GL.glPopMatrix();


            GL.glFlush();
            WGL.wglSwapBuffers(m_uint_DC);

        }

        void MakeShadow(double[] ModelVievMatrixBeforeSpecificTransforms, double[] CurrentRotationTraslation)
        {
            #region Shadow Robot floor
            GL.glPushMatrix();
            MakeShadowMatrix(floorCoordForShadow);
            GL.glMultMatrixf(cubeXform);

            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, ModelVievMatrixBeforeSpecificTransforms);
            GL.glLoadIdentity(); // make it identity matrix
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, CurrentRotationTraslation);
            GL.glLoadMatrixd(AccumulatedRotationsTraslations); //Global Matrix
            GL.glMultMatrixd(CurrentRotationTraslation);
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, AccumulatedRotationsTraslations);
            GL.glLoadMatrixd(ModelVievMatrixBeforeSpecificTransforms);
            GL.glMultMatrixd(AccumulatedRotationsTraslations);


            //draw Robot shadow on floor
            GL.glPushMatrix();
            RotateRobotByDirection();
            GL.glCullFace(GL.GL_BACK);
            GL.glCullFace(GL.GL_FRONT);
            GL.glDisable(GL.GL_CULL_FACE);
            GL.glDisable(GL.GL_LIGHTING);
            GL.glRotatef(-Robot.WalkAngle, 0, 1, 0);

            Robot.Draw(true);
            //GL.glRotatef(180, 0, 1, 0);

            GL.glPopMatrix();

            GL.glDisable(GL.GL_CULL_FACE);
            GL.glPopMatrix();
            GL.glPopMatrix();
            #endregion



            #region Shadow Robot back wall
            GL.glPushMatrix();
            MakeShadowMatrix(backWallCoordForShadow);
            GL.glMultMatrixf(cubeXform);

            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, ModelVievMatrixBeforeSpecificTransforms);
            GL.glLoadIdentity();
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, CurrentRotationTraslation);
            GL.glLoadMatrixd(AccumulatedRotationsTraslations);
            GL.glMultMatrixd(CurrentRotationTraslation);
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, AccumulatedRotationsTraslations);
            GL.glLoadMatrixd(ModelVievMatrixBeforeSpecificTransforms);
            GL.glMultMatrixd(AccumulatedRotationsTraslations);

            //draw Robot shadow on back wall
            GL.glPushMatrix();
            RotateRobotByDirection();
            GL.glCullFace(GL.GL_BACK);
            GL.glCullFace(GL.GL_FRONT);
            GL.glDisable(GL.GL_CULL_FACE);
            GL.glDisable(GL.GL_LIGHTING);
            GL.glRotatef(-Robot.WalkAngle, 0, 1, 0);
            Robot.Draw(true);
            GL.glPopMatrix();

            GL.glDisable(GL.GL_CULL_FACE);
            GL.glPopMatrix();
            GL.glPopMatrix();

            #endregion
            #region Shadow Robot front wall
            GL.glPushMatrix();
            MakeShadowMatrix(frontWallCoordForShadow);
            GL.glMultMatrixf(cubeXform);

            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, ModelVievMatrixBeforeSpecificTransforms);
            GL.glLoadIdentity();
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, CurrentRotationTraslation);
            GL.glLoadMatrixd(AccumulatedRotationsTraslations);
            GL.glMultMatrixd(CurrentRotationTraslation);
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, AccumulatedRotationsTraslations);
            GL.glLoadMatrixd(ModelVievMatrixBeforeSpecificTransforms);
            GL.glMultMatrixd(AccumulatedRotationsTraslations);

            //draw Robot shadow on front wall
            GL.glPushMatrix();
            RotateRobotByDirection();
            GL.glCullFace(GL.GL_BACK);
            GL.glCullFace(GL.GL_FRONT);
            GL.glDisable(GL.GL_CULL_FACE);
            GL.glDisable(GL.GL_LIGHTING);
            GL.glRotatef(-Robot.WalkAngle, 0, 1, 0);
            Robot.Draw(true);
            GL.glPopMatrix();

            GL.glDisable(GL.GL_CULL_FACE);
            GL.glPopMatrix();
            GL.glPopMatrix();
            #endregion

            #region Shadow Robot right wall
            GL.glPushMatrix();
            MakeShadowMatrix(rightWallCoordForShadow);
            GL.glMultMatrixf(cubeXform);

            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, ModelVievMatrixBeforeSpecificTransforms);
            GL.glLoadIdentity();
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, CurrentRotationTraslation);
            GL.glLoadMatrixd(AccumulatedRotationsTraslations);
            GL.glMultMatrixd(CurrentRotationTraslation);
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, AccumulatedRotationsTraslations);
            GL.glLoadMatrixd(ModelVievMatrixBeforeSpecificTransforms);
            GL.glMultMatrixd(AccumulatedRotationsTraslations);

            //draw Robot shadow on right wall
            GL.glPushMatrix();
            RotateRobotByDirection();
            GL.glCullFace(GL.GL_BACK);
            GL.glCullFace(GL.GL_FRONT);
            GL.glDisable(GL.GL_CULL_FACE);
            GL.glDisable(GL.GL_LIGHTING);
            GL.glRotatef(-Robot.WalkAngle, 0, 1, 0);
            Robot.Draw(true);
            GL.glPopMatrix();

            GL.glDisable(GL.GL_CULL_FACE);
            GL.glPopMatrix();
            GL.glPopMatrix();
            #endregion
            #region Shadow Robot left wall
            GL.glPushMatrix();
            MakeShadowMatrix(leftWallCoordForShadow);
            GL.glMultMatrixf(cubeXform);

            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, ModelVievMatrixBeforeSpecificTransforms);
            GL.glLoadIdentity();
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, CurrentRotationTraslation);
            GL.glLoadMatrixd(AccumulatedRotationsTraslations);
            GL.glMultMatrixd(CurrentRotationTraslation);
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, AccumulatedRotationsTraslations);
            GL.glLoadMatrixd(ModelVievMatrixBeforeSpecificTransforms);
            GL.glMultMatrixd(AccumulatedRotationsTraslations);

            //draw Robot shadow on left wall
            GL.glPushMatrix();
            RotateRobotByDirection();
            GL.glCullFace(GL.GL_BACK);
            GL.glCullFace(GL.GL_FRONT);
            GL.glDisable(GL.GL_CULL_FACE);
            GL.glDisable(GL.GL_LIGHTING);
            GL.glRotatef(-Robot.WalkAngle, 0, 1, 0);
            Robot.Draw(true);
            GL.glPopMatrix();

            GL.glDisable(GL.GL_CULL_FACE);
            GL.glPopMatrix();
            GL.glPopMatrix();
            #endregion

            #region Shadow Robot roof
            GL.glPushMatrix();
            MakeShadowMatrix(ceilingCoordForShadow);
            GL.glMultMatrixf(cubeXform);

            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, ModelVievMatrixBeforeSpecificTransforms);
            GL.glLoadIdentity();
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, CurrentRotationTraslation);
            GL.glLoadMatrixd(AccumulatedRotationsTraslations);
            GL.glMultMatrixd(CurrentRotationTraslation);
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, AccumulatedRotationsTraslations);
            GL.glLoadMatrixd(ModelVievMatrixBeforeSpecificTransforms);
            GL.glMultMatrixd(AccumulatedRotationsTraslations);

            //draw Robot shadow on roof
            GL.glPushMatrix();
            RotateRobotByDirection();
            GL.glCullFace(GL.GL_BACK);
            GL.glCullFace(GL.GL_FRONT);
            GL.glDisable(GL.GL_CULL_FACE);
            GL.glDisable(GL.GL_LIGHTING);
            GL.glRotatef(-Robot.WalkAngle, 0, 1, 0);
            Robot.Draw(true);
            GL.glPopMatrix();

            GL.glDisable(GL.GL_CULL_FACE);
            GL.glPopMatrix();
            GL.glPopMatrix();
            #endregion


        }

        void StartReflaction(int x, int y, int z)
        {
            GL.glEnable(GL.GL_BLEND);
            GL.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA);


            //only floor, draw only to STENCIL buffer
            GL.glEnable(GL.GL_STENCIL_TEST);
            GL.glStencilOp(GL.GL_REPLACE, GL.GL_REPLACE, GL.GL_REPLACE);
            GL.glStencilFunc(GL.GL_ALWAYS, 1, 0xFFFFFFFF); // draw floor always
            GL.glColorMask((byte)GL.GL_FALSE, (byte)GL.GL_FALSE, (byte)GL.GL_FALSE, (byte)GL.GL_FALSE);
            GL.glDisable(GL.GL_DEPTH_TEST);
            GL.glPushMatrix();
            GL.glRotated(90, 0, 1, 0);
            GL.glTranslatef(0, -4, 0);
            mirror.Draw();
            GL.glPopMatrix();
            GL.glColorMask((byte)GL.GL_TRUE, (byte)GL.GL_TRUE, (byte)GL.GL_TRUE, (byte)GL.GL_TRUE);
            GL.glEnable(GL.GL_DEPTH_TEST);

            // reflection is drawn only where STENCIL buffer value equal to 1
            GL.glStencilFunc(GL.GL_EQUAL, 1, 0xFFFFFFFF);
            GL.glStencilOp(GL.GL_KEEP, GL.GL_KEEP, GL.GL_KEEP);

            GL.glEnable(GL.GL_STENCIL_TEST);

            // draw reflected scene
            GL.glPushMatrix();
            GL.glScalef(x, y, z); //swap on Z axis
        }

        public uint[] Textures = new uint[9];
        void GenerateTextures()
        {
            GL.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA);
            GL.glGenTextures(9, Textures);
            string[] imagesName ={"kalahk.jpg","divingsuit.jpg","Wall_Texture_1_by_Insan_Stock.jpg",
                                     "stock-photo-old-brick-wall-texture-66007105.jpg",
                                     "stock-photo-old-brick-wall-texture-73022833.jpg",
                                     "Concrete_Basement_Wall_Texture_by_FantasyStock.jpg",
                                     "top.bmp","stock-photo-185015-floor-texture-6.jpg","Scared_face.jpg"};

            GL.glGenTextures(6, Textures);

            //monster = new Milkshape.Character("kalahk.ms3d");
            //diver = new Milkshape.Character("diver.ms3d");

            //skip on the first two images
            for (int i = 2; i < 9; i++)
            {
                Bitmap image = new Bitmap(imagesName[i]);
                image.RotateFlip(RotateFlipType.RotateNoneFlipY); //Y axis in Windows is directed downwards, while in OpenGL-upwards
                System.Drawing.Imaging.BitmapData bitmapdata;
                Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

                bitmapdata = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                GL.glBindTexture(GL.GL_TEXTURE_2D, Textures[i]);
                //2D for XYZ
                GL.glTexImage2D(GL.GL_TEXTURE_2D, 0, (int)GL.GL_RGB8, image.Width, image.Height,
                                                              0, GL.GL_BGR_EXT, GL.GL_UNSIGNED_byte, bitmapdata.Scan0);
                GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, (int)GL.GL_LINEAR);
                if (i == 5)
                    GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, (int)GL.GL_SPHERE_MAP);
                else
                    GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, (int)GL.GL_LINEAR);

                image.UnlockBits(bitmapdata);
                image.Dispose();
            }
        }

        protected virtual void InitializeGL()
        {
            m_uint_HWND = (uint)panel.Handle.ToInt32();
            m_uint_DC = WGL.GetDC(m_uint_HWND);

            // Not doing the following WGL.wglSwapBuffers() on the DC will
            // result in a failure to subsequently create the RC.
            WGL.wglSwapBuffers(m_uint_DC);

            WGL.PIXELFORMATDESCRIPTOR pfd = new WGL.PIXELFORMATDESCRIPTOR();
            WGL.ZeroPixelDescriptor(ref pfd);
            pfd.nVersion = 1;
            pfd.dwFlags = (WGL.PFD_DRAW_TO_WINDOW | WGL.PFD_SUPPORT_OPENGL | WGL.PFD_DOUBLEBUFFER);
            pfd.iPixelType = (byte)(WGL.PFD_TYPE_RGBA);
            pfd.cColorBits = 32;
            pfd.cDepthBits = 32;
            pfd.iLayerType = (byte)(WGL.PFD_MAIN_PLANE);

            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            //for Stencil support 

            pfd.cStencilBits = 32;

            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            int pixelFormatIndex = 0;
            pixelFormatIndex = WGL.ChoosePixelFormat(m_uint_DC, ref pfd);
            if (pixelFormatIndex == 0)
            {
                MessageBox.Show("Unable to retrieve pixel format");
                return;
            }

            if (WGL.SetPixelFormat(m_uint_DC, pixelFormatIndex, ref pfd) == 0)
            {
                MessageBox.Show("Unable to set pixel format");
                return;
            }
            //Create rendering context
            m_uint_RC = WGL.wglCreateContext(m_uint_DC);
            if (m_uint_RC == 0)
            {
                MessageBox.Show("Unable to get rendering context");
                return;
            }
            if (WGL.wglMakeCurrent(m_uint_DC, m_uint_RC) == 0)
            {
                MessageBox.Show("Unable to make rendering context current");
                return;
            }


            initRenderingGL();
        }

        public void OnResize()
        {
            Width = panel.Width;
            Height = panel.Height;

            GL.glViewport(0, 0, Width, Height);
            Draw();
        }

        protected virtual void initRenderingGL()
        {
            if (m_uint_DC == 0 || m_uint_RC == 0)
                return;
            if (this.Width == 0 || this.Height == 0)
                return;

            GL.glShadeModel(GL.GL_SMOOTH);
            GL.glClearColor(0.0f, 0.0f, 0.0f, 0.5f);
            GL.glClearDepth(1.0f);


            GL.glEnable(GL.GL_LIGHT0);
            GL.glEnable(GL.GL_COLOR_MATERIAL);
            GL.glColorMaterial(GL.GL_FRONT_AND_BACK, GL.GL_AMBIENT_AND_DIFFUSE);

            GL.glEnable(GL.GL_DEPTH_TEST);
            GL.glDepthFunc(GL.GL_LEQUAL);
            GL.glHint(GL.GL_PERSPECTIVE_CORRECTION_Hint, GL.GL_NICEST);

            GL.glViewport(0, 0, this.Width, this.Height);
            GL.glMatrixMode(GL.GL_PROJECTION);
            GL.glLoadIdentity();

            //nice 3D
            GLU.gluPerspective(45.0, 1.0, 0.4, 100.0);


            GL.glMatrixMode(GL.GL_MODELVIEW);
            GL.glLoadIdentity();

            //save the current MODELVIEW Matrix (now it is Identity)
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, AccumulatedRotationsTraslations);
            GenerateTextures();
        }

        // Reduces a normal vector specified as a set of three coordinates,
        // to a unit normal vector of length one.
        void ReduceToUnit(float[] vector)
        {
            float length;

            // Calculate the length of the vector		
            length = (float)Math.Sqrt((vector[0] * vector[0]) +
                                (vector[1] * vector[1]) +
                                (vector[2] * vector[2]));

            // Keep the program from blowing up by providing an exceptable
            // value for vectors that may calculated too close to zero.
            if (length == 0.0f)
                length = 1.0f;

            // Dividing each element by the length will result in a
            // unit normal vector.
            vector[0] /= length;
            vector[1] /= length;
            vector[2] /= length;
        }
        const int x = 0;
        const int y = 1;
        const int z = 2;

        // Points p1, p2, & p3 specified in counter clock-wise order
        void calcNormal(float[,] v, float[] outp)
        {
            float[] v1 = new float[3];
            float[] v2 = new float[3];

            // Calculate two vectors from the three points
            v1[x] = v[0, x] - v[1, x];
            v1[y] = v[0, y] - v[1, y];
            v1[z] = v[0, z] - v[1, z];

            v2[x] = v[1, x] - v[2, x];
            v2[y] = v[1, y] - v[2, y];
            v2[z] = v[1, z] - v[2, z];

            // Take the cross product of the two vectors to get
            // the normal vector which will be stored in out
            outp[x] = v1[y] * v2[z] - v1[z] * v2[y];
            outp[y] = v1[z] * v2[x] - v1[x] * v2[z];
            outp[z] = v1[x] * v2[y] - v1[y] * v2[x];

            // Normalize the vector (shorten length to one)
            ReduceToUnit(outp);
        }

        float[] cubeXform = new float[16];

        // Creates a shadow projection matrix out of the plane equation
        // coefficients and the position of the light. The return value is stored
        // in cubeXform[,]

        void MakeShadowMatrix(float[,] points)
        {
            float[] planeCoeff = new float[4];
            float dot;

            // Find the plane equation coefficients
            // Find the first three coefficients the same way we
            // find a normal.
            calcNormal(points, planeCoeff);

            // Find the last coefficient by back substitutions
            planeCoeff[3] = -(
                (planeCoeff[0] * points[2, 0]) + (planeCoeff[1] * points[2, 1]) +
                (planeCoeff[2] * points[2, 2]));


            // Dot product of plane and light position
            dot = planeCoeff[0] * LightPosition[0] +
                    planeCoeff[1] * LightPosition[1] +
                    planeCoeff[2] * LightPosition[2] +
                    planeCoeff[3];

            // Now do the projection
            // First column
            cubeXform[0] = dot - LightPosition[0] * planeCoeff[0];
            cubeXform[4] = 0.0f - LightPosition[0] * planeCoeff[1];
            cubeXform[8] = 0.0f - LightPosition[0] * planeCoeff[2];
            cubeXform[12] = 0.0f - LightPosition[0] * planeCoeff[3];

            // Second column
            cubeXform[1] = 0.0f - LightPosition[1] * planeCoeff[0];
            cubeXform[5] = dot - LightPosition[1] * planeCoeff[1];
            cubeXform[9] = 0.0f - LightPosition[1] * planeCoeff[2];
            cubeXform[13] = 0.0f - LightPosition[1] * planeCoeff[3];

            // Third Column
            cubeXform[2] = 0.0f - LightPosition[2] * planeCoeff[0];
            cubeXform[6] = 0.0f - LightPosition[2] * planeCoeff[1];
            cubeXform[10] = dot - LightPosition[2] * planeCoeff[2];
            cubeXform[14] = 0.0f - LightPosition[2] * planeCoeff[3];

            // Fourth Column
            cubeXform[3] = 0.0f - LightPosition[3] * planeCoeff[0];
            cubeXform[7] = 0.0f - LightPosition[3] * planeCoeff[1];
            cubeXform[11] = 0.0f - LightPosition[3] * planeCoeff[2];
            cubeXform[15] = dot - LightPosition[3] * planeCoeff[3];
        }


    }
}


