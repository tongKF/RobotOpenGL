using System;
using System.Collections.Generic;
using System.Windows.Forms;

//2
using System.Drawing;

namespace OpenGL
{
    class cOGL
    {
        Control p;
        int Width;
        int Height;

        GLUquadric obj;

        public cOGL(Control pb)
        {
            p=pb;
            Width = p.Width;
            Height = p.Height; 
            InitializeGL();
            obj = GLU.gluNewQuadric(); //!!!
            PrepareLists();
        }

        ~cOGL()
        {
            GLU.gluDeleteQuadric(obj); //!!!
            WGL.wglDeleteContext(m_uint_RC);
        }

		uint m_uint_HWND = 0;

        public uint HWND
		{
			get{ return m_uint_HWND; }
		}
		
        uint m_uint_DC   = 0;

        public uint DC
		{
			get{ return m_uint_DC;}
		}
		uint m_uint_RC   = 0;

        public uint RC
		{
			get{ return m_uint_RC; }
		}


        void DrawOldAxes()
        {
	        //for this time
	        //Lights positioning is here!!!
	        float []pos=new float[4]; 
	        pos[0] = 10; pos[1] = 10; pos[2] = 10; pos[3] = 1;
            GL.glLightfv ( GL.GL_LIGHT0,  GL.GL_POSITION, pos);
            GL.glDisable( GL.GL_LIGHTING);

	        //INITIAL axes
            GL.glEnable ( GL.GL_LINE_STIPPLE);
            GL.glLineStipple (1, 0xFF00);  //  dotted   
	        GL.glBegin( GL.GL_LINES);	
	            //x  RED
	            GL.glColor3f(1.0f,0.0f,0.0f);						
		        GL.glVertex3f( -3.0f, 0.0f, 0.0f);	
		        GL.glVertex3f( 3.0f, 0.0f, 0.0f);	
	            //y  GREEN 
	            GL.glColor3f(0.0f,1.0f,0.0f);						
		        GL.glVertex3f( 0.0f, -3.0f, 0.0f);	
		        GL.glVertex3f( 0.0f, 3.0f, 0.0f);	
	            //z  BLUE
	            GL.glColor3f(0.0f,0.0f,1.0f);						
		        GL.glVertex3f( 0.0f, 0.0f, -3.0f);	
		        GL.glVertex3f( 0.0f, 0.0f, 3.0f);	
            GL.glEnd();
            GL.glDisable ( GL.GL_LINE_STIPPLE);
        }
        void DrawAxes()
        {
            GL.glBegin( GL.GL_LINES);
            //x  RED
            GL.glColor3f(1.0f, 0.0f, 0.0f);
            GL.glVertex3f(-3.0f, 0.0f, 0.0f);
            GL.glVertex3f(3.0f, 0.0f, 0.0f);
            //y  GREEN 
            GL.glColor3f(0.0f, 1.0f, 0.0f);
            GL.glVertex3f(0.0f, -3.0f, 0.0f);
            GL.glVertex3f(0.0f, 3.0f, 0.0f);
            //z  BLUE
            GL.glColor3f(0.0f, 0.0f, 1.0f);
            GL.glVertex3f(0.0f, 0.0f, -3.0f);
            GL.glVertex3f(0.0f, 0.0f, 3.0f);
            GL.glEnd();
        }

        
        void DrawFigures()
        {
            GL.glEnable(GL.GL_COLOR_MATERIAL);
            GL.glEnable(GL.GL_LIGHT0);
            GL.glEnable(GL.GL_LIGHTING);
            
	        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            GL.glRotatef (ROBOT_angle, 0, 0, 1);
	        GL.glCallList (ROBOT_LIST);
	        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            // saw 
            GL.glLineStipple (1, 0x1243);
            GL.glLineWidth (3);
            GL.glEnable (GL.GL_LINE_STIPPLE);
               
            GL.glColor3f (1, 1, 0 );//yellow
            GL.glBegin (GL.GL_LINES);
            float angle;
            for(int i = 0;i<=9; i++)
            {
		        angle = alpha + i * 6.283f / 10;
                GL.glVertex3d( 0.5f * r * Math.Cos(angle), 0.5f * r * Math.Sin(angle), 0.01f);
                GL.glVertex3d( 1.5f * r * Math.Cos(angle + 0.6), 1.5f * r * Math.Sin(angle + 0.6), 0.01f);
	        }
           GL.glEnd();
           GL.glLineWidth (1);
           GL.glDisable (GL.GL_LINE_STIPPLE);
        }

        public float[] ScrollValue = new float[10];
        public float zShift = 0.0f;
        public float yShift = 0.0f;
        public float xShift = 0.0f;
        public float zAngle = 0.0f;
        public float yAngle = 0.0f;
        public float xAngle = 0.0f;
        public int intOptionC = 0;
        double[] AccumulatedRotationsTraslations = new double[16];
        
