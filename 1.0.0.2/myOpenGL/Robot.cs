using System;
using System.Collections.Generic;
using System.Text;

namespace OpenGL
{
    class Robot : TransformableObject
    {
        Milkshape.Character gun1, gun2, sword;
        GLUquadric obj;
        uint Robot_LIST, ARM_LIST, SHOULDER_LIST, HAND_LIST, BODY_LIST, HEAD_LIST, LEG_UP_LIST, LEG_DOWN_LIST;
        uint Robot_SHADOW_LIST, ARM_SHADOW_LIST, SHOULDER_SHADOW_LIST, HAND_SHADOW_LIST, BODY_SHADOW_LIST, HEAD_SHADOW_LIST, LEG_UP_SHADOW_LIST, LEG_DOWN_SHADOW_LIST;

        public float WalkAngle = 0;
        public float WeaponPlace = 0;
        public float ShootDist = 0;
        public float Jump = 0;
        public float Angle = 0, AngleCrash = 0, AngleWeapon3 = 0;
        //indicates weapon selected index
        public int WeaponIndex = 0;
        public bool IsMoving = false;
        public bool IsCrashWithCar = false;
        public bool IsJumping = false;
        public bool IsDoingSalta = false;


        public float LegLeftUpAngle = 0.0f;
        public float LegRightUpAngle = 0.0f;
        public float LegLeftDownAngle = 0.0f;
        public float LegRightDownAngle = 0.0f;
        public float ShoulderLeftAngle = 0;
        public float ShoulderRightAngle = 0;
        public float ArmLeftAngle = 70;
        public float ArmRightAngle = 70;


        // indicates radius
        float radius;
        float legUpLength = 1.2f;
        float legDownLength = 1.2f;
        float bodyLength = 2;
        float armLength = 1.3f;
        float shoulderLength = 1.1f;
        uint  headTexture;

        public Robot(uint texture)
        {
            this.headTexture = texture;
            gun1 = new Milkshape.Character("mauser_red9.ms3d");
            gun2 = new Milkshape.Character("hk-mp7.ms3d");
            sword = new Milkshape.Character("katana.ms3d");
            obj = GLU.gluNewQuadric(); //!!!
        }

        ~Robot()
        {
            GLU.gluDeleteQuadric(obj); //!!!
        }
        public void Draw(bool isForShades)
        {
            GL.glPushMatrix();
            GL.glTranslatef(0, -7, 0);
            //adapt Robot size to models 
            GL.glScalef(0.5f, 0.5f, 0.5f);


            GL.glTranslatef(0, Jump * 1.3f, 0);
            GL.glRotatef(Angle, 0, 0, 1);

            GL.glRotatef(AngleCrash / 4, 0, 0, 1);
            GL.glRotatef(AngleCrash, 1, 0, 0);
            GL.glRotatef(-AngleCrash * 2, 0, 1, 0);

            GL.glRotatef(AngleWeapon3, 0, 1, 0);
            GL.glTranslatef(0, -Jump * 1.3f, 0);
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            GL.glTranslatef(0, 3.7f, 0);

            if (!isForShades)
            {
                GL.glEnable(GL.GL_LIGHTING);
                GL.glColor3f(0.5f, 0.5f, 0.9f);
                GL.glCallList(Robot_LIST);
            }
            else
            {
                GL.glDisable(GL.GL_LIGHTING);
                GL.glColor3f(0, 0, 0);
                GL.glCallList(Robot_SHADOW_LIST);
            }

            GL.glPopMatrix();
        }

        public void PrepareAndDraw()
        {
            Prepare();
            Draw();

        }

        public void DrawLeg(bool side)
        {
            GL.glPushMatrix();
            if (side == false)
                GL.glRotatef(LegLeftUpAngle, 0, 1, 0);
            else
                GL.glRotatef(LegRightUpAngle, 0, 1, 0);
            GLU.gluSphere(obj, radius * 1.7, 20, 20);
            GL.glCallList(LEG_UP_LIST);
            if (side == false)
                if (WeaponIndex == 1 || WeaponIndex == 2)
                    GL.glRotatef(LegLeftDownAngle, 0, 1, 0);
                else
                {
                    GL.glScalef(-1, 1, 1);
                    GL.glRotatef(LegLeftDownAngle, 0, 1, 0);
                    GL.glScalef(1, 1, 1);
                }
            else
                if (WeaponIndex == 1 || WeaponIndex == 2)
                    GL.glRotatef(LegRightDownAngle, 0, 1, 0);
                else
                {
                    GL.glScalef(-1, 1, 1);
                    GL.glRotatef(LegRightDownAngle, 0, 1, 0);
                    GL.glScalef(1, 1, 1);
                }
            GL.glCallList(LEG_DOWN_LIST);
            GL.glPopMatrix();


        }

        public void DrawHand(bool side)
        {
            GL.glPushMatrix();
            GL.glTranslated(0, 0, 0.23f);
            GL.glPushMatrix();
            if (side == false)
            {
                GL.glTranslatef(0, 0.6f, 0);
                if (WeaponIndex == 1 || WeaponIndex == 2)
                {
                    GL.glRotatef(ShoulderRightAngle / 10, 0, 0, 1);
                    GL.glRotatef(WeaponPlace, 0, 0, 1);//hand Move from fire
                }
                else
                    GL.glRotatef(ShoulderRightAngle, 0, 0, 1);
                GL.glTranslatef(0, -0.6f, 0);
            }
            else
            {
                GL.glTranslatef(0, 0.6f, 0);
                if (WeaponIndex == 1 || WeaponIndex == 2)
                {
                    GL.glRotatef(-ShoulderRightAngle / 10, 0, 0, 1);
                    GL.glRotatef(WeaponPlace, 0, 0, 1);//hand Move from fire
                }
                else
                    GL.glRotatef(ShoulderRightAngle, 0, 0, 1);
                GL.glTranslatef(0, -0.6f, 0);
            }

            GL.glTranslatef(0, 0.6f, 0.55f);
            GLU.gluCylinder(obj, radius, radius, radius * 1.2, 40, 20);
            GL.glTranslated(0, 0, radius * 1.2);
            GLU.gluDisk(obj, 0, radius, 40, 20);
            GLU.gluSphere(obj, radius * 1.2, 20, 20);
            GL.glTranslatef(0, -0.6f, -0.55f);

            // build Robot shoulder 
            GL.glCallList(SHOULDER_LIST);
            GL.glRotatef(ArmLeftAngle, 1, 0, 0);

            //Move down hand with weapon
            if (WeaponIndex == 2)
            {
                GL.glRotatef(-WeaponPlace, 0, 1, 0);
                GL.glRotatef(WeaponPlace, 0, 0, 1);
            }

            // build Robot arm   
            GL.glCallList(ARM_LIST);
            GL.glPopMatrix();

            if (IsMoving == false) //do not see bullets when walking
            {
                GL.glPushMatrix();
                if (WeaponIndex == 2)
                {
                    GL.glTranslatef(0.2f, 0.2f, 1.4f);
                    GL.glRotatef(-8, 0, 1, 0);
                }
                else//WeaponIndex 1
                {
                    GL.glTranslatef(2.2f, 1.4f, 1.3f);
                    GL.glRotatef(-8, 0, 1, 0);
                }

                GL.glRotatef(90, 0, 1, 0);
                GL.glTranslatef(0, 0, ShootDist);
                if (WeaponIndex == 1)
                {
                    //draw bullet
                    radius = radius * 2.2f;
                    GL.glColor3f(0.5f, 0.2f, 0);
                    GLU.gluCylinder(obj, radius / 4.5f, radius / 4.5f, radius / 2, 40, 20);
                    GLU.gluDisk(obj, 0, radius / 4.5f, 20, 20);
                    GL.glTranslatef(0, 0, radius / 2);
                    GLU.gluCylinder(obj, radius / 4.5f, 0, radius / 4, 40, 20);
                    //return to default
                    GL.glColor3f(0.5f, 0.5f, 0.9f);
                    radius = radius / 2.2f;
                }
                else if (WeaponIndex == 2)
                {
                    //draw bullet
                    GL.glColor3f(0.5f, 0.2f, 0);
                    GLU.gluCylinder(obj, radius / 1.3f, radius / 1.3f, radius * 7, 40, 20);
                    GLU.gluDisk(obj, 0, radius / 1.3f, 20, 20);
                    GL.glTranslatef(0, 0, radius * 7);
                    GLU.gluCylinder(obj, radius / 1.3f, 0, radius * 5, 40, 20);
                    GL.glColor3f(0.5f, 0.5f, 0.9f);
                }
                GL.glPopMatrix();
            }
            GL.glPopMatrix();
        }