        public void Draw()
        {
            if (m_uint_DC == 0 || m_uint_RC == 0)
                return;

            GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT);
            
            GL.glLoadIdentity();								

            // not trivial
            double []ModelVievMatrixBeforeSpecificTransforms=new double[16];
            double []CurrentRotationTraslation=new double[16];
                     
            GLU.gluLookAt (ScrollValue[0], ScrollValue[1], ScrollValue[2], 
	                   ScrollValue[3], ScrollValue[4], ScrollValue[5],
		               ScrollValue[6],ScrollValue[7],ScrollValue[8]);
            GL.glTranslatef(0.0f, 0.0f, -1.0f);
                     
            DrawOldAxes();

            //save current ModelView Matrix values
            //in ModelVievMatrixBeforeSpecificTransforms array
            //ModelView Matrix ========>>>>>> ModelVievMatrixBeforeSpecificTransforms
            GL.glGetDoublev (GL.GL_MODELVIEW_MATRIX, ModelVievMatrixBeforeSpecificTransforms);
            //ModelView Matrix was saved, so
            GL.glLoadIdentity(); // make it identity matrix
                     
            //make transformation in accordance to KeyCode
            float delta;
            if (intOptionC != 0)
            {
                delta = 5.0f * Math.Abs(intOptionC) / intOptionC; // signed 5

                switch (Math.Abs(intOptionC))
                {
                    case 1:
                        GL.glRotatef(delta, 1, 0, 0);
                        break;
                    case 2:
                        GL.glRotatef(delta, 0, 1, 0);
                        break;
                    case 3:
                        GL.glRotatef(delta, 0, 0, 1);
                        break;
                    case 4:
                        GL.glTranslatef(delta / 20, 0, 0);
                        break;
                    case 5:
                        GL.glTranslatef(0, delta / 20, 0);
                        break;
                    case 6:
                        GL.glTranslatef(0, 0, delta / 20);
                        break;
                }
            }
            //as result - the ModelView Matrix now is pure representation
            //of KeyCode transform and only it !!!

            //save current ModelView Matrix values
            //in CurrentRotationTraslation array
            //ModelView Matrix =======>>>>>>> CurrentRotationTraslation
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, CurrentRotationTraslation);

            //The GL.glLoadMatrix function replaces the current matrix with
            //the one specified in its argument.
            //The current matrix is the
            //projection matrix, modelview matrix, or texture matrix,
            //determined by the current matrix mode (now is ModelView mode)
            GL.glLoadMatrixd(AccumulatedRotationsTraslations); //Global Matrix

            //The GL.glMultMatrix function multiplies the current matrix by
            //the one specified in its argument.
            //That is, if M is the current matrix and T is the matrix passed to
            //GL.glMultMatrix, then M is replaced with M • T
            GL.glMultMatrixd(CurrentRotationTraslation);

            //save the matrix product in AccumulatedRotationsTraslations
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, AccumulatedRotationsTraslations);

            //replace ModelViev Matrix with stored ModelVievMatrixBeforeSpecificTransforms
            GL.glLoadMatrixd(ModelVievMatrixBeforeSpecificTransforms);
            //multiply it by KeyCode defined AccumulatedRotationsTraslations matrix
            GL.glMultMatrixd(AccumulatedRotationsTraslations);

            DrawAxes();
            
            DrawFigures();

            GL.glFlush();

            WGL.wglSwapBuffers(m_uint_DC);

        }

		protected virtual void InitializeGL()
		{
			m_uint_HWND = (uint)p.Handle.ToInt32();
			m_uint_DC   = WGL.GetDC(m_uint_HWND);

            // Not doing the following WGL.wglSwapBuffers() on the DC will
			// result in a failure to subsequently create the RC.
			WGL.wglSwapBuffers(m_uint_DC);

			WGL.PIXELFORMATDESCRIPTOR pfd = new WGL.PIXELFORMATDESCRIPTOR();
			WGL.ZeroPixelDescriptor(ref pfd);
			pfd.nVersion        = 1; 
			pfd.dwFlags         = (WGL.PFD_DRAW_TO_WINDOW |  WGL.PFD_SUPPORT_OPENGL |  WGL.PFD_DOUBLEBUFFER); 
			pfd.iPixelType      = (byte)(WGL.PFD_TYPE_RGBA);
			pfd.cColorBits      = 32;
			pfd.cDepthBits      = 32;
			pfd.iLayerType      = (byte)(WGL.PFD_MAIN_PLANE);

			int pixelFormatIndex = 0;
			pixelFormatIndex = WGL.ChoosePixelFormat(m_uint_DC, ref pfd);
			if(pixelFormatIndex == 0)
			{
				MessageBox.Show("Unable to retrieve pixel format");
				return;
			}

			if(WGL.SetPixelFormat(m_uint_DC,pixelFormatIndex,ref pfd) == 0)
			{
				MessageBox.Show("Unable to set pixel format");
				return;
			}
			//Create rendering context
			m_uint_RC = WGL.wglCreateContext(m_uint_DC);
			if(m_uint_RC == 0)
			{
				MessageBox.Show("Unable to get rendering context");
				return;
			}
			if(WGL.wglMakeCurrent(m_uint_DC,m_uint_RC) == 0)
			{
				MessageBox.Show("Unable to make rendering context current");
				return;
			}


            initRenderingGL();
        }

        public void OnResize()
        {
            Width = p.Width;
            Height = p.Height;
            GL.glViewport(0, 0, Width, Height);
            Draw();
        }

        protected virtual void initRenderingGL()
		{
			if(m_uint_DC == 0 || m_uint_RC == 0)
				return;
			if(this.Width == 0 || this.Height == 0)
				return;
            GL.glClearColor(0.5f, 0.5f, 0.5f, 0.0f);
            GL.glEnable(GL.GL_DEPTH_TEST);
            GL.glDepthFunc(GL.GL_LEQUAL);

            GL.glViewport(0, 0, this.Width, this.Height);
			GL.glMatrixMode ( GL.GL_PROJECTION );
			GL.glLoadIdentity();
            
            //nice 3D
			GLU.gluPerspective( 45.0,  1.0, 0.4,  100.0);

            
            GL.glMatrixMode ( GL.GL_MODELVIEW );
			GL.glLoadIdentity();

            //save the current MODELVIEW Matrix (now it is Identity)
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, AccumulatedRotationsTraslations);
		}



            public float ARM_angle;
            public float SHOULDER_angle;
            public float ROBOT_angle;
            public float alpha;




            uint ROBOT_LIST, ARM_LIST, SHOULDER_LIST;
            float r;

            void PrepareLists()
            {
                float ARM_length, SHOULDER_length;
                ARM_length = 2;
                ARM_angle = -45;
                SHOULDER_length = 2.5f;
                SHOULDER_angle = 10;
                ROBOT_angle = 45;
                r = 0.3f;

                ROBOT_LIST = GL.glGenLists(3);
                ARM_LIST = ROBOT_LIST + 1;
                SHOULDER_LIST = ROBOT_LIST + 2;

                GL.glPushMatrix();
                GL.glNewList(ARM_LIST, GL.GL_COMPILE);
                       //cone
                       GL.glColor3f (0.5f, 0, 0);
                       GLU.gluCylinder (obj, r, 0, ARM_length, 20, 20);
                       GL.glTranslated (0, 0, ARM_length);
                       //internal disk
                       GL.glColor3f (1, 1, 0);
                       GLU.gluDisk (obj, 0, r * 0.5, 20, 20);
                       //external disk
                       GL.glColor3f (1, 0, 0);
                       GLU.gluDisk (obj, r * 0.5, r * 1.5, 20, 20);
                GL.glEndList();
             GL.glPopMatrix();
             
             GL.glPushMatrix();
                GL.glNewList( SHOULDER_LIST, GL.GL_COMPILE);
                       GL.glColor3f( 0, 0.5f, 0);
                       GLU.gluCylinder (obj, r, r, SHOULDER_length, 20, 20);
                       GL.glTranslated (0, 0, SHOULDER_length);
                       GL.glColor3f (0, 1, 0);
                       GLU.gluSphere (obj, r * 1.2, 20, 20);
                GL.glEndList();
             GL.glPopMatrix();

             CreateRobotList();
            }

            public void CreateRobotList()
            {

                GL.glPushMatrix();
                //
                // hierarchical list
                //
                GL.glNewList(ROBOT_LIST, GL.GL_COMPILE);
            	       
	                   // BASE : no rotations!!! Angle will be implemented in the CALL routine
	                   //                   before call to CreateRobotList()
                GL.glColor3f(0, 0, 0.5f);
                       GLU.gluCylinder (obj, 3 * r, 3 * r, r * 1.2, 40, 20);
                       GL.glTranslated( 0, 0, r * 1.2);
                       GLU.gluDisk (obj, 0, 3 * r, 40, 20);
                       GL.glColor3f (0, 0, 1);
                       GLU.gluSphere (obj, r * 1.2, 20, 20);
                       // end base

                       // transformations
                       GL.glRotatef (SHOULDER_angle, 1, 0, 0);

                       // call SHOULDER 
                       GL.glCallList (SHOULDER_LIST);
                       
                       // transformations
		               //no need in glTranslated 0, 0, SHOULDER_length
                       //it is located properly now !!!
                       GL.glRotatef (ARM_angle, 1, 0, 0);
                       
                       // call ARM  
		               GL.glCallList (ARM_LIST);
                GL.glEndList();
             GL.glPopMatrix();
         }

    
    }

}