        void DrawDynamicHand()
        {
            switch (WeaponIndex)
            {
                case 0:
                    GL.glPushMatrix();
                    GL.glNewList(SHOULDER_LIST, GL.GL_COMPILE);
                    //shoulder
                    GL.glTranslatef(0, 0.6f, 0);
                    GL.glTranslated(0, 0, shoulderLength - 0.55);
                    GL.glRotatef(55, 1, 0, 0);
                    GLU.gluCylinder(obj, radius, radius, shoulderLength, 20, 20);
                    GL.glTranslated(0, 0, shoulderLength);
                    GLU.gluSphere(obj, 1.3 * radius, 20, 20);
                    GL.glEndList();
                    GL.glPopMatrix();

                    GL.glPushMatrix();
                    GL.glNewList(ARM_LIST, GL.GL_COMPILE);
                    //arm
                    GL.glRotatef(-30, 1, 0, 0);
                    GLU.gluCylinder(obj, radius, radius, armLength, 20, 20);
                    GL.glTranslated(0, 0, armLength);
                    GLU.gluSphere(obj, radius * 2.2, 20, 20);

                    GL.glEndList();
                    GL.glPopMatrix();
                    break;
                case 1:
                    GL.glPushMatrix();
                    GL.glNewList(SHOULDER_LIST, GL.GL_COMPILE);
                    //shoulder
                    GL.glTranslatef(0, 0.6f, 0);
                    GL.glTranslated(0, 0, shoulderLength - 0.55);
                    GL.glRotatef(65, 0, 1, 0);
                    GLU.gluCylinder(obj, radius, radius, shoulderLength, 20, 20);
                    GL.glTranslated(0, 0, shoulderLength);
                    GLU.gluSphere(obj, 1.3 * radius, 20, 20);
                    GL.glEndList();
                    GL.glPopMatrix();

                    GL.glPushMatrix();
                    GL.glNewList(ARM_LIST, GL.GL_COMPILE);
                    //arm
                    GL.glRotatef(-80, 1, 0, 0);
                    GL.glRotatef(20, 0, 1, 0);
                    GLU.gluCylinder(obj, radius, radius, armLength, 20, 20);
                    GL.glTranslated(0, 0, armLength);
                    GLU.gluSphere(obj, radius * 2.2, 20, 20);

                    //gun1
                    GL.glTranslatef(0, 0.6f, 0);
                    GL.glScalef(1.5f, 1.5f, 1.5f);
                    gun1.DrawModel(true, 1);
                    break;
                case 2:
                    GL.glPushMatrix();
                    GL.glNewList(SHOULDER_LIST, GL.GL_COMPILE);
                    //shoulder
                    GL.glTranslatef(0, 0.6f, 0);
                    GL.glTranslated(0, 0, shoulderLength - 0.55);
                    GL.glRotatef(65, 1, 0, 0);
                    GLU.gluCylinder(obj, radius, radius, shoulderLength, 20, 20);
                    GL.glTranslated(0, 0, shoulderLength);
                    GLU.gluSphere(obj, 1.3 * radius, 20, 20);
                    GL.glEndList();
                    GL.glPopMatrix();

                    GL.glPushMatrix();
                    GL.glNewList(ARM_LIST, GL.GL_COMPILE);
                    //arm
                    GL.glRotatef(-80, 1, 0, 0);
                    GL.glRotatef(90, 0, 1, 0);
                    GLU.gluCylinder(obj, radius, radius, armLength, 20, 20);
                    GL.glTranslated(0, 0, armLength);
                    GLU.gluSphere(obj, radius * 2.2, 20, 20);

                    //gun2
                    GL.glRotatef(-30, 0, 0, 1);
                    GL.glTranslatef(0, 0.6f, 0);
                    GL.glScalef(1.3f, 1.3f, 1.3f);
                    gun2.DrawModel(true, 1);
                    break;
                case 3:
                    GL.glPushMatrix();
                    GL.glNewList(SHOULDER_LIST, GL.GL_COMPILE);
                    //shoulder
                    GL.glTranslatef(0, 0.6f, 0);
                    GL.glTranslated(0, 0, shoulderLength - 0.55);
                    GL.glRotatef(55, 1, 0, 0);
                    GLU.gluCylinder(obj, radius, radius, shoulderLength, 20, 20);
                    GL.glTranslated(0, 0, shoulderLength);
                    GLU.gluSphere(obj, 1.3 * radius, 20, 20);
                    GL.glEndList();
                    GL.glPopMatrix();

                    GL.glPushMatrix();
                    GL.glNewList(ARM_LIST, GL.GL_COMPILE);
                    //arm
                    GL.glRotatef(-30, 1, 0, 0);
                    GLU.gluCylinder(obj, radius, radius, armLength, 20, 20);
                    GL.glTranslated(0, 0, armLength);
                    GLU.gluSphere(obj, radius * 2.2, 20, 20);

                    //sword
                    GL.glScalef(0.3f, 0.3f, 0.3f);
                    sword.DrawModel(true, 1);
                    GL.glScalef(-0.3f, -0.3f, -0.3f);
                    break;
            }
            GL.glEndList();
            GL.glPopMatrix();


        }

        public void Draw()
        {
            GL.glPushMatrix();
            GL.glNewList(Robot_LIST, GL.GL_COMPILE);

            //build the Robot
            GL.glTranslatef(0, Jump, 0);
            GL.glTranslatef(0, 1.3f, 0);
            // build Robot head
            GL.glCallList(HEAD_LIST);
            GL.glTranslatef(0, -1.3f, 0);
            // build Robot body 
            GL.glCallList(BODY_LIST);
            //build Robot hands   
            DrawHand(false);  //left hand
            GL.glRotatef(180, 0, 1, 0);
            if (WeaponIndex == 1 || WeaponIndex == 2)
                GL.glScalef(-1, 1, 1);
            DrawHand(true);  //right hand

            //build Robot legs
            GL.glRotatef(90, 1, 0, 0);
            GL.glTranslatef(0, 0, 1.9f);
            GL.glTranslatef(0, 0.3f, 0);
            DrawLeg(false);  //left leg
            GL.glTranslatef(0, -0.6f, 0);
            DrawLeg(true);  //right leg

            GL.glEndList();
            GL.glPopMatrix();
        }

        public void Prepare()
        {
            radius = 0.18f;

            Robot_LIST = GL.glGenLists(7);
            ARM_LIST = Robot_LIST + 1;
            SHOULDER_LIST = Robot_LIST + 2;
            HAND_LIST = Robot_LIST + 3;
            BODY_LIST = Robot_LIST + 4;
            HEAD_LIST = Robot_LIST + 5;
            LEG_UP_LIST = Robot_LIST + 6;
            LEG_DOWN_LIST = Robot_LIST + 7;


            GL.glPushMatrix();
            GL.glNewList(HEAD_LIST, GL.GL_COMPILE);

            GL.glTranslatef(0, 1, 0);
            //head
            GL.glPushMatrix();
            GL.glTranslatef(0, 1, 0);
            GL.glRotatef(-90, 1, 0, 0);
            GL.glEnable(GL.GL_LIGHTING);
            GL.glBindTexture(GL.GL_TEXTURE_2D, this.headTexture);
            GL.glEnable(GL.GL_TEXTURE_2D);
            GLU.gluSphere(obj, radius * 3.5, 20, 20);
            GL.glDisable(GL.GL_TEXTURE_2D);
            GL.glRotatef(180, 1, 0, 0);
            GLU.gluCylinder(obj, 3.5 * radius, 3.5 * radius, radius * 2, 20, 20);
            GL.glTranslatef(0, 0, radius * 2);
            GLU.gluCylinder(obj, 3.5 * radius, 0.5, radius * 2, 20, 20);
            GL.glTranslatef(0, 0, radius * 2);
            GLU.gluCylinder(obj, 0.5, 0, radius * 2, 20, 20);
            GL.glPopMatrix();
            GL.glEndList();
            GL.glPopMatrix();

            GL.glPushMatrix();
            GL.glNewList(BODY_LIST, GL.GL_COMPILE);


            //ALL body
            //neck
            GL.glPushMatrix();
            GL.glRotatef(90, 1, 0, 0);
            GL.glTranslatef(0, 0, -2.0f);
            GLU.gluCylinder(obj, 1.5 * radius, 1.5 * radius, bodyLength, 20, 20);
            GL.glPopMatrix();

            ////body up
            GL.glRotatef(90, 1, 0, 0);
            GL.glTranslatef(0, 0, -1.2f);
            GLU.gluCylinder(obj, radius, 6 * radius, bodyLength / 4, 20, 20);
            GL.glTranslatef(0, 0, 1.2f);

            //body midle
            GL.glScalef(1.0f, -1.0f, 1.0f);
            GL.glTranslatef(0, 0, -(bodyLength / 4 + 0.2f));
            GLU.gluCylinder(obj, 6 * radius, 3 * radius, bodyLength / 1.5f, 20, 20);
            GL.glTranslatef(0, 0, (bodyLength / 4 + 0.2f));
            GL.glScalef(1.0f, 1.0f, 1.0f);

            //Tusik
            GLU.gluCylinder(obj, 3 * radius, 3 * radius, bodyLength / 1.5f, 20, 20);

            //body down
            GL.glTranslatef(0, 0, (bodyLength / 1.5f));
            GLU.gluCylinder(obj, 3 * radius, radius, bodyLength / 3.5f, 10, 10);
            GL.glTranslatef(0, 0, -(bodyLength / 1.5f));

            GL.glRotatef(-90, 1, 0, 0);
            GL.glEndList();
            GL.glPopMatrix();
            DrawDynamicHand();

            GL.glPushMatrix();
            GL.glNewList(LEG_UP_LIST, GL.GL_COMPILE);
            //leg_up
            GLU.gluCylinder(obj, 1.5f * radius, 1.5f * radius, legUpLength, 20, 20);
            GL.glTranslated(0, 0, legUpLength);
            GLU.gluSphere(obj, radius * 1.7f, 20, 20);
            GL.glEndList();
            GL.glPopMatrix();


            GL.glPushMatrix();
            GL.glNewList(LEG_DOWN_LIST, GL.GL_COMPILE);
            //leg_down
            GLU.gluCylinder(obj, 1.5f * radius, 1.5f * radius, legDownLength, 20, 20);
            GL.glTranslated(0, 0, legDownLength);
            GLU.gluSphere(obj, 1.7f * radius, 20, 20);
            GL.glEndList();
            GL.glPopMatrix();
        }

        public void PrepareShadow()
        {
            radius = 0.18f;

            Robot_SHADOW_LIST = GL.glGenLists(7);
            ARM_SHADOW_LIST = Robot_SHADOW_LIST + 1;
            SHOULDER_SHADOW_LIST = Robot_SHADOW_LIST + 2;
            HAND_SHADOW_LIST = Robot_SHADOW_LIST + 3;
            BODY_SHADOW_LIST = Robot_SHADOW_LIST + 4;
            HEAD_SHADOW_LIST = Robot_SHADOW_LIST + 5;
            LEG_UP_SHADOW_LIST = Robot_SHADOW_LIST + 6;
            LEG_DOWN_SHADOW_LIST = Robot_SHADOW_LIST + 7;

            GL.glPushMatrix();
            GL.glNewList(HEAD_SHADOW_LIST, GL.GL_COMPILE);

            GL.glTranslatef(0, 1, 0);
            //head
            GL.glPushMatrix();
            GL.glTranslatef(0, 1, 0);
            GL.glRotatef(-90, 1, 0, 0);
            GLU.gluSphere(obj, radius * 3.5, 20, 20);
            GL.glRotatef(180, 1, 0, 0);
            GLU.gluCylinder(obj, 3.5 * radius, 3.5 * radius, radius * 2, 20, 20);
            GL.glTranslatef(0, 0, radius * 2);
            GLU.gluCylinder(obj, 3.5 * radius, 0.5, radius * 2, 20, 20);
            GL.glTranslatef(0, 0, radius * 2);
            GLU.gluCylinder(obj, 0.5, 0, radius * 2, 20, 20);
            GL.glPopMatrix();
            GL.glEndList();
            GL.glPopMatrix();

            GL.glPushMatrix();
            GL.glNewList(BODY_SHADOW_LIST, GL.GL_COMPILE);


            //ALL body
            //neck
            GL.glPushMatrix();
            GL.glRotatef(90, 1, 0, 0);
            GL.glTranslatef(0, 0, -2.0f);
            GLU.gluCylinder(obj, 1.5 * radius, 1.5 * radius, bodyLength, 20, 20);
            GL.glPopMatrix();

            ////body up
            GL.glRotatef(90, 1, 0, 0);
            GL.glTranslatef(0, 0, -1.2f);
            GLU.gluCylinder(obj, radius, 6 * radius, bodyLength / 4, 20, 20);
            GL.glTranslatef(0, 0, 1.2f);

            //body midle
            GL.glScalef(1.0f, -1.0f, 1.0f);
            GL.glTranslatef(0, 0, -(bodyLength / 4 + 0.2f));
            GLU.gluCylinder(obj, 6 * radius, 3 * radius, bodyLength / 1.5f, 20, 20);
            GL.glTranslatef(0, 0, (bodyLength / 4 + 0.2f));
            GL.glScalef(1.0f, 1.0f, 1.0f);

            //Tusik
            GLU.gluCylinder(obj, 3 * radius, 3 * radius, bodyLength / 1.5f, 20, 20);

            //body down
            GL.glTranslatef(0, 0, (bodyLength / 1.5f));
            GLU.gluCylinder(obj, 3 * radius, radius, bodyLength / 3.5f, 10, 10);
            GL.glTranslatef(0, 0, -(bodyLength / 1.5f));

            GL.glRotatef(-90, 1, 0, 0);
            GL.glEndList();
            GL.glPopMatrix();
            DrawDynamicShadowHand();

            GL.glPushMatrix();
            GL.glNewList(LEG_UP_SHADOW_LIST, GL.GL_COMPILE);
            //leg_up
            GLU.gluCylinder(obj, 1.5f * radius, 1.5f * radius, legUpLength, 20, 20);
            GL.glTranslated(0, 0, legUpLength);
            GLU.gluSphere(obj, radius * 1.7f, 20, 20);
            GL.glEndList();
            GL.glPopMatrix();


            GL.glPushMatrix();
            GL.glNewList(LEG_DOWN_SHADOW_LIST, GL.GL_COMPILE);
            //leg_down
            GLU.gluCylinder(obj, 1.5f * radius, 1.5f * radius, legDownLength, 20, 20);
            GL.glTranslated(0, 0, legDownLength);
            GLU.gluSphere(obj, 1.7f * radius, 20, 20);
            GL.glEndList();
            GL.glPopMatrix();
        }

        public void PrepareAndDrawShadow()
        {
            PrepareShadow();
            //DrawShadow();
        }

        void DrawDynamicShadowHand()
        {
            switch (WeaponIndex)
            {
                case 0:
                    GL.glPushMatrix();
                    GL.glNewList(SHOULDER_SHADOW_LIST, GL.GL_COMPILE);
                    //shoulder
                    GL.glTranslatef(0, 0.6f, 0);
                    GL.glTranslated(0, 0, shoulderLength - 0.55);
                    GL.glRotatef(55, 1, 0, 0);
                    GLU.gluCylinder(obj, radius, radius, shoulderLength, 20, 20);
                    GL.glTranslated(0, 0, shoulderLength);
                    GLU.gluSphere(obj, 1.3 * radius, 20, 20);
                    GL.glEndList();
                    GL.glPopMatrix();

                    GL.glPushMatrix();
                    GL.glNewList(ARM_SHADOW_LIST, GL.GL_COMPILE);
                    //arm
                    GL.glRotatef(-30, 1, 0, 0);
                    GLU.gluCylinder(obj, radius, radius, armLength, 20, 20);
                    GL.glTranslated(0, 0, armLength);
                    GLU.gluSphere(obj, radius * 2.2, 20, 20);

                    GL.glEndList();
                    GL.glPopMatrix();
                    break;
                case 1:
                    GL.glPushMatrix();
                    GL.glNewList(SHOULDER_SHADOW_LIST, GL.GL_COMPILE);
                    //shoulder
                    GL.glTranslatef(0, 0.6f, 0);
                    GL.glTranslated(0, 0, shoulderLength - 0.55);
                    GL.glRotatef(65, 0, 1, 0);
                    GLU.gluCylinder(obj, radius, radius, shoulderLength, 20, 20);
                    GL.glTranslated(0, 0, shoulderLength);
                    GLU.gluSphere(obj, 1.3 * radius, 20, 20);
                    GL.glEndList();
                    GL.glPopMatrix();

                    GL.glPushMatrix();
                    GL.glNewList(ARM_SHADOW_LIST, GL.GL_COMPILE);
                    //arm
                    GL.glRotatef(-80, 1, 0, 0);
                    GL.glRotatef(20, 0, 1, 0);
                    GLU.gluCylinder(obj, radius, radius, armLength, 20, 20);
                    GL.glTranslated(0, 0, armLength);
                    GLU.gluSphere(obj, radius * 2.2, 20, 20);

                    //gun1
                    GL.glTranslatef(0, 0.6f, 0);
                    GL.glScalef(1.5f, 1.5f, 1.5f);
                    gun1.DrawModel(false, 1);
                    break;
                case 2:
                    GL.glPushMatrix();
                    GL.glNewList(SHOULDER_SHADOW_LIST, GL.GL_COMPILE);
                    //shoulder
                    GL.glTranslatef(0, 0.6f, 0);
                    GL.glTranslated(0, 0, shoulderLength - 0.55);
                    GL.glRotatef(65, 1, 0, 0);
                    GLU.gluCylinder(obj, radius, radius, shoulderLength, 20, 20);
                    GL.glTranslated(0, 0, shoulderLength);
                    GLU.gluSphere(obj, 1.3 * radius, 20, 20);
                    GL.glEndList();
                    GL.glPopMatrix();

                    GL.glPushMatrix();
                    GL.glNewList(ARM_SHADOW_LIST, GL.GL_COMPILE);
                    //arm
                    GL.glRotatef(-80, 1, 0, 0);
                    GL.glRotatef(90, 0, 1, 0);
                    GLU.gluCylinder(obj, radius, radius, armLength, 20, 20);
                    GL.glTranslated(0, 0, armLength);
                    GLU.gluSphere(obj, radius * 2.2, 20, 20);

                    //gun2
                    GL.glRotatef(-30, 0, 0, 1);
                    GL.glTranslatef(0, 0.6f, 0);
                    GL.glScalef(1.3f, 1.3f, 1.3f);
                    gun2.DrawModel(false, 1);
                    break;
                case 3:
                    GL.glPushMatrix();
                    GL.glNewList(SHOULDER_SHADOW_LIST, GL.GL_COMPILE);
                    //shoulder
                    GL.glTranslatef(0, 0.6f, 0);
                    GL.glTranslated(0, 0, shoulderLength - 0.55);
                    GL.glRotatef(55, 1, 0, 0);
                    GLU.gluCylinder(obj, radius, radius, shoulderLength, 20, 20);
                    GL.glTranslated(0, 0, shoulderLength);
                    GLU.gluSphere(obj, 1.3 * radius, 20, 20);
                    GL.glEndList();
                    GL.glPopMatrix();

                    GL.glPushMatrix();
                    GL.glNewList(ARM_SHADOW_LIST, GL.GL_COMPILE);
                    //arm
                    GL.glRotatef(-30, 1, 0, 0);
                    GLU.gluCylinder(obj, radius, radius, armLength, 20, 20);
                    GL.glTranslated(0, 0, armLength);
                    GLU.gluSphere(obj, radius * 2.2, 20, 20);

                    //sword
                    GL.glScalef(0.3f, 0.3f, 0.3f);
                    sword.DrawModel(false, 1);
                    GL.glScalef(-0.3f, -0.3f, -0.3f);
                    break;
            }
            GL.glEndList();
            GL.glPopMatrix();


        }

        public void DrawShadowHand(bool side)
        {
            GL.glPushMatrix();
            GL.glTranslated(0, 0, 0.23f);
            GL.glPushMatrix();
            if (side == false)
            {
                GL.glTranslatef(0, 0.6f, 0);
                if (WeaponIndex == 1 || WeaponIndex == 2)
                {
                    GL.glRotatef(ShoulderLeftAngle / 10, 0, 0, 1);
                    GL.glRotatef(WeaponPlace, 0, 0, 1);//hand Move from fire
                }
                else
                    GL.glRotatef(-ShoulderLeftAngle, 0, 0, 1);
                GL.glTranslatef(0, -0.6f, 0);
            }
            else
            {
                GL.glTranslatef(0, 0.6f, 0);
                if (WeaponIndex == 1 || WeaponIndex == 2)
                {
                    GL.glRotatef(-ShoulderRightAngle / 10, 0, 0, 1);
                    GL.glRotatef(WeaponPlace, 0, 0, 1);//hand Move from fire
                }
                else
                    GL.glRotatef(-ShoulderRightAngle, 0, 0, 1);
                GL.glTranslatef(0, -0.6f, 0);
            }

            GL.glTranslatef(0, 0.6f, 0.55f);
            GLU.gluCylinder(obj, radius, radius, radius * 1.2, 40, 20);
            GL.glTranslated(0, 0, radius * 1.2);
            GLU.gluDisk(obj, 0, radius, 40, 20);
            GLU.gluSphere(obj, radius * 1.2, 20, 20);
            GL.glTranslatef(0, -0.6f, -0.55f);

            // build Robot shoulder 
            GL.glCallList(SHOULDER_SHADOW_LIST);
            GL.glRotatef(ArmLeftAngle, 1, 0, 0);

            //Move down hand with WeaponIndex
            if (WeaponIndex == 2)
            {
                GL.glRotatef(-WeaponPlace, 0, 1, 0);
                GL.glRotatef(WeaponPlace, 0, 0, 1);
            }

            // build Robot arm   
            GL.glCallList(ARM_SHADOW_LIST);
            GL.glPopMatrix();

            if (IsMoving == false) //do not see bullets when walking
            {
                GL.glPushMatrix();
                if (WeaponIndex == 2)
                {
                    GL.glTranslatef(0.2f, 0.2f, 1.4f);
                    GL.glRotatef(-8, 0, 1, 0);
                }
                else//WeaponIndex 1
                {
                    GL.glTranslatef(2.2f, 1.4f, 1.3f);
                    GL.glRotatef(-8, 0, 1, 0);
                }

                GL.glRotatef(90, 0, 1, 0);
                GL.glTranslatef(0, 0, ShootDist);
                if (WeaponIndex == 1)
                {
                    //draw bullet
                    radius = radius * 2.2f;
                    GL.glColor3f(0.0f, 0.0f, 0);
                    GLU.gluCylinder(obj, radius / 4.5f, radius / 4.5f, radius / 2, 40, 20);
                    GLU.gluDisk(obj, 0, radius / 4.5f, 20, 20);
                    GL.glTranslatef(0, 0, radius / 2);
                    GLU.gluCylinder(obj, radius / 4.5f, 0, radius / 4, 40, 20);
                    radius = radius / 2.2f;
                }
                else if (WeaponIndex == 2)
                {
                    //draw bullet
                    GL.glColor3f(0.0f, 0.0f, 0);
                    GLU.gluCylinder(obj, radius / 1.3f, radius / 1.3f, radius * 7, 40, 20);
                    GLU.gluDisk(obj, 0, radius / 1.3f, 20, 20);
                    GL.glTranslatef(0, 0, radius * 7);
                    GLU.gluCylinder(obj, radius / 1.3f, 0, radius * 5, 40, 20);
                }
                GL.glPopMatrix();
            }
            GL.glPopMatrix();
        }

        public void DrawShadowLeg(bool side)
        {
            GL.glPushMatrix();
            if (side == false)
                GL.glRotatef(LegLeftUpAngle, 0, 1, 0);
            else
                GL.glRotatef(LegRightUpAngle, 0, 1, 0);
            GLU.gluSphere(obj, radius * 1.7, 20, 20);
            GL.glCallList(LEG_UP_SHADOW_LIST);
            if (side == false)
                if (WeaponIndex == 1 || WeaponIndex == 2)
                    GL.glRotatef(LegLeftDownAngle, 0, 1, 0);
                else
                {
                    GL.glScalef(-1, 1, 1);
                    GL.glRotatef(LegLeftDownAngle, 0, 1, 0);
                    GL.glScalef(1, 1, 1);
                }
            else
                if (WeaponIndex == 1 || WeaponIndex == 2)
                    GL.glRotatef(LegRightDownAngle, 0, 1, 0);
                else
                {
                    GL.glScalef(-1, 1, 1);
                    GL.glRotatef(LegRightDownAngle, 0, 1, 0);
                    GL.glScalef(1, 1, 1);
                }
            GL.glCallList(LEG_DOWN_SHADOW_LIST);
            GL.glPopMatrix();

        }

        public void DrawShadow()
        {
            GL.glPushMatrix();
            GL.glNewList(Robot_SHADOW_LIST, GL.GL_COMPILE);
            //build the Robot
            GL.glTranslatef(0, Jump, 0);
            GL.glTranslatef(0, 1.3f, 0);
            // build Robot head
            GL.glCallList(HEAD_SHADOW_LIST);
            GL.glTranslatef(0, -1.3f, 0);
            // build Robot body 
            GL.glCallList(BODY_SHADOW_LIST);
            //build Robot hands   
            DrawShadowHand(false);  //left hand
            GL.glRotatef(180, 0, 1, 0);
            if (WeaponIndex == 1 || WeaponIndex == 2)
                GL.glScalef(-1, 1, 1);
            DrawShadowHand(true);  //right hand

            //build Robot legs
            GL.glRotatef(90, 1, 0, 0);
            GL.glTranslatef(0, 0, 1.9f);
            GL.glTranslatef(0, 0.3f, 0);
            DrawShadowLeg(false);  //left leg
            GL.glTranslatef(0, -0.6f, 0);
            DrawShadowLeg(true);  //right leg

            GL.glEndList();
            GL.glPopMatrix();
        }


    }
}
